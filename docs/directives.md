---
title: Customizing snapshot creation
layout: "default"
nav_order: 2
---
## Customizing Snapshot creation
To customize the creation of snapshots, a Directive can be added to the Parser used in Polaroider.  
Directives are added through the SnapshotOptions.

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

## Examples

### Add a directive to alter the value that is compared
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
