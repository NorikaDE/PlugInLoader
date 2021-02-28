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
        private readonly MetadataLoadContextFactory _metadataLoadContextFactory;
        private readonly IFileSystem _fileSystem;

        internal DefaultLoadContextFactory(IFileSystem fileSystem) : this(fileSystem, new MetadataLoadContextFactory())
        { }

        internal DefaultLoadContextFactory(IFileSystem fileSystem, MetadataLoadContextFactory metadataLoadContextFactory)
        {
            _fileSystem = fileSystem;
            _metadataLoadContextFactory = metadataLoadContextFactory;
        }
        
        public ILoadContext CreateLoadContext(IList<string> assemblies)
        {
            return new DefaultLoadContext(CreateResolver(AddCorePaths(assemblies)));
        }
        
        private MetadataLoadContext CreateResolver(IList<string> paths)
        {
            return _metadataLoadContextFactory.CreateMetadataLoadContext(paths);
        }

        private IList<string> AddCorePaths(IList<string> paths)
        {
            return paths.Union(_fileSystem.Directory.GetFiles(
                RuntimeEnvironment.GetRuntimeDirectory(), "*.dll")).ToList();
        }
    }
}
