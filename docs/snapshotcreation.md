---
title: Customizing snapshot
layout: "default"
nav_order: 2
---
## Snapshot creation
When creating a snapshot, Polaroider uses the DefaultMapper to map objects.  
The DefaultMapper uses the [SnapshotTokenizer](#SnapshotTokenizer) to tokenize objects.  
When tokenizing, the DefaultMapper checks for registered [Mappers](mappers) or uses the default Mappingstrategy to create a snapshot.  
The default Mappinstrategy casts all [ValueTypes](#valuetype-matching) to string and maps the value to the snapshot.  
  
### <a name="SnapshotTokenizer"></a>SnapshotTokenizer
The SnapshotTokenizer uses already configured mappers to create snapshottokens of objects or creates tokens based on strings.
```csharp
ObjectMapper.Configure<CustomClass>(m =>
{
    var token = SnapshotTokenizer.Tokenize(m.Value);
    return token;
});
```
  
### Mappingstrategy
The default Mappingstrategy loops over all properties on a object. If a Propertytype corresponds to the definition of a [ValueType](#valuetype-matching), the property is cast to string.  
  
### <a name="valuetype-matching"></a> ValueType matching
Polaroider uses the ToString() method on ValueTypes to create the snapshot.  
The default definition of a ValueType can be altered in the SnapshotOptions. In the Options use the EvaluateValueType method to override the behaviour for matching valuetypes.
```charp
var options = SnapshotOptions.Create(o =>
{
    // exclude generics from the ValueTypes matching
    o.EvaluateValueType((type, obj) => type.IsValueType && !type.IsGenericType);
});
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
