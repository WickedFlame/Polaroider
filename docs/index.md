---
layout: "default"
---
# Polaroider

Automated Snapshottesting made simple for .NET

- [GitHub](https://github.com/WickedFlame/Polaroider)
- [Changelog](changelog)

Simplify UnitTesting with snapshots.
Polaroider is a Approval Testing Framework that creates and compares snapshots of objects

### Assertion testing
Conventional assertion testing needs multiple assertion checks to test all properties of an object
```csharp
// arrange
var repository = new PersonRepository();

// act
var person = repository.LoadTesPerson(...);

// assert
Assert.IsEqual(person.Firstname, "Chris");
Assert.IsEqual(person.Lastname, "Walpen");
Assert.IsEqual(person.Company, "WickedFlame");
Assert.IsEqual(person.Address.Street, "Teststreet");
Assert.IsEqual(person.Address.Streetnumber, 3);
```

### Approval testing
Approval testing reduces all assertions to just one assertion check with the help of Snappshottesting
```csharp
// arrange
var repository = new PersonRepository();

// act
var person = repository.LoadTesPerson(...);

// assert
person.MatchSnapshot();
```

## Supported Testframeworks
- MSTest
- NUnit
- Xunit