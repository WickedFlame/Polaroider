---
layout: "default"
---
# Polaroider

Automated Snapshottesting made simple

Simplify UnitTesting with snapshots.
Polaroider is a Approval Testing Framework that creates and compares snapshots of almost anything

### Timeconsuming assertion testing
Conventional assertion testing needs multiple assertion checks to test and verify all properties of an object
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

### Fast and easy approval testing with Polaroider
Polaroider reduces all assertions to just one call. 
Snapshottesting keeps the code simple, clean and readable.
```csharp
// arrange
var repository = new PersonRepository();

// act
var person = repository.LoadTesPerson(...);

// assert
person.MatchSnapshot();
```

- [GitHub](https://github.com/WickedFlame/Polaroider)
- [Changelog](changelog)