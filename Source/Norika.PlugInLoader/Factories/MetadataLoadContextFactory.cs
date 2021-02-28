using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Norika.PlugInLoader.Factories
{
    [ExcludeFromCodeCoverage]
    internal class MetadataLoadContextFactory
    {
        internal virtual MetadataLoadContext CreateMetadataLoadContext(IList<string> paths)
        {
            PathAssemblyResolver pathResolver = new PathAssemblyResolver(paths.Distinct());
            return new MetadataLoadContext(pathResolver);
        }
    }
}