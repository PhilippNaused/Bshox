// Bshox, PublicKeyToken=71dcaf280189db03
// Platform: AnyCPU (64-bit preferred)
// Runtime: v4.0.30319
// Reference: netstandard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51
// Reference: System.Buffers, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51
// Reference: System.Memory, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51
// Reference: System.Runtime.CompilerServices.Unsafe, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
[assembly: System.Reflection.AssemblyCompany("Philipp Naused")]
[assembly: System.Reflection.AssemblyCopyright("© Philipp Naused")]
[assembly: System.Reflection.AssemblyDescription("High performance binary serialization for C#")]
[assembly: System.Reflection.AssemblyMetadata("RepositoryUrl", "https://github.com/PhilippNaused/Bshox")]
[assembly: System.Reflection.AssemblyProduct("Bshox")]
[assembly: System.Reflection.AssemblyTitle("Bshox")]
[assembly: System.Resources.NeutralResourcesLanguage("en-US")]
[assembly: System.Runtime.CompilerServices.CompilationRelaxations(8)]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Benchmark, PublicKey=0024000004800000940000000602000000240000525341310004000001000100dd5aaf9dfcf30dc5f78e30f2ccbd27f7dc88c9a8db26eda6ed229b883fd34edbdcffce799b053db93a3c4d288e976266f67acb9cd2fa5d24c3642e5b8191d53aebe1954a64512f7d9e992eeb779d011e2c25b4b76d8cacde8f0c675c8093a2f4b8eaafbf6ff24e271d502b023c5f5f2afced11ed447be096d332d3f8c4f70fcd")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Bshox.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100dd5aaf9dfcf30dc5f78e30f2ccbd27f7dc88c9a8db26eda6ed229b883fd34edbdcffce799b053db93a3c4d288e976266f67acb9cd2fa5d24c3642e5b8191d53aebe1954a64512f7d9e992eeb779d011e2c25b4b76d8cacde8f0c675c8093a2f4b8eaafbf6ff24e271d502b023c5f5f2afced11ed447be096d332d3f8c4f70fcd")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Bshox.Utils, PublicKey=0024000004800000940000000602000000240000525341310004000001000100dd5aaf9dfcf30dc5f78e30f2ccbd27f7dc88c9a8db26eda6ed229b883fd34edbdcffce799b053db93a3c4d288e976266f67acb9cd2fa5d24c3642e5b8191d53aebe1954a64512f7d9e992eeb779d011e2c25b4b76d8cacde8f0c675c8093a2f4b8eaafbf6ff24e271d502b023c5f5f2afced11ed447be096d332d3f8c4f70fcd")]
[assembly: System.Runtime.CompilerServices.RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: System.Runtime.Versioning.TargetFramework(".NETStandard,Version=v2.0", FrameworkDisplayName = ".NET Standard 2.0")]
[assembly: System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.RequestMinimum, SkipVerification = true)]
[module: System.Runtime.CompilerServices.RefSafetyRules(11)]
[module: System.Security.UnverifiableCode]
namespace Bshox
{
    public enum BshoxCode : byte
    {
        VarInt = 0,
        Fixed4 = 1,
        Fixed8 = 2,
        Prefixed = 3,
        Array = 4,
        SubObject = 5
    }
    public static class BshoxConstants
    {
        public const uint MinKey = 1u;
        public const uint MaxKey = 536870911u;
    }
    public abstract class BshoxContract<T> : Bshox.IBshoxContract
    {
        protected BshoxContract(Bshox.BshoxCode encoding);
        public Bshox.BshoxCode Encoding { get; }
        public abstract void Deserialize(ref Bshox.BshoxReader reader, out T value);
        public abstract void Serialize(ref Bshox.BshoxWriter writer, scoped ref readonly T value);
    }
    public static class BshoxContractExtensions
    {
        public static T Deserialize<T>(this Bshox.BshoxContract<T> contract, in System.Buffers.ReadOnlySequence<byte> sequence);
        public static T Deserialize<T>(this Bshox.BshoxContract<T> contract, System.ReadOnlyMemory<byte> memory);
        public static T Deserialize<T>(this Bshox.BshoxContract<T> contract, System.IO.Stream stream);
        public static async System.Threading.Tasks.Task<T> DeserializeAsync<T>(this Bshox.BshoxContract<T> contract, System.IO.Stream stream, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        public static void Serialize<T>(this Bshox.BshoxContract<T> contract, System.Buffers.IBufferWriter<byte> buffer, scoped in T value);
        public static void Serialize<T>(this Bshox.BshoxContract<T> contract, System.IO.Stream stream, scoped in T value);
        public static byte[] Serialize<T>(this Bshox.BshoxContract<T> contract, scoped in T value);
    }
    [System.Serializable]
    public class BshoxException : System.Exception
    {
        public BshoxException(string message);
        public BshoxException(string message, System.Exception? inner);
        protected BshoxException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context);
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context);
        public static void ThrowIfWrongEncoding(Bshox.BshoxCode encoding, Bshox.BshoxCode expected);
    }
    public readonly record struct BshoxOptions
    {
        public const int DefaultMaxDepth = 64;
        public int MaxDepth { get; init; }
    }
    public ref struct BshoxReader
    {
        public BshoxReader(System.Buffers.ReadOnlySequence<byte> sequence, Bshox.BshoxOptions options = default(Bshox.BshoxOptions));
        public BshoxReader(System.ReadOnlyMemory<byte> memory, Bshox.BshoxOptions options = default(Bshox.BshoxOptions));
        public long Consumed { readonly get; }
        public readonly int CurrentDepth { get; }
        public readonly long Length { get; }
        public readonly long Remaining { get; }
        public void Advance(int count);
        public void CopyTo(System.Span<byte> destination);
        public Bshox.Internals.DepthLockScope DepthLock();
        public int ReadArrayHeader(out Bshox.BshoxCode encoding);
        public byte ReadByte();
        public byte[] ReadByteArray();
        public double ReadDouble();
        public float ReadSingle();
        public string ReadString();
        public uint ReadTag(out Bshox.BshoxCode encoding);
        public uint ReadUInt32();
        public ulong ReadUInt64();
        public uint ReadVarInt32();
        public ulong ReadVarInt64();
        public int ReadZigZagVarInt32();
        public long ReadZigZagVarInt64();
        public void SkipValue(Bshox.BshoxCode encoding);
    }
    public abstract class BshoxSerializer
    {
        protected BshoxSerializer();
        public object Deserialize(in System.Buffers.ReadOnlySequence<byte> sequence, System.Type returnType);
        public object Deserialize(System.ReadOnlyMemory<byte> memory, System.Type returnType);
        public object Deserialize(System.IO.Stream stream, System.Type returnType);
        public async System.Threading.Tasks.Task<object> DeserializeAsync(System.IO.Stream stream, System.Type returnType, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        public Bshox.BshoxContract<T> GetContract<T>();
        public void Serialize(System.Buffers.IBufferWriter<byte> buffer, object value, System.Type inputType);
        public void Serialize(System.IO.Stream stream, object value, System.Type inputType);
        public byte[] Serialize(object value, System.Type inputType);
        protected abstract Bshox.IBshoxContract? GetContractInternal(System.Type type);
    }
    public ref struct BshoxWriter
    {
        public BshoxWriter(System.Buffers.IBufferWriter<byte> buffer, Bshox.BshoxOptions options = default(Bshox.BshoxOptions));
        public readonly int CurrentDepth { get; }
        public void Advance(int count);
        public Bshox.Internals.DepthLockScope DepthLock();
        public void Flush();
        public System.Span<byte> GetSpan(int sizeHint);
        public void WriteArrayHeader(int count, Bshox.BshoxCode elementEncoding);
        public void WriteByte(byte value);
        public void WriteByteArray(byte[] value);
        public void WriteDouble(double value);
        public void WriteSingle(float value);
        public void WriteString(string value);
        public void WriteTag(uint key, Bshox.BshoxCode encoding);
        public void WriteUInt32(uint value);
        public void WriteUInt64(ulong value);
        public void WriteVarInt32(uint value);
        public void WriteVarInt64(ulong value);
        public void WriteZigZagVarInt32(int value);
        public void WriteZigZagVarInt64(long value);
    }
    public static class DefaultContracts
    {
        public static Bshox.BshoxContract<bool> Boolean { get; }
        public static Bshox.BshoxContract<byte> Byte { get; }
        public static Bshox.BshoxContract<byte[]> ByteArray { get; }
        public static Bshox.BshoxContract<char> Char { get; }
        public static Bshox.BshoxContract<System.DateTime> DateTime { get; }
        public static Bshox.BshoxContract<double> Double { get; }
        public static Bshox.BshoxContract<System.Guid> Guid { get; }
        public static Bshox.BshoxContract<short> Int16 { get; }
        public static Bshox.BshoxContract<int> Int32 { get; }
        public static Bshox.BshoxContract<int> Int32Z { get; }
        public static Bshox.BshoxContract<long> Int64 { get; }
        public static Bshox.BshoxContract<long> Int64Z { get; }
        public static Bshox.BshoxContract<sbyte> SByte { get; }
        public static Bshox.BshoxContract<float> Single { get; }
        public static Bshox.BshoxContract<string> String { get; }
        public static Bshox.BshoxContract<System.TimeSpan> TimeSpan { get; }
        public static Bshox.BshoxContract<ushort> UInt16 { get; }
        public static Bshox.BshoxContract<uint> UInt32 { get; }
        public static Bshox.BshoxContract<ulong> UInt64 { get; }
        public static Bshox.BshoxContract<T[]> Array<T>(Bshox.BshoxContract<T> contract) where T : notnull;
        public static Bshox.BshoxContract<System.Collections.Concurrent.ConcurrentDictionary<TKey, TValue>> ConcurrentDictionary<TKey, TValue>(Bshox.BshoxContract<System.Collections.Generic.Dictionary<TKey, TValue>> contract) where TKey : notnull;
        public static Bshox.BshoxContract<System.Collections.Generic.Dictionary<TKey, TValue>> Dictionary<TKey, TValue>(Bshox.BshoxContract<TKey> keyContract, Bshox.BshoxContract<TValue> valueContract) where TKey : notnull;
        public static Bshox.BshoxContract<T> Enum<T>(Bshox.IBshoxContract contract) where T : unmanaged, System.Enum;
        public static Bshox.BshoxContract<System.Collections.Generic.IDictionary<TKey, TValue>> IDictionary<TKey, TValue>(Bshox.BshoxContract<System.Collections.Generic.Dictionary<TKey, TValue>> contract) where TKey : notnull;
        public static Bshox.BshoxContract<System.Collections.Generic.IList<T>> IList<T>(Bshox.BshoxContract<System.Collections.Generic.List<T>> contract);
        public static Bshox.BshoxContract<System.Collections.Generic.List<T>> List<T>(Bshox.BshoxContract<T> contract) where T : notnull;
        public static Bshox.BshoxContract<System.ValueTuple<T1>> ValueTuple<T1>(Bshox.BshoxContract<T1> contract1);
        public static Bshox.BshoxContract<(T1, T2)> ValueTuple<T1, T2>(Bshox.BshoxContract<T1> contract1, Bshox.BshoxContract<T2> contract2);
        public static Bshox.BshoxContract<(T1, T2, T3)> ValueTuple<T1, T2, T3>(Bshox.BshoxContract<T1> contract1, Bshox.BshoxContract<T2> contract2, Bshox.BshoxContract<T3> contract3);
        public static Bshox.BshoxContract<(T1, T2, T3, T4)> ValueTuple<T1, T2, T3, T4>(Bshox.BshoxContract<T1> contract1, Bshox.BshoxContract<T2> contract2, Bshox.BshoxContract<T3> contract3, Bshox.BshoxContract<T4> contract4);
        public static Bshox.BshoxContract<(T1, T2, T3, T4, T5)> ValueTuple<T1, T2, T3, T4, T5>(Bshox.BshoxContract<T1> contract1, Bshox.BshoxContract<T2> contract2, Bshox.BshoxContract<T3> contract3, Bshox.BshoxContract<T4> contract4, Bshox.BshoxContract<T5> contract5);
        public static Bshox.BshoxContract<(T1, T2, T3, T4, T5, T6)> ValueTuple<T1, T2, T3, T4, T5, T6>(Bshox.BshoxContract<T1> contract1, Bshox.BshoxContract<T2> contract2, Bshox.BshoxContract<T3> contract3, Bshox.BshoxContract<T4> contract4, Bshox.BshoxContract<T5> contract5, Bshox.BshoxContract<T6> contract6);
        public static Bshox.BshoxContract<(T1, T2, T3, T4, T5, T6, T7)> ValueTuple<T1, T2, T3, T4, T5, T6, T7>(Bshox.BshoxContract<T1> contract1, Bshox.BshoxContract<T2> contract2, Bshox.BshoxContract<T3> contract3, Bshox.BshoxContract<T4> contract4, Bshox.BshoxContract<T5> contract5, Bshox.BshoxContract<T6> contract6, Bshox.BshoxContract<T7> contract7);
        public static Bshox.BshoxContract<System.ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>> ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>(Bshox.BshoxContract<T1> contract1, Bshox.BshoxContract<T2> contract2, Bshox.BshoxContract<T3> contract3, Bshox.BshoxContract<T4> contract4, Bshox.BshoxContract<T5> contract5, Bshox.BshoxContract<T6> contract6, Bshox.BshoxContract<T7> contract7, Bshox.BshoxContract<TRest> contract8) where TRest : struct;
    }
    public interface IBshoxContract
    {
        Bshox.BshoxCode Encoding { get; }
        System.Type Type { get; }
        void Deserialize(ref Bshox.BshoxReader reader, out object value);
        void Serialize(ref Bshox.BshoxWriter writer, object value);
    }
    public interface ISpanContract<T>
    {
        void Deserialize(ref Bshox.BshoxReader reader, System.Span<T> destination);
        void Serialize(ref Bshox.BshoxWriter writer, System.ReadOnlySpan<T> values);
    }
}
namespace Bshox.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public class BshoxContractAttribute : System.Attribute
    {
        public BshoxContractAttribute();
        public bool ImplicitDefaultValues { get; set; }
        public bool ImplicitMembers { get; set; }
    }
    [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class BshoxDefaultContractAttribute : System.Attribute
    {
        public BshoxDefaultContractAttribute(System.Type containingType, string symbolName);
        public System.Type ContainingType { get; }
        public string SymbolName { get; }
    }
    [System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class BshoxMemberAttribute : System.Attribute
    {
        public BshoxMemberAttribute([System.Diagnostics.CodeAnalysis.ConstantExpected(Min = 1u, Max = 536870911u)] uint key);
        public uint Key { get; }
    }
    [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class BshoxSerializerAttribute : System.Attribute
    {
        public BshoxSerializerAttribute(params System.Type[] types);
        public System.Type[] Surrogates { get; set; }
        public System.Type[] Types { get; }
    }
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public sealed class BshoxSurrogateAttribute<T> : Bshox.Attributes.BshoxSurrogateAttribute
    {
        public BshoxSurrogateAttribute();
    }
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public class BshoxSurrogateAttribute : Bshox.Attributes.BshoxContractAttribute
    {
        public BshoxSurrogateAttribute(System.Type type);
        public System.Type Type { get; }
    }
}
namespace Bshox.Internals
{
    [System.Obsolete("This type should only be referenced implicitly")]
    public readonly ref struct DepthLockScope
    {
        public void Dispose();
    }
}
