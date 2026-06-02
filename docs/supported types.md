# Types with built-in serialization contracts

This is a list of every Type in the standard library with a built-in serialization contract.

## Primitives

```cs
bool
byte
char
double
float
int
long
sbyte
short
uint
ulong
ushort
```

## Collections

```cs
byte[] // This type has special handling. It is not serialized as an array.
System.Collections.Concurrent.ConcurrentDictionary<TKey, TValue>
System.Collections.Generic.Dictionary<TKey, TValue>
System.Collections.Generic.IDictionary<TKey, TValue>
System.Collections.Generic.IList<T>
System.Collections.Generic.List<T>
System.Collections.Generic.SortedDictionary<,>
T[]
```

## Other

```cs
string
System.DateTime
System.Guid
System.Nullable<T>
System.TimeSpan
System.ValueTuple<...> // supported with 1-8 type parameters
```
