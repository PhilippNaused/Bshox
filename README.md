# Bshox

[![GitHub Release](https://img.shields.io/github/v/release/PhilippNaused/Bshox?include_prereleases)](https://github.com/PhilippNaused/Bshox/pkgs/nuget/Bshox)
[![Test Coverage](/docs/coverage/badge_linecoverage.svg)](/docs/coverage/SummaryGithub.md)
[![License](https://img.shields.io/github/license/PhilippNaused/Bshox)](/LICENSE)

Bshox is a binary serialization framework for C# optimized for speed and AOT/Trim compatibility.\
It uses a similar syntax existing frameworks like `protobuf-net` or `MessagePack`, but instead of using Reflection, Bshox relies **exclusively** on compile-time code generation for maximum performance and minimal overhead.

## Features

- Higher performance and lower memory usage compared to other frameworks (See: [benchmarks](/docs/benchmarks/results/))
- .NET Standard 2.0 compatible
- Full AOT and Trim support
- **No** reflection

## Basic usage

### 1. Decorate the type you want to serialize

This uses the same syntax as protobuf-net.
Every type without built-in support must be marked as `BshoxContract`, and the serialized members as `BshoxMember(id)`. The member ids must be positive and unique.

```cs
[BshoxContract]
class MyType
{
    [BshoxMember(1)]
    public int Id { get; set; }
    [BshoxMember(2)]
    public float[]? Values { get; set; }
    [BshoxMember(3)]
    public DateTime CreatedAt { get; set; }
}
```

### 2. Create a serializer type

A partial type marked `BshoxSerializer(params types)` will contain the logic required to serialize/deserialize the types you specified. Since the Bshox code generator will write that code for you, there is normally no need to add a body.

```cs
[BshoxSerializer(typeof(MyType))]
partial class MySerializer;
```

### 3. Serialize the data

The serializer type has a static Property for every type you specified which contains a `Serialize` method.

```cs
var obj = new MyType
{
    Id = 7,
    Values = [1.1f, 2.2f, 3.3f],
    CreatedAt = DateTime.UtcNow
};
// write to byte array
byte[] array = MySerializer.MyType.Serialize(obj);
// write to stream
using var stream = new MemoryStream();
MySerializer.MyType.Serialize(stream, obj);
```

### 4. Deserialize the data

Deserializing works the same as serializing.

```cs
// read from byte array
byte[] data = File.ReadAllBytes("data.bin");
var obj = MySerializer.MyType.Deserialize(data);
// read from stream
using var stream = File.OpenRead("data.bin");
obj = MySerializer.MyType.Deserialize(stream);
stream.Position = 0;
// read from stream asynchronously
obj = await MySerializer.MyType.DeserializeAsync(stream);
```

## Notes

Member IDs in Bshox function very similar to those in protobuf

- Member IDs must be unique and in the range from 1 to 536870911
- Smaller IDs result in smaller binary data
- Members can be added and removed without breaking compatibility

## Limitation

Here is what Bshox cannot do:

- Reentrancy
- Async serialization/deserialization\
  The async overload of the Deserialize method simplify buffers the stream asynchronously. The deserialization itself is pure CPU-bound work.
- Polymorphism (coming soon™)
- By-ref serialization (maybe in the future)
