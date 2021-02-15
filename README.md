# PlugInLoader
A simple .NET plugin loader for dynamically loading extension to the current app domain from the file system.

## How to use 

```cs
 IPlugInLoader loader = new PlugInLoader();

var plugInCollection = loader.FromDirectory(".\\")
    .Where(c 
        => c.MatchesName("Norika.*Samples.dll"))
    .LookUp();

IList<MyPlugIn> plugIns = plugInCollection.LoadAll<IMyPlugIn>();
```

Just add the assembly attribute to all your plug in assemblies: 

```cs
[assembly: PlugInAssembly("SamplePlugIn")]
```

Afterwards you can load all of your plugins by specifying their types with `plugInCollection.LoadAll<IMyPlugIn>()`.

## Build 

This project is built by azure devops build pipelines and analysed by sonar.

[![Build Status](https://dev.azure.com/NorikaDE/PlugInLoader/_apis/build/status/NorikaDE.PlugInLoader?branchName=main)](https://dev.azure.com/PlugInLoader/_build/latest?definitionId=10&branchName=main)
[![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/NorikaDE/PlugInLoader/10)](https://dev.azure.com/NorikaDE/PlugInLoader/_build?definitionId=10)
![Sonar Quality Gate](https://img.shields.io/sonar/quality_gate/NorikaDE_PlugInLoader?server=https%3A%2F%2Fsonarcloud.io)
![Sonar Violations (long format)](https://img.shields.io/sonar/violations/NorikaDE_PlugInLoader?format=long&server=https%3A%2F%2Fsonarcloud.io)
[![Azure DevOps tests (compact)](https://img.shields.io/azure-devops/tests/NorikaDE/PlugInLoader/10?compact_message)](https://dev.azure.com/NorikaDE/PlugInLoader/_build?definitionId=10)
[![Nuget](https://img.shields.io/nuget/v/Norika.PlugInLoader.Abstractions)](https://www.nuget.org/packages/Norika.PlugInLoader)
[![Nuget](https://img.shields.io/nuget/v/Norika.PlugInLoader.Abstractions)](https://www.nuget.org/packages/Norika.PlugInLoader.Abstractions)
