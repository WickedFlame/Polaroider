---
title: Directives
layout: "default"
nav_order: 2
---

## Directives
Directives are used to customize the resulting string of an already tokenized snapshot.  
Each line of the tokenized snapshot is passed to the dirvective.  
The simmplest directive has an inputstring and returns the altered string.
```csharp
SnapshotOptions AddDirective(Func<string, string> directive)
```

```csharp
// ignore all whitespaces by removing them
SnapshotOptions.Default.AddDirective(line => line.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase));
```

It is possible to add multiple directives.  
Directives are applied in the order that they are added.
```csharp
// ignore all whitespaces by removing them
SnapshotOptions.Default.AddDirective(line => line.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase));
// case insensitive comparison by converting all to uppercase
SnapshotOptions.Default.AddDirective(line => line.ToUpper());
```

### Extensions
There are some Stringextensions that can be applied to the directive

| Method | Description | 
|----|----| 
| ReplaceRegex | Replaces all parts of the string that apply to the regex | 
| ReplaceGuid | Replaces all Guids with the provided alternative. Defaults to 00000000-0000-0000-0000-000000000000 | 
| ReplaceDateTime | Replaces all DateTimes that are formatted to ISO 8601 with the provided alternative. Defaults to 0000-00-00T00:00:00.0000 | 

#### ReplaceRegex
Use a regex to replace a string 
```csharp
// replace a ISO 8601 TimeStamp with a constant string
var regex = "[0-9]{1,4}-[0-9]{1,2}-[0-9]{1,2}T[0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}.[0-9]{1,7}\\+[0-9]{1,2}:[0-9]{1,2}";
SnapshotOptions.Default.AddDirective(line => line.ReplaceRegex(regex, "0000-00-00T00:00:00.0000"));
```
#### ReplaceGuid
Replace a Guid with another value.  
This can be used when an object has a Guid Id that is generated on objectcreation and each object gets a new Id.  
```csharp
// replace a Guid with a constant string
SnapshotOptions.Default.AddDirective(line => line.ReplaceGuid("00000000-0000-0000-0000-000000000000"));
```
#### ReplaceDateTime
Replace a DateTime formatted in the ISO 8601 format with another value.  
This can be used when an object has a DateTime that changes with each compare.  
```csharp
// replace a Guid with a constant string
SnapshotOptions.Default.AddDirective(line => line.ReplaceDateTime("0000-00-00T00:00:00.0000"));
```

### Examples
```csharp
var options = SnapshotOptions.Create(o =>
{
    // remove all whitespaces
    o.AddDirective(line => line.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase));
    // alter all to uppercase
    o.AddDirective(line => line.ToUpper());
});

sn = new StringBuilder()
    .AppendLine("Line    1")
    .AppendLine("   Line 2")
    .AppendLine("  Line     3")
    .ToString();

sn.MatchSnapshot(options);
```
