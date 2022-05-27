---
title: Troubleshooting
layout: "default"
nav_order: 2
---
## Troubleshooting
Polaroider stores the snapshots in the folder _Snapshots at the sampe place where the testclass is contained. To be able to find the path of the testclass, Polaroider relies on information foind in the pdb files.  
Make sure to enable pdb files when compiling.  
  
By default pdb files are not generated when using Live Unit Testing. These can be activated when enabling the checkbox for 'Enable debug symbol and xml documentation comment generation' in the Visual Studio Options.