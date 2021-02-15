# PlugInLoader
A simple .NET plugin loader for dynamically loading extension to the current app domain from the file system.

## How to use 

```cs
 IPlugInLoader loader = new PlugInLoader();

var plugInCollection = loader.FromDirectory(".\")
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
