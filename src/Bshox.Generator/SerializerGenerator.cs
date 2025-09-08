using System.Diagnostics;
using Bshox.Attributes;
using Bshox.Generator.Contracts;
using Bshox.Generator.Extensions;
using Bshox.Generator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Bshox.Generator;

internal static class SerializerGenerator
{
    public static void Generate(SerializerInfo serializer, ClassDeclarationSyntax classDeclaration, CancellationToken cancellation)
    {
        if (!classDeclaration.IsPartial())
        {
            serializer.ReportDiagnostic(Diagnostics.TypeMustBePartial, classDeclaration.Identifier, serializer.ClassSymbol.Name, nameof(BshoxSerializerAttribute));
            return;
        }

        if (serializer.ClassSymbol.IsNested())
        {
            serializer.ReportDiagnostic(Diagnostics.TypeMustNotBeNested, classDeclaration.Identifier, serializer.ClassSymbol.Name, nameof(BshoxSerializerAttribute));
            return;
        }

        // TODO: must not have base type
        // TODO: must not be abstract
        // TODO: must not be static

        try
        {
            Generate(serializer, cancellation);
        }
        catch (DiagnosticException ex)
        {
            serializer.ReportDiagnostic(ex.Diagnostic);
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
        {
            serializer.InternalError(null as Location, ex.Message, ex.StackTrace);
#if DEBUG
            // Debugger.Launch();
#endif
        }
    }

    private static void Generate(SerializerInfo serializer, CancellationToken cancellation)
    {
        var classSymbol = serializer.ClassSymbol;
        if (serializer.RequestedTypes.IsEmpty)
        {
            serializer.ReportDiagnostic(Diagnostics.SerializerMustHaveAtLeastOneType, classSymbol, classSymbol);
            return;
        }

#if DEBUG
        List<string> logMessages = [];
        Action<string> log = logMessages.Add;
#else
        Action<string>? log = null;
#endif
        var explicitDemands = serializer.RequestedTypes.Select(ContractDemand.DefaultForType).ToList();
        List<ContractInfo> contracts = GetContracts(explicitDemands, serializer.ContractResolver, out bool error, log, serializer, cancellation);

        foreach (var contractInfo in contracts)
        {
            if (contractInfo.Generator?.TryGenerate(contractInfo, serializer) == false)
            {
                error = true;
            }
        }

        if (error)
        {
            return;
        }

        var code = new SourceWriter();
#if DEBUG
        foreach (var logMessage in logMessages)
        {
            code.WriteComment(logMessage);
        }
#endif
        foreach (var type in serializer.RequestedTypes)
        {
            code.WriteComment($"Requested type: {type}");
        }
        foreach (var contract in contracts)
        {
            code.WriteComment($"Contract: {contract.Type} ({(contract.Explicit ? contract.PropertyName : contract.VariableName)})");
        }

        code.DeclareAlias(serializer);
        code.WriteLine();

        var ns = classSymbol.ContainingNamespace;
        if (!ns.IsGlobalNamespace)
        {
            code.WriteLine($"namespace {ns};");
            code.WriteLine();
        }

        string className = classSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);

        // write xml doc
        code.WriteLine("/// <summary>");
        code.WriteLine("/// A source generated Bshox serializer that can serialize the following types:");
        foreach (var contract in contracts.Where(c => c.Explicit)) // only document the explicitly requested contracts
        {
            string displayString = contract.Type.ToXmlCommentString();
            code.WriteLine($"/// <para>{displayString}</para>");
        }
        code.WriteLine("/// </summary>");

        code.WriteLine(Constants.GeneratedCodeAttributeText);
        code.WriteLine($"sealed partial class {className} : bsx::BshoxSerializer");
        code.OpenScope();
        {
            foreach (var contract in contracts)
            {
                code.WriteLine($"private static readonly bsx::BshoxContract<{contract.Type.FullyQualifiedToString()}> {contract.VariableName};");
                if (contract.Explicit)
                {
                    if (contract.Generator is { } generator)
                    {
                        code.WriteLine($"""
                                        /// <summary>
                                        /// A source generated Bshox contract for {contract.Type.ToXmlCommentString()}
                                        /// </summary>
                                        /// <remarks>
                                        """);
                        generator.WriteXmlDocRemarks(code, serializer.ContractResolver);
                        code.WriteLine("/// </remarks>");
                    }
                    else
                    {
                        // TODO: add more info about the contract
                        code.WriteLine($"""
                                        /// <summary>
                                        /// A Bshox contract for {contract.Type.ToXmlCommentString()}
                                        /// </summary>
                                        """);
                    }

                    code.WriteLine($"public static bsx::BshoxContract<{contract.Type.FullyQualifiedToString()}> {contract.PropertyName} => {contract.VariableName};");
                }
            }

            code.WriteLine();

            // singleton
            code.WriteLine($$"""
                        /// <summary>
                        /// Singleton instance of {{classSymbol.ToXmlCommentString()}}
                        /// </summary>
                        public static {{className}} Instance { get; } = new {{className}}();
                        """);
            code.WriteLine();

            // static constructor
            code.WriteLine($"static {className}()");
            code.OpenScope();
            {
                foreach (var contract in contracts)
                {
                    code.WriteLine($"{contract.VariableName} = {contract.GetDefinition(serializer.ContractResolver)};");
                }
            }
            code.CloseScope(); // static constructor
            code.WriteLine();

            // IBshoxContract GetContractInternal(Type type)
            code.WriteLine("protected override bsx::IBshoxContract GetContractInternal(global::System.Type type)");
            code.OpenScope();
            {
                foreach (var serializableType in contracts)
                {
                    code.WriteLine($"""
                                    if (type == typeof({serializableType.Type.FullyQualifiedToString()}))
                                        return {serializableType.VariableName};
                                    """);
                }
                code.WriteLine("return null;");
            }
            code.CloseScope(); // IBshoxContract GetContractInternal(Type type)
        }
        code.CloseScope(); // class

        string fullType = classSymbol.FullyQualifiedToStringNG()
            .Replace('<', '(')
            .Replace('>', ')');
        string fileName = Constants.InvalidPathChars.Replace($"{fullType}.g.cs", string.Empty);

        serializer.AddSource(fileName, code);
    }

    /// <remarks>
    /// This is probably the most complex function in this project.
    /// The static dependencies between the contracts form a directed acyclic graph.
    /// The output of this function is a list of contracts where the static dependencies are in the correct order.
    /// </remarks>
    private static List<ContractInfo> GetContracts(IReadOnlyList<ContractDemand> demands, IContractResolver resolver, out bool error, Action<string>? log, IDiagnosticOutput diagnostics, CancellationToken cancellation)
    {
        log ??= _ => { };
        // dict of all the demands that have been resolved
        Dictionary<ContractDemand, ContractInfo> resolved = [];
        error = false;

        // the stack of demands that still need to be resolved.
        var stack = new Stack<ContractDemand>(demands);

        // add all the dependencies of the already resolved contracts to the stack
        foreach (var demand in resolved.Values.SelectMany(c => c.Dependencies))
        {
            stack.Push(demand);
        }
        while (stack.Count > 0) // can't use foreach since we're modifying the collection in the loop
        {
            cancellation.ThrowIfCancellationRequested();
            var demand = stack.Pop();
            log($"Checking {demand}");
            if (resolved.ContainsKey(demand))
            {
                log($"Already resolved {demand}");
                continue;
            }

            if (!resolver.TryResolveContract(demand, out var contract))
            {
                log($"Failed to resolve {demand}");
                error = true;
                // don't return since there may be more than one of these errors.
                continue;
            }

            resolved.Add(demand, contract);

            // Now deal with the contract dependencies
            foreach (var dependency in contract.Dependencies)
            {
                cancellation.ThrowIfCancellationRequested();
                log($"Adding dependency {contract} -> {dependency}");
                if (!resolved.ContainsKey(dependency))
                {
                    // If we don't already have a contract for this type, we will add it later.
                    stack.Push(dependency);
                }
            }
        }

        // mark all contracts for the types from the "types" collection as "Explicit"
        foreach (var demand in demands)
        {
            if (resolved.TryGetValue(demand, out ContractInfo? contract))
                contract.Explicit = true;
            else
                Debug.Assert(error, "error"); // this should only happen if we failed to find one of the contracts.
        }

        IEnumerable<string> propertyNames = resolved.Values.Where(c => c.Explicit).Select(c => c.PropertyName).ToList();
        if (propertyNames.HasDuplicate())
        {
            // TODO: add proper diagnostic for this.
            diagnostics.InternalError(null as Location, "Property names are not unique", string.Join(", ", propertyNames));
        }

        if (error)
        {
            return [];
        }

        // this map represents the edges of the directed graph of the static dependencies between the contracts
        Dictionary<ContractInfo, HashSet<ContractInfo>> map = [];
        foreach (var contract in resolved.Values)
        {
            cancellation.ThrowIfCancellationRequested();
            HashSet<ContractInfo> list = [];
            if (contract.StaticDependencies)
            {
                foreach (var dependency in contract.Dependencies)
                {
                    _ = list.Add(resolved[dependency]);
                }
            }
            map.Add(contract, list);
        }

        List<ContractInfo> contracts = [];

        // to sort the contracts we iteratively remove the first node in the graph that has no outgoing edges and add it to the output list.
        while (map.Count > 0)
        {
            cancellation.ThrowIfCancellationRequested();
            ContractInfo contract;
            try
            {
                // find a node with no outgoing edges (i.e. a contract with no static dependencies that aren't already in the output list)
                contract = map.First(static kv => kv.Value.Count == 0).Key;
            }
            // this call will fail if and only if the graph is cyclical. That would be a bug.
            catch (InvalidOperationException)
            {
                var list = map.Keys.Select(k => k.Type.FullyQualifiedToStringNG());
                diagnostics.InternalError(null as Location, "The static type dependencies are cyclical", string.Join(", ", list));
                error = true;
                return [];
            }
            contracts.Add(contract); // add it to the end of the list

            var removed = map.Remove(contract); // remove the node from the graph
            Debug.Assert(removed, "removed");
            foreach (var list in map.Values) // remove the edges from the graph
            {
                _ = list.Remove(contract);
            }
        }

        AssertDependencyOrder(contracts, resolved, diagnostics);
        return contracts;
    }

    [Conditional("DEBUG")]
    private static void AssertDependencyOrder(List<ContractInfo> contracts, Dictionary<ContractDemand, ContractInfo> resolved, IDiagnosticOutput diagnostics)
    {
        for (int i = 0; i < contracts.Count; i++)
        {
            var contract = contracts[i];
            if (!contract.StaticDependencies)
            {
                // only static dependencies need to be in the correct order.
                continue;
            }
            foreach (var dependency in contract.Dependencies)
            {
                var dependencyContract = resolved[dependency];
                int dependencyIndex = contracts.IndexOf(dependencyContract);
                // the contract of the dependency must have a lower index that the contract at index i.
                Debug.Assert(dependencyIndex != -1, "dependencyIndex != -1");
                Debug.Assert(dependencyIndex != i, "dependencyIndex != i");
                if (dependencyIndex > i)
                    diagnostics.InternalError(null as Location, $"Dependency order is wrong: {contract} ({i}) depends on {dependencyContract} ({dependencyIndex})");
            }
        }
    }
}
