---
title: Getting started
layout: "default"
nav_order: 3
---
## Snapshotting objects
Objects are mapped by property and value. 
Inside the snapshot hirarchie is displayed by indentation
```csharp
var obj = new {
    id = 1,
    value = new {
        name = "test"
    }
}

obj.MatchSnapshot();
```
The generated snapshot looks the following:
```csharp
id: 1
value:
  name: test
```

### Configure mapping for objects
Use ObjectMapper.Configure to define a custom mapping of objects to snapshots
```csharp
ObjectMapper.Configure<CustomClass>(m =>
{
    // create snapshot object and add the lines
    var token = new Snapshot()
        .Add(m.Value);
    return token;
});
```

#### Using Tokenizer to create a Snapshot object
SnapshotTokenizer uses already configured mappers to create snapshottokens of objects or creates tokens based on strings
```csharp
ObjectMapper.Configure<CustomClass>(m =>
{
    var token = SnapshotTokenizer.Tokenize(m.Value);
    return token;
});
```

### Add a directive to alter the value that is compared
```csharp
sn = new StringBuilder()
    .AppendLine("Line    1")
    .AppendLine("   Line 2")
    .AppendLine("  Line     3")
    .ToString();

options = SnapshotOptions.Create(o =>
{
    // read lines without whitespaces and in uppercase
    o.AddDirective(line => line.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase));
    o.AddDirective(line => line.ToUpper());
});

sn.MatchSnapshot(options);
```

### Alter the way lines are compared
```csharp
sn = new StringBuilder()
    .AppendLine("Line    1")
    .AppendLine("   Line 2")
    .AppendLine("  Line     3")
    .ToString();

options = SnapshotOptions.Create(o =>
{
    // ignore whitespaces when comparing
    o.SetComparer((newline, savedline) => newline.Value.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase).Equals(savedline.Value.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase)));
});

sn.MatchSnapshot(options);
```

### Global SnapshotOptions
```csharp
SnapshotOptions.Setup(o =>
{
    // read lines without whitespaces
    o.SetParser(line => line.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase));
});

sn.MatchSnapshot();
```
