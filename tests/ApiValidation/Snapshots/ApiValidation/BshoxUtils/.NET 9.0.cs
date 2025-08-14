// Bshox.Utils, PublicKeyToken=71dcaf280189db03
// Platform: AnyCPU (64-bit preferred)
// Runtime: v4.0.30319
// Reference: System.Runtime, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Reference: System.Collections, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Reference: Bshox, Version=0.0.0.0, Culture=neutral, PublicKeyToken=71dcaf280189db03
// Reference: System.Text.Encoding.Extensions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Reference: System.Linq, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Reference: System.Memory, Version=9.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51
[assembly: System.Runtime.CompilerServices.CompilationRelaxations(8)]
[assembly: System.Runtime.CompilerServices.RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: System.Runtime.Versioning.TargetFramework(".NETCoreApp,Version=v9.0", FrameworkDisplayName = ".NET 9.0")]
[assembly: System.Reflection.AssemblyMetadata("IsTrimmable", "True")]
[assembly: System.Reflection.AssemblyCompany("Philipp Naused")]
[assembly: System.Reflection.AssemblyCopyright("© Philipp Naused")]
[assembly: System.Reflection.AssemblyDescription("High performance binary serialization for C#")]
[assembly: System.Reflection.AssemblyProduct("Bshox")]
[assembly: System.Reflection.AssemblyTitle("Bshox.Utils")]
[assembly: System.Reflection.AssemblyMetadata("RepositoryUrl", "https://github.com/PhilippNaused/Bshox")]
[assembly: System.Resources.NeutralResourcesLanguage("en-US")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Bshox.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100dd5aaf9dfcf30dc5f78e30f2ccbd27f7dc88c9a8db26eda6ed229b883fd34edbdcffce799b053db93a3c4d288e976266f67acb9cd2fa5d24c3642e5b8191d53aebe1954a64512f7d9e992eeb779d011e2c25b4b76d8cacde8f0c675c8093a2f4b8eaafbf6ff24e271d502b023c5f5f2afced11ed447be096d332d3f8c4f70fcd")]
[assembly: System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.RequestMinimum, SkipVerification = true)]
[module: System.Security.UnverifiableCode]
[module: System.Runtime.CompilerServices.RefSafetyRules(11)]
namespace Bshox.Utils
{
    public sealed class BshoxArray : Bshox.Utils.BshoxValue, System.Collections.Generic.IList<Bshox.Utils.BshoxValue>, System.Collections.Generic.ICollection<Bshox.Utils.BshoxValue>, System.Collections.Generic.IEnumerable<Bshox.Utils.BshoxValue>, System.Collections.IEnumerable
    {
        public BshoxArray(int capacity);
        public BshoxArray();
        public int Count { get; }
        public Bshox.BshoxCode? ElementEncoding { get; }
        public Bshox.Utils.BshoxValue this[int index] { get; set; }
        public void Add(Bshox.Utils.BshoxValue item);
        public void Clear();
        public bool Contains(Bshox.Utils.BshoxValue item);
        public System.Collections.Generic.IEnumerator<Bshox.Utils.BshoxValue> GetEnumerator();
        public int IndexOf(Bshox.Utils.BshoxValue item);
        public void Insert(int index, Bshox.Utils.BshoxValue item);
        public static Bshox.Utils.BshoxArray Read(ref Bshox.BshoxReader reader);
        public bool Remove(Bshox.Utils.BshoxValue item);
        public void RemoveAt(int index);
        public override void Write(ref Bshox.BshoxWriter writer);
    }
    public sealed class BshoxBlob : Bshox.Utils.BshoxValue
    {
        public BshoxBlob(byte[] Data);
        public BshoxBlob(string utf8String);
        public byte[] Data { get; set; }
        public string AsHexString();
        public string AsUtf8String();
        public static Bshox.Utils.BshoxBlob Read(ref Bshox.BshoxReader reader);
        public override void Write(ref Bshox.BshoxWriter writer);
    }
    public sealed class BshoxObject : Bshox.Utils.BshoxValue, System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<uint, Bshox.Utils.BshoxValue>>, System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<uint, Bshox.Utils.BshoxValue>>, System.Collections.IEnumerable
    {
        public BshoxObject();
        public int Count { get; }
        public Bshox.Utils.BshoxValue this[uint key] { get; }
        public void Add(uint key, Bshox.Utils.BshoxValue value);
        public void Clear();
        public bool ContainsKey(uint key);
        public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<uint, Bshox.Utils.BshoxValue>> GetEnumerator();
        public static Bshox.Utils.BshoxObject Read(ref Bshox.BshoxReader reader);
        public bool Remove(uint key);
        public bool TryGetValue(uint key, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out Bshox.Utils.BshoxValue? value);
        public override void Write(ref Bshox.BshoxWriter writer);
    }
    [System.Serializable]
    public sealed class BshoxParserException : Bshox.BshoxException
    {
        public (int Line, int Column)? Position { get; }
        public string? Token { get; }
        [System.Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId = "SYSLIB0051", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context);
    }
    public class BshoxTextParser
    {
        public static Bshox.Utils.BshoxValue Parse(string text);
    }
    public abstract class BshoxValue
    {
        public Bshox.BshoxCode Encoding { get; }
        public static Bshox.Utils.BshoxValue Read(ref Bshox.BshoxReader reader, Bshox.BshoxCode encoding);
        public override string ToString();
        public abstract void Write(ref Bshox.BshoxWriter writer);
    }
    public static class Extensions
    {
        public static string ToBshoxString<T>(this Bshox.BshoxContract<T> contract, scoped in T value);
        public static Bshox.Utils.BshoxValue ToBshoxValue<T>(this Bshox.BshoxContract<T> contract, scoped in T value);
    }
    public sealed class Fixed4 : Bshox.Utils.BshoxValue
    {
        public Fixed4(float Value);
        public float Value { get; set; }
        public static Bshox.Utils.Fixed4 Read(ref Bshox.BshoxReader reader);
        public override string ToString();
        public override void Write(ref Bshox.BshoxWriter writer);
    }
    public sealed class Fixed8 : Bshox.Utils.BshoxValue
    {
        public Fixed8(double Value);
        public double Value { get; set; }
        public static Bshox.Utils.Fixed8 Read(ref Bshox.BshoxReader reader);
        public override string ToString();
        public override void Write(ref Bshox.BshoxWriter writer);
    }
    public sealed class VarInt : Bshox.Utils.BshoxValue
    {
        public VarInt(ulong Value);
        public VarInt(int value);
        public ulong Value { get; set; }
        public static Bshox.Utils.VarInt Read(ref Bshox.BshoxReader reader);
        public override string ToString();
        public override void Write(ref Bshox.BshoxWriter writer);
    }
}
