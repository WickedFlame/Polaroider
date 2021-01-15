---
title: Customizing snapshot comparer
layout: "default"
nav_order: 2
---
## Customizing snapshot comparing
To customize the comparing of snapshots, the comparer can be altered in the SnapshotOptions.

```csharp
SnapshotOptions SetComparer(Func<Line, Line, bool> comparer)
```
 By default Line has an override of Equals. That way Lines can be compared directly with each other.

```csharp
SnapshotOptions.Default.SetComparer((newline, savedline) => newline.Equals(savedline));
```


## Examples
Ignore all whitespaces when comparing the lines with each other

```csharp
var sn = new StringBuilder()
    .AppendLine("Line    1")
    .AppendLine("   Line 2")
    .AppendLine("  Line     3")
    .ToString();

var options = SnapshotOptions.Create(o =>
{
    // ignore whitespaces when comparing
    o.SetComparer((newline, savedline) => newline.Value.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase).Equals(savedline.Value.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase)));
});

sn.MatchSnapshot(options);
```
