---
title: Customizing snapshot
layout: "default"
nav_order: 2
---
## Customizing Snapshot creation
There are several way to influence the value of a snapshot.
- [Mappers](#Mappers)
- [ObjectMapper](#ObjectMapper)
- [SnapshotTokenizer](#SnapshotTokenizer)
- [Valuetype-Matching](#valuetype-matching)
  
### <a name="Mappers"></a>Mappers
Mappers are used to define how a complete object is transformed to a snapshot.  
Mappers are added to the Options that are used for the snapshotcreation.  
All output is added to the snaphsot through the context.

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
  
This results in a snapshot where the CustomData object should be as following
```
Item: item
Data: 
  Id: 1
  OuterValue: value
  InnerObject:
    Id: 2
    Value: inner
```

### <a name="ObjectMapper"></a>ObjectMapper
The default ObjectMapper can be replaced per object.  
Use ObjectMapper.Configure to define a custom mapping of objects to snapshots
```csharp
ObjectMapper.Configure<CustomClass>(m =>
{
    // create snapshot object and add the lines
    var token = new Snapshot()
        .Add(m.Value);
    return token;
});
```

### <a name="SnapshotTokenizer"></a>SnapshotTokenizer
The SnapshotTokenizer uses already configured mappers to create snapshottokens of objects or creates tokens based on strings.
```csharp
ObjectMapper.Configure<CustomClass>(m =>
{
    var token = SnapshotTokenizer.Tokenize(m.Value);
    return token;
});
```
  
### <a name="valuetype-matching"></a> Valuetype matching
Polaroider uses the ToString() method on ValueTypes to create the snapshot.  
The default definition of a ValueType can be altered in the SnapshotOptions. In the Options use the EvaluateValueType method to override the behaviour for matching valuetypes.
```charp
var options = SnapshotOptions.Create(o =>
{
    // exclude generics from the ValueTypes matching
    o.EvaluateValueType((type, obj) => type.IsValueType && !type.IsGenericType);
});
```