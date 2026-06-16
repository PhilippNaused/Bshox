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

## Dictionaries

```cs
System.Collections.Concurrent.ConcurrentDictionary<TKey, TValue>
System.Collections.Generic.Dictionary<TKey, TValue>
System.Collections.Generic.IDictionary<TKey, TValue>
System.Collections.Generic.IReadOnlyDictionary<,>
System.Collections.Generic.SortedDictionary<,>
System.Collections.ObjectModel.ReadOnlyDictionary<,>
```

## Collections

```cs
T[] // one-dimensional arrays
System.Collections.Concurrent.BlockingCollection<>
System.Collections.Concurrent.ConcurrentBag<>
System.Collections.Concurrent.ConcurrentQueue<>
System.Collections.Concurrent.ConcurrentStack<>
System.Collections.Generic.HashSet<>
System.Collections.Generic.ICollection<>
System.Collections.Generic.IList<T>
System.Collections.Generic.List<T>
System.Collections.Generic.Queue<>
System.Collections.Generic.Stack<>
System.Collections.ObjectModel.Collection<>
```

## Other

```cs
byte[] // This type has special handling. It is not serialized as an array.
decimal
string
System.DateTime
System.Guid
System.Nullable<T>
System.TimeSpan
System.ValueTuple<...> // supported with 1-8 type parameters
```
