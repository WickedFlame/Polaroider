---
title: Snapshoting dynamic data
layout: "default"
nav_order: 2
---
## Mocking dynamic data
Sometimes objects contain properties that have data like timestamps or hashes that change with each run. That would mean the snapshot would imediately be invalid after generation.  
Polaroider provides several possibilities to handle the parts of data that changes but still test the rest of the object.
  
## Formatter
If the changing data is based on a fixed type, it is possible to create a formatter that returns a constant string that represents the data of the type.
```csharp
var options = SnapshotOptions.Create(o => o.AddFormatter<DateTime>(obj => "0000-00-00T00:00:00.0000"));
sn.MatchSnapshot(options);
```
For more complex structures create a formatter that derives IValueFormatter and add this to the SnapshotOptions.  
```csharp
public class MockDateTimeFormatter : IValueFormatter
{
	public string Format(object value)
	{
		if (value is DateTime)
		{
            // return a constant value instead of the changing datetime
            return "0000-00-00T00:00:00.0000";
		}
		return value?.ToString();
	}
}
```
## Directive
The directives help find a part of the replace string that would match the changing data and convert this to a constant value  
```csharp
// change all occurrences of ISO 8601 DateTimes to a constant value
var options = SnapshotOptions.Create(o => 
  o.AddDirective(line => 
    line.ReplaceRegex("[0-9]{1,4}-[0-9]{1,2}-[0-9]{1,2}T[0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}.[0-9]{1,7}\\+[0-9]{1,2}:[0-9]{1,2}", "0000-00-00T00:00:00.0000")));
sn.MatchSnapshot(options);
```
## Extensions
Currently there is a Builtin formatter for mocking DateTimes.  
This is configured in the SnapshotOptions.
```csharp
// mock all occurrences of DateTimes to a constant value
var options = SnapshotOptions.Create(o => o.MockDateTimes());
```
