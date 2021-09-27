---
title: Creating formatters
layout: "default"
nav_order: 2
---
## Formatters
Value formatters are used similarly to Mappers, but output a direct string instead of writing to the context. The default objectmapper uses formatters to convert some predefined types to a string. 
Formatters can be added as an expression or as a implementation of IValueFormatter.
  
### Expression
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
  
### IValueFormatter
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
  
Add the formatter to the options
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

