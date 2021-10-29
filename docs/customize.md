---
title: Snapshot customizing
layout: "default"
nav_order: 2
---
## Customizing Snapshots
Objects can't allways be created and compared 1:1 with a snapshot. Some datatypes or datastructures are too complex to be compared or simply don't make sense to be compared as a whole.  
For this purpose Polaroider offers several possibilities to alter the way objects are mapped, formatted or analyzed.  
All customization is done in the SnapshotOptions on a global scope or per match.  
* [Snapshot creation](snapshotcreation)
* [Mappers](mappers) are used to define how objects are mapped to the snapshot  
* [Directives](directives) are used to customize the tokenized output per line  
* [Formatters](formatters) define the string value of a type.  
* [Mocking data](mocking) shows how changing values can be mocked as a constant to ensure snapshots don't fail on values that always change  
