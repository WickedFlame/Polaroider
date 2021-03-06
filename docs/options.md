---
title: Options
layout: "default"
nav_order: 2
---
## Snapshot Options
The SnapshotOptions are used to configure the way Polaroider takes snapshots of objects.  
The SnapshotOptions can be configured on a global scope or per snapshot.

### SnapshotOptions for global scope
```csharp
SnapshotOptions.Setup(o =>
{
    // read lines without whitespaces
    o.AddDirective(line => line.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase));
});

sn.MatchSnapshot();
```

### SnapshotOptions per Snapshot
To use the options per Snapshot, simply pass an instance of the desired options to the MatchSnapshot method.  

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
