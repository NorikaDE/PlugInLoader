using System;

namespace Norika.PlugInLoader.Abstractions
{
    public interface IPlugInLoadRequest
    { 
        IPlugInLoadRequest Where(Action<IPlugInLoadConfiguration> configuration);

        IPlugInLoadRequest UseCustomAssemblyMatcher(IPlugInMatcher matcher);

        IPlugInCollection LookUp();
    }
}
