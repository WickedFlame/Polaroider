Polaroider
---

Automated Snapshottesting for .NET

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

