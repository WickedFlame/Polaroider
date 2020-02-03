# Polaroider

Automated Snapshottesting for .NET

- [GitHub](https://github.com/WickedFlame/Polaroider)
- [Changelog](changelog)

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

## Multiple Snapshots per test

### Get explicite Snapshot
Snapshots can contain metadata. The snapshot is compared with the saved snapshot that contains the same metadata.
It is not needed to provide all metadata contained on the saved snapshot, only the data needed to identify the saved snapshot.
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

