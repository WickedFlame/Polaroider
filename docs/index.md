---
layout: "default"
---
# Polaroider

Automated Snapshottesting for .NET

- [GitHub](https://github.com/WickedFlame/Polaroider)
- [Changelog](changelog)

Polaroider is a Approval Testing Framework that creates and compares snapshots of objects

### Assertion testing
Testing all properties of objects using conventional Assertion testing needs multiple asserts
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
Reduce all to just one assertion check using Snappshottesting
```csharp
// arrange
var repository = new PersonRepository();

// act
var person = repository.LoadTesPerson(...);

// assert
person.MatchSnapshot();
```