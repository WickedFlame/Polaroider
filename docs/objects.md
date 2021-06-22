---
title: Getting started
layout: "default"
nav_order: 3
---
## Snapshotting objects
Objects are mapped by property and value. 
Inside the snapshot hirarchie is displayed by indentation
```csharp
var obj = new {
    id = 1,
    value = new {
        name = "test"
    }
}

obj.MatchSnapshot();
```
The generated snapshot looks the following:
```csharp
id: 1
value:
  name: test
```
