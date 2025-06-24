### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|-------
BSHOX001 | Bshox | Error | Bshox serializable types must be partial
BSHOX002 | Bshox | Error | Bshox serializable types must not be nested
BSHOX003 | Bshox | Error | A generated Bshox serializer must have at least one serializable type
BSHOX004 | Bshox | Error | Bshox requires C# 12 or later
BSHOX005 | Bshox | Error | Bshox serializable members must have an explicit key
BSHOX006 | Bshox | Error | Bshox serializable members must have unique keys
BSHOX007 | Bshox | Error | Bshox serializable members with implicit layout must not have an explicit key
BSHOX008 | Bshox | Error | Bshox serializable members must have a valid key
BSHOX009 | Bshox | Error | DefaultValueAttribute must have exactly one constructor argument
BSHOX010 | Bshox | Error | Type is not serializable
BSHOX011 | Bshox | Warning | Surrogate types should have the 'Surrogate' suffix
BSHOX012 | Bshox | Error | Surrogate types must have the required constructor
BSHOX013 | Bshox | Error | Surrogate types must have the required 'Convert' method
BSHOX014 | Bshox | Error | The current compiler version is too low
BSHOX015 | Bshox | Warning | Dispose the return value of the DepthLock() method correctly
BSHOX016 | Bshox | Error | Surrogate types must have the [BshoxSurrogate<T>] attribute
BSHOX017 | Bshox | Error | The specified symbol for the contract is not unique

BSHOX999 | Bshox | Error | Internal Error
