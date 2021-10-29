# Polaroider
Automated Snapshottesting for .NET

[![Build Status](https://img.shields.io/travis/com/WickedFlame/polaroider/master.svg?label=Travis-CI&style=for-the-badge)](https://app.travis-ci.com/github/WickedFlame/polaroider)
[![Build status](https://img.shields.io/appveyor/build/chriswalpen/polaroider/master?label=Master&logo=appveyor&style=for-the-badge)](https://ci.appveyor.com/project/chriswalpen/polaroider/branch/master)
[![Build status](https://img.shields.io/appveyor/build/chriswalpen/polaroider/dev?label=Dev&logo=appveyor&style=for-the-badge)](https://ci.appveyor.com/project/chriswalpen/polaroider/branch/dev)
[![NuGet Version](https://img.shields.io/nuget/v/polaroider.svg?style=for-the-badge&label=Latest)](https://www.nuget.org/packages/polaroider/)
[![NuGet Version](https://img.shields.io/nuget/vpre/polaroider.svg?style=for-the-badge&label=RC)](https://www.nuget.org/packages/polaroider/)

Simplify UnitTesting with snapshots.
Polaroider is a Approval Testing Framework that creates and compares snapshots of objects.  
  
Fast and easy testing of objects

```
// arrange
var repository = new PersonRepository();

// act
var person = repository.LoadTestPerson(...);

// assert
person.MatchSnapshot();
```


Visit [https://wickedflame.github.io/Polaroider/](https://wickedflame.github.io/Polaroider/) for the full documentation.

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
```
SnapshotOptions.Setup(o =>
{
    o.UseBasicFormatters();
});
```

## Upcomming features
- Loading saved Snapshots to access the Metadata
- Add support for MSTest, cUnit, xUnit