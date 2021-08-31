---
title: Creating formatters
layout: "default"
nav_order: 2
---
## Using formatters to mock objects
If a object being snapshotted has some properties with data that changes with each snapshot, it is possible to create a formatter that returns a constant value that represents the data of the type.  
This can be the case with DateTimes or Guids that are timebased or generated with each object creation.  
  
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
There are some builtin formatters for mocking objects.  
| Method | Description | Result | 
| --- | --- | --- | 
| MockDateTimes | Format all DateTimes to a value that always matches | 0000-00-00T00:00:00.0000 | 
| MockGuids | Format all Guids to a value that always matches | 00000000-0000-0000-0000-000000000000 | 
  
Mocking objects is configured in the SnapshotOptions.
```csharp
// mock all occurrences of DateTimes to a constant value
var options = SnapshotOptions.Create(o => o.MockDateTimes());
```
