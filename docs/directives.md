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
There are some predefined Extensions for strings that can be applied to the directive.

| Method | Description | 
|----|----| 
| ReplaceRegex | Replaces all parts of the string that apply to the regex | 
| ReplaceGuid | Replaces all Guids with the provided alternative. Defaults to 00000000-0000-0000-0000-000000000000 | 
| ReplaceDateTime | Replaces all DateTimes that are formatted to ISO 8601 with the provided alternative. Defaults to 0000-00-00T00:00:00.0000 | 

#### ReplaceRegex
ReplaceRegex is a Extension that accepts a Regex and the stringvalue to replace the findings with. 
```csharp
// replace a ISO 8601 TimeStamp with a constant string
var regex = "[0-9]{1,4}-[0-9]{1,2}-[0-9]{1,2}T[0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}.[0-9]{1,7}\\+[0-9]{1,2}:[0-9]{1,2}";
SnapshotOptions.Default.AddDirective(line => line.ReplaceRegex(regex, "0000-00-00T00:00:00.0000"));
```
#### ReplaceGuid
Guids are often the case of change. And changes allways breake a Snaphottest.  
ReplaceGuid allows to replace a Guid with a constant value.  
This can be used for example when an object has a Guid Id that is generated on creation of the object and each instance gets a new Id.  
```csharp
// replace a Guid with a constant string
SnapshotOptions.Default.AddDirective(line => line.ReplaceGuid("00000000-0000-0000-0000-000000000000"));
```
#### ReplaceDateTime
DateTimes are very bad when trying to take Snapshots. These are almost never the same.  
ReplaceDateTime replaces a DateTime that is formatted in the ISO 8601 format with a constant value.  
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
  
- Remove all whitespaces
- Make all to uppercase
  
This example results in the following Snapshot:
```csharp
LINE1
LINE2
LINE3
```
  
With this I can compare any object/string that is caseinsensitive and ignores whitespacecs.
```csharp
sn = new StringBuilder()
    .AppendLine("line 1")
    .AppendLine("line 2")
    .AppendLine("LINE 3")
    .ToString();
```
The compare with this string will return true