# Polaroider
Automated Snapshottesting for .NET

| Environment | Branch | State |
|---|---|---|
| Travis-ci | Master | [![Build Status](https://travis-ci.org/WickedFlame/Polaroider.svg?branch=master)](https://travis-ci.org/WickedFlame/Polaroider) |
| Appveyor | Master | [![Build status](https://ci.appveyor.com/api/projects/status/3v8mpq0p35vlegda/branch/master?svg=true)](https://ci.appveyor.com/project/chriswalpen/polaroider) |
| Appveyor | dev | [![Build status](https://ci.appveyor.com/api/projects/status/3v8mpq0p35vlegda/branch/dev?svg=true)](https://ci.appveyor.com/project/chriswalpen/polaroider) |
| Nuget | Master | [![NuGet Version](https://img.shields.io/nuget/v/polaroider.svg?style=flat)](https://www.nuget.org/packages/polaroider/) |

Simplify UnitTesting with snapshots.
Polaroider is a Approval Testing Framework that creates and compares snapshots of objects

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