# Polaroider
Automated Snapshottesting for .NET

[![Build status](https://img.shields.io/appveyor/build/chriswalpen/polaroider/master?label=Master&logo=appveyor&style=for-the-badge)](https://ci.appveyor.com/project/chriswalpen/polaroider/branch/master)
[![Build status](https://img.shields.io/appveyor/build/chriswalpen/polaroider/dev?label=Dev&logo=appveyor&style=for-the-badge)](https://ci.appveyor.com/project/chriswalpen/polaroider/branch/dev)
  
[![NuGet Version](https://img.shields.io/nuget/v/polaroider.svg?style=for-the-badge&label=Latest)](https://www.nuget.org/packages/polaroider/)
[![NuGet Version](https://img.shields.io/nuget/vpre/polaroider.svg?style=for-the-badge&label=RC)](https://www.nuget.org/packages/polaroider/)
  
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=WickedFlame_Polaroider&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=WickedFlame_Polaroider)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=WickedFlame_Polaroider&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=WickedFlame_Polaroider)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=WickedFlame_Polaroider&metric=coverage)](https://sonarcloud.io/summary/new_code?id=WickedFlame_Polaroider)
  
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/34983ecbd3dc41bea645f6e255505016)](https://www.codacy.com/gh/WickedFlame/Polaroider/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=WickedFlame/Polaroider&amp;utm_campaign=Badge_Grade)
  
  
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
- **Sanpshots ensure the state of objects.** Allway ensure the atomic state of obejcts. Any changes to the data contained in the properties is automatically validated and asserted.
- **Simplify comparing big strings.** Just create a snapshot of the string and ensure it does not change in any future testrun.
  
Visit [https://wickedflame.github.io/Polaroider/](https://wickedflame.github.io/Polaroider/) for the full documentation.
  
## Troubleshooting
When having trouble generating Snapshots or the **TestMethodNotFoundException** is thrown, please make sure that the option for Optimize code is disabled and *.pdb files are generated. 
For more information visit [https://wickedflame.github.io/Polaroider/troubleshooting](https://wickedflame.github.io/Polaroider/troubleshooting)
  
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

