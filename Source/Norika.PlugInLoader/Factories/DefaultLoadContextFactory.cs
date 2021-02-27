using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Norika.PlugInLoader.Interfaces;

namespace Norika.PlugInLoader.Factories
{
    internal class DefaultLoadContextFactory : ILoadContextFactory
    {
        private readonly IFileSystem _fileSystem;

        internal DefaultLoadContextFactory(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        
        public ILoadContext CreateLoadContext(IList<string> assemblies)
        {
            return new DefaultLoadContext(CreateResolver(AddCorePaths(assemblies)));
        }
        
        private static MetadataLoadContext CreateResolver(IList<string> paths)
        {
            PathAssemblyResolver pathResolver = new PathAssemblyResolver(paths);
            MetadataLoadContext loadContext = new MetadataLoadContext(pathResolver);
            return loadContext;
        }

        private IList<string> AddCorePaths(IList<string> paths)
        {
            return paths.Union(_fileSystem.Directory.GetFiles(
                RuntimeEnvironment.GetRuntimeDirectory(), "*.dll")).ToList();
        }
    }
}
