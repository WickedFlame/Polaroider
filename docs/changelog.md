---
title: Changelog
layout: "default"
nav_order: 99
---

## Polaroider Changelog
### 2.0.2
- Mocking Guids also adds a directive to replace string Guids
- Mocking DateTimes also adds a directive to replace ISO 8601 string DateTimes

### 2.0.1
- Mocking dynamic data
- Indexers are ignored when mapping properties
- Map only properties that have getters
- Added Defaultformatter for MethodInfo and NullableDateTime
- Simplified mocking DateTimes and Guids in the Options
- Added directive for replacing DateTimes in the ISO 8601 format

### 2.0.0
- Added Formatters to be able to influence the way a type is parsed
- DateTimes are compared in ISO 8601 format to be Culture independent
- Null strings are displayed as null
- Empty strings are displayed as ''
- Internal refactorings for better usability
- Add custom formatters to the options

### 1.1.5
- Don't map generics as Valuetypes
- Nullable DateTimes are mapped as mull
- Map DateTimes to string with different culture

### 1.1.4
- Directives work for Snapshotting objecs
- Snapshotting properties of Type caused a StackOverflow

### 1.1.3
- Snapshotting IEnumerable did not snapshot child objects

### 1.1.2
- Snapshotting Sub-objects that are null caused an error

### 1.1.0
- Added Options to the fluent API
- Custom line comparer
- Parse lines to customize the output
- Added support for Xunit

### 1.0.3
- Strongnamed the assembly

### 1.0.2
- Update Snapshots with Attributes

### 0.4.0
- The length of both snapshots is measured and compared
- Show the exact part of the mismatching strings in the exceptionmessage
- Internal refactorings

### 0.3.2
- Fix snapshotting objects
- Fix saving multiple snapshots in one file
- Fix updating snapshots in existing file
- Add metadata to object snapshots

### 0.3.1
- Target NetStandard and NetFramework 4.6

### 0.3.0
- Snapshotting objects
- Basic refactoring of Exception
- Added SnapshotResult to SnapshotMatchException

### 0.2.0
- Updated Exceptionmessage to show the missmatching lines in SnapshotMatchException
- Identify Snapshots based on the metadata instead of a Id