using System.Reflection;

namespace Norika.PlugInLoader.Abstractions
{
    public interface IPlugInMatcher
    {
        bool VerifyMatches(IAssemblyMetadata assembly);
    }
}
