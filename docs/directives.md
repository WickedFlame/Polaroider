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
- [ValueFormatters](#ValueFormatters)
- [Directives](#Directives)
- [Valutype-Matching](#valutype-matching)
  
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

This results in a snapshot that should look as following
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

### <a name="ValueFormatters"></a>ValueFormatters
Value formatters are used similarly but output a direct string instead of writing to the context.  
Formatters can be added as an expression or as a implementation of IValueFormatter.
#### Expression
```csharp
var options = SnapshotOptions.Create(o =>
{
    o.AddFormatter<double>(value => ((int) value).ToString());
});

new
{
    Value = 2.2
}.MatchSnapshot(options);
```
This results in the following snapshotvalue
```
Value: 2
```
#### Implementation of IValueFormatter
```csharp
public class DateTimeFormatter : IValueFormatter
{
    public string Format(object value)
    {
        if (value is DateTime dte)
        {
            return dte.ToString("o");
        }

        return value?.ToString();
    }
}
```
  
```csharp
var options = SnapshotOptions.Create(o =>
{
    o.AddFormatter(typeof(DateTime), new DateTimeFormatter());
});
new
{
    Value = new DateTime(2012, 12, 21, 12, 21, 21)
}.MatchSnapshot(options);
```
This results in the following snapshotvalue
```
Value: 2012-12-21T12:21:21.0000000
```

### <a name="Directives"></a>Directives
Directives are used to customize the value of an already tokenized snapshot per line.  
The simmplest directive has an inputstring and returns the altered string.
```csharp
SnapshotOptions AddDirective(Func<string, string> directive)
```

```csharp
// ignore all whitespaces by removing them
SnapshotOptions.Default.AddDirective(line => line.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase));
```

It is possible to add multiple directives.  
Directives are applied in the order that they are added.
```csharp
// ignore all whitespaces by removing them
SnapshotOptions.Default.AddDirective(line => line.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase));
// case insensitive comparison by converting all to uppercase
SnapshotOptions.Default.AddDirective(line => line.ToUpper());
```

#### Extensions
There are some Stringextensions that can be applied to the directive

| Method | Description |
|----|----|
| ReplaceRegex | Replaces all parts of the string that apply to the regex |
| ReplaceGuid | Replaces all Guids with the provided alternative. Defaults to 00000000-0000-0000-0000-000000000000 |

#### Examples
```csharp
var options = SnapshotOptions.Create(o =>
{
    // remove all whitespaces
    o.AddDirective(line => line.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase));
    // alter all to uppercase
    o.AddDirective(line => line.ToUpper());
});

sn = new StringBuilder()
    .AppendLine("Line    1")
    .AppendLine("   Line 2")
    .AppendLine("  Line     3")
    .ToString();

sn.MatchSnapshot(options);
```

### <a name="valutype-matching"></a> Valutype matching
Valuetypes are automatically mapped with ToString(). In the Options it is possible override the behaviour for matching valuetypes.
```charp
var options = SnapshotOptions.Create(o =>
{
    // exclude generics from the ValueTypes matching
    o.EvaluateValueType((type, obj) => type.IsValueType && !type.IsGenericType);
});
```