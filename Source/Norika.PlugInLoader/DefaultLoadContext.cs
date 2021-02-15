using System.IO.Abstractions;
using System.Reflection;
using Norika.PlugInLoader.Abstractions;
using Norika.PlugInLoader.Interfaces;

namespace Norika.PlugInLoader
{
    internal class DefaultLoadContext : ILoadContext
    {
        private readonly MetadataLoadContext _loadContext;

        public DefaultLoadContext(MetadataLoadContext metadataLoadContext)
        {
            _loadContext = metadataLoadContext;
        }
        
        public void Dispose()
        {
            _loadContext?.Dispose();
        }

        public IAssemblyMetadata LoadFromAssemblyPath(string path)
        {
            return new AssemblyAbstraction(_loadContext.LoadFromAssemblyPath(path));
        }

        public IAssemblyMetadata LoadFromFileInfo(IFileInfo fileInfo)
        {
            return LoadFromAssemblyPath(fileInfo.FullName);
        }
    }
}
