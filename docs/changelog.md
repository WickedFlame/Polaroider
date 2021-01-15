---
title: Changelog
layout: "default"
nav_order: 99
---

## Polaroider Changelog
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