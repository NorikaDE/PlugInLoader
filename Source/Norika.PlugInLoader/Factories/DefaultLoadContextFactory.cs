using System.Collections.Generic;
using System.Reflection;
using Norika.PlugInLoader.Interfaces;

namespace Norika.PlugInLoader.Factories
{
    internal class DefaultLoadContextFactory : ILoadContextFactory
    {
        public ILoadContext CreateLoadContext(IList<string> assemblies)
        {
            return new DefaultLoadContext(CreateResolver(assemblies));
        }
        
        private static MetadataLoadContext CreateResolver(IList<string> paths)
        {
            PathAssemblyResolver pathResolver = new PathAssemblyResolver(paths);
            MetadataLoadContext loadContext = new MetadataLoadContext(pathResolver);
            return loadContext;
        }
    }
}
