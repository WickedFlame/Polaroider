---
layout: "default"
---
# Polaroider

Automated Snapshottesting made simple  
  
Simplify UnitTesting with snapshots.  
Polaroider is a Approval Testing Framework that creates and compares snapshots of almost anything  

### Common, timeconsuming assertion testing
Conventional assertion testing needs multiple assertion checks to test and verify all properties of an object.  
The tests have to be updated every time a new property is added.  
  
```csharp
// arrange
var repository = new PersonRepository();

// act
var person = repository.LoadTestPerson(...);

// assert
Assert.IsEqual(person.Firstname, "Chris");
Assert.IsEqual(person.Lastname, "Walpen");
Assert.IsEqual(person.Company, "WickedFlame");
Assert.IsEqual(person.Address.Street, "Teststreet");
Assert.IsEqual(person.Address.Streetnumber, 3);
```
  
### Fast and easy approval testing with Polaroider
**Polaroider reduces all assertions to just one call.**  
**Snapshottesting keeps the code simple, clean and readable.**  
  
```csharp
// arrange
var repository = new PersonRepository();

// act
var person = repository.LoadTestPerson(...);

// assert
person.MatchSnapshot();
```
  
- [GitHub](https://github.com/WickedFlame/Polaroider)
- [Changelog](changelog)

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