---
title: Changelog
layout: "default"
nav_order: 99
---

## Polaroider Changelog
### 0.3.3
- The length of both snapshots is measured and compared
- Show the exact index and part of the mismatching strings

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