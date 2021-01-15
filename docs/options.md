---
title: Options
layout: "default"
nav_order: 2
---
## Snapshot Options

### Setup SnapshotOptions for global scope
```csharp
SnapshotOptions.Setup(o =>
{
    // read lines without whitespaces
    o.AddDirective(line => line.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase));
});

sn.MatchSnapshot();
```

### Custom SnapshotOptions per Match
```csharp
var options = new SnapshotOptions();
// read lines without whitespaces
options.AddDirective(line => line.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase));

sn.MatchSnapshot(options);
```

Or by using the options factory
```csharp
var options = SnapshotOptions.Create(o =>
{
    // read lines without whitespaces
    o.SetParser(line => line.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase));
});

sn.MatchSnapshot(options);
```

Global options are merged when using a custom option object. 

If the Parser is altered or a directive is added in the custom options, the parsersettings of the default options will be ignored.