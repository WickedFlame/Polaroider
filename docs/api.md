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

### Identitfy Snapshot by metadata
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
