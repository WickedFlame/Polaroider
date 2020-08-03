---
title: Snapshot testing
layout: "default"
nav_order: 2
---
## Create Snapshot
Polaroider automaticaly saves the Snapshots to the folder _Snapshots
```csharp
public class MyClass
{
    [Nunit.Framwork.Test]
    public void TestSomething()
    {
        "This is a test snapshot".MatchSnapshot();
    }
}
```

## Update Snapshot
The simplest way to update a Snapshot is to delete the Sanpshot file from the folder _Snapshots. Run the test again and the Snapshot is recreated.
Alternatively just add the UpdateSnapshotAttribute on the Method
```csharp
public class MyClass
{
    [Nunit.Framwork.Test]
    [UpdateSnapshot]
    public void TestSomething()
    {
        "This is a test snapshot".MatchSnapshot();
    }
}
```

Adding the UpdateSnapshotAttribute on the class updates all snapshots of the class
```csharp
[UpdateSnapshot]
public class MyClass
{
    [Nunit.Framwork.Test]
    public void TestSomething()
    {
        "This is a test snapshot".MatchSnapshot();
    }
}
```

## Multiple Snapshots per test
Snapshots are saved by Class- and Methodname. If it is needed to save multiple Snapshots in a test, just provide some metadata with the snapshot. The snapshots can then be identified by that metadata.
```csharp
public class MyClass
{
    [Nunit.Framwork.Test]
    public void TestSomething()
    {
        "This is a test snapshot".MatchSnapshot(() => new { id = "one"});

        "This is another test snapshot".MatchSnapshot(() => new { id = "two"});
    }
}
```

### Get Snapshot by metadata
Snapshots can contain metadata. The snapshot is compared with the saved snapshot that contains the same metadata.
It is not needed to provide all metadata contained on the saved snapshot, only the data needed to identify the saved snapshot.

The following code matches the snapshot that contains the Key "id" with the value "one" in the metadata
```csharp
public class MyClass
{
    [Nunit.Framwork.Test]
    public void TestSomething()
    {
        "This is a test snapshot".MatchSnapshot(() => new { id = "one"});
    }
}
```

## Snapshotting objects
Objects are mapped by property and value. Hirarchie is displayed by indentation
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

### Parse lines for custom output
```csharp
sn = new StringBuilder()
    .AppendLine("Line    1")
    .AppendLine("   Line 2")
    .AppendLine("  Line     3")
    .ToString();

options = SnapshotOptions.Create(o =>
{
    // read lines without whitespaces
    o.SetParser(line => line.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase));
});

sn.MatchSnapshot(options);
```

### Alter the comparer
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
