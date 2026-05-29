# Polaroider
Automated Snapshottesting for .NET
| What | Badge |
| --- | --- |
| Build | [![Build status](https://ci.appveyor.com/api/projects/status/3v8mpq0p35vlegda/branch/master?svg=true)](https://ci.appveyor.com/project/chriswalpen/polaroider/branch/master) [![Build status](https://ci.appveyor.com/api/projects/status/3v8mpq0p35vlegda/branch/dev?svg=true)](https://ci.appveyor.com/project/chriswalpen/polaroider/branch/dev) |
| Nuget | [![NuGet Version](https://img.shields.io/nuget/v/polaroider.svg?svg=true&label=Latest)](https://www.nuget.org/packages/polaroider/) [![NuGet Version](https://img.shields.io/nuget/vpre/polaroider.svg?svg=true&label=RC)](https://www.nuget.org/packages/polaroider/) |
| Sonar | [![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=WickedFlame_Polaroider&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=WickedFlame_Polaroider) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=WickedFlame_Polaroider&metric=coverage)](https://sonarcloud.io/summary/new_code?id=WickedFlame_Polaroider)|
| Codacy | [![Codacy Badge](https://app.codacy.com/project/badge/Grade/34983ecbd3dc41bea645f6e255505016)](https://www.codacy.com/gh/WickedFlame/Polaroider/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=WickedFlame/Polaroider&amp;utm_campaign=Badge_Grade) |
  
  
Simplify UnitTesting with snapshots.  
Polaroider is a Approval Testing Framework that creates and compares snapshots of objects.  
This makes testing of objects and their content easy and fast.
  
```csharp
// arrange
var repository = new PersonRepository();

// act
var person = repository.LoadTestPerson(...);

// assert
person.MatchSnapshot();
```
  
## Testing with Snapshots
Snashots help when testing objects or large strings
- **Snapshots ensure that object structure does not change unintendedly.** When adding or removing properties in objects, these are automatically captured and asserted.
- **Sanpshots ensure the state of objects.** Allways ensure the atomic state of obejcts. Any changes to the data contained in the properties of the matched object is automatically validated and asserted.
- **Simplify comparing big strings.** Just create a snapshot of the string and ensure it does not change in any future testrun.
  
Visit [https://wickedflame.github.io/Polaroider/](https://wickedflame.github.io/Polaroider/) for the full documentation.
  
## Troubleshooting
When having trouble generating Snapshots or the **TestMethodNotFoundException** is thrown, please make sure that the option for Optimize code is disabled *.pdb files are generated and that the Test does not depend on async/await. 
For more information visit [https://wickedflame.github.io/Polaroider/troubleshooting](https://wickedflame.github.io/Polaroider/troubleshooting)
  
## Examples
```csharp
[Test]
public void TestPerson()
{
    // act
    var person = repository.LoadTestPerson(...);
    // assert
    person.MatchSnapshot();
}
```
  
### Update Snapshots
Data changes and the snapshot has to be updated. This can be done by adding the UpdateSnapshot attribute to the test method or class.

```csharp
[Test]
[UpdateSnapshot]
public void TestPerson()
{
    // act
    var person = repository.LoadTestPerson(...);
    // assert
    person.MatchSnapshot();
}
```
It is important to remove the attribute after the snapshot is updated. Else the snapshot will be updated in every test run and the test will not fail when the data changes.
  
### Custom Snapshot Name
Usig the SnapshotName attribute you can specify the name of the snapshot for a test method. This is useful to avoid long filenames of snapshots.
```csharp
[SnapshotName("CustomSnapshotName")]
public void TestPerson()
{
    // act
    var person = repository.LoadTestPerson(...);
    // assert
    person.MatchSnapshot();
}
```

  
## Update from v1 to v2
v1 was focused on simplicity. 
v2 is focused on flexibility while maintaining simplicity.
  
There are some breaking changes when updating to v2.
### Mapping of simple types
- DateTimes are mapped in the ISO 8601 format to be Culture-Independent
- Null strings are now displayed as null
- Empty strings are now displayed as ''
  
Before updating to v2 ensure that all tests are executed without errors.
If some tests throw a MismatchException there are two possibilities:
- Update the snapshots with the UpdateSnapshot Attribute
- Set the UseBasicFormatters on the SnapshotOptions. This resets all Formatters to the same as were used in v1
```csharp
SnapshotOptions.Setup(o =>
{
    o.UseBasicFormatters();
});
```

