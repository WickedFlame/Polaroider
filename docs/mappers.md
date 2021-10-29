---
title: Mappers
layout: "default"
nav_order: 2
---
## Mappers

Mappers are used to define how a object is transformed to a snapshot.  
Mappers are added to the Options that are used for the snapshotcreation.  
All output is added to the snaphsot through the context.
  
Polaroider checks the type of each object to try and match a registered mapper.  
If no mapper is registered, Polaroider uses the default mapping startegy to map objects to the snapshot.
```csharp
var options = SnapshotOptions.Create(o =>
{
    o.AddMapper<CustomData>((ctx, itm) =>
    {
        // Map the Id to the snaphsot
        ctx.AddLine("Id", itm.Value);
        // Map the Value to the property OuterValue
        ctx.AddLine("OuterValue", itm.Value);
        // All other properties are ignored
    });
});

var item = new
{
    Item = "item",
    Data = new CustomData
    {
        Id = 1,
        Dbl = 2.2,
        Value = "value",
        Inner = new InnerData
        {
            Id = 2,
            Value = "inner"
        }
    }
}.MatchSnapshot(options);
```
  
This results in a snapshot where the CustomData object should be as following
```
Item: item
Data: 
  Id: 1
  OuterValue: value
```
When mapping the Data property, Polaroider uses the registered mapper to map the CustomData object.
  
### Map nested objects
The Map(string, object) method on the context allows adding nested objects to the snapshot.  

```csharp
var options = SnapshotOptions.Create(o =>
{
    o.AddMapper<CustomData>((ctx, itm) =>
    {
        // Map the Id to the snaphsot
        ctx.AddLine("Id", itm.Value);
        // Map the Value to the property OuterValue
        ctx.AddLine("OuterValue", itm.Value);
        // Let the default mapper map the inner object
        ctx.Map("InnerObject", itm.Inner);

        // All other properties are ignored
    });
});

var item = new
{
    Item = "item",
    Data = new CustomData
    {
        Id = 1,
        Dbl = 2.2,
        Value = "value",
        Inner = new InnerData
        {
            Id = 2,
            Value = "inner"
        }
    }
}.MatchSnapshot(options);
```
  
The InnerData object is mapped using the default mappingstrategy of Polaroider.  
This results in a snapshot where the InnerData object is mapped with all properties.
```
Item: item
Data: 
  Id: 1
  OuterValue: value
  InnerObject:
    Id: 2
    Value: inner
```

### Custom mapper for nested objects
The Map method checks the registered mappers before using the default mappingstrategy.  

```csharp
var options = SnapshotOptions.Create(o =>
{
    o.AddMapper<InnerData>((ctx, itm) => {
        // Map the Id to the snaphsot
        ctx.AddLine("Id", itm.Value);
    });
    o.AddMapper<CustomData>((ctx, itm) =>
    {
        // Map the Id to the snaphsot
        ctx.AddLine("Id", itm.Value);
        // Map the Value to the property OuterValue
        ctx.AddLine("OuterValue", itm.Value);
        // Let the default mapper map the inner object
        ctx.Map("InnerObject", itm.Inner);

        // All other properties are ignored
    });
});

var item = new
{
    Item = "item",
    Data = new CustomData
    {
        Id = 1,
        Dbl = 2.2,
        Value = "value",
        Inner = new InnerData
        {
            Id = 2,
            Value = "inner"
        }
    }
}.MatchSnapshot(options);
```
  
This results in a snapshot where the InnerData object should be as following
```
Item: item
Data: 
  Id: 1
  OuterValue: value
  InnerObject:
    Id: 2
```