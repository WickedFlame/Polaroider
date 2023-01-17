---
title: Troubleshooting
layout: "default"
nav_order: 2
---
## Troubleshooting
Polaroider stores the snapshots in the folder _Snapshots at the sampe place where the testclass is contained.  
  
To be able to find the path of the testclass, Polaroider relies on some information to be generated when compiling.  
  
### PDB Files
Make sure to enable *.pdb files when compiling.  
By default *.pdb files are not generated when using Live Unit Testing. These can be activated when enabling the checkbox for 'Enable debug symbol and xml documentation comment generation' in the Visual Studio Options.
  
### Optimize code
By default Visual Studio uses Optimize code when compiling with Release configuration.  
Polaroider needs the correct StackTrace to find the Testmethods.  
When using Optimized code, the StackTrace can be changed and Polaroider may not find the Testmethod.  
Optimized code can be disabled int the properties of the project with the checkbox 'Optimize code' in the Build section or directly in the properties of the *.csproj file.
  
```csharp
<PropertyGroup>
    <Optimize>False</Optimize>
</PropertyGroup>
```
  
### TestMethodNotFoundException
When the TestMethodNotFoundException is thrown, please make sure the Optimize code option is set and *.pdb files are generated.
