using Norika.PlugInLoader.Abstractions;

namespace Norika.PlugInLoader.Interfaces
{
    internal interface IPlugInFactory
    {
        IPlugIn CreatePlugIn(IAssemblyMetadata assemblyMetadata);
    }
}
