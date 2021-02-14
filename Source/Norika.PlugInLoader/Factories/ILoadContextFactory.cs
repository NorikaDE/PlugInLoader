using System.Collections.Generic;
using Norika.PlugInLoader.Interfaces;

namespace Norika.PlugInLoader.Factories
{
    internal interface ILoadContextFactory
    {
        ILoadContext CreateLoadContext(IList<string> assemblies);
    }
}
