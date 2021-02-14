using System.Collections.Generic;
using System.Reflection;

namespace Norika.PlugInLoader.Abstractions
{
    public interface IAssemblyMetadata
    {
        string Location { get; }
        
        string FullName { get; }

        byte[] GetPublicKey();

        ICollection<CustomAttributeData> GetCustomAttributeData();

        Assembly GetAssembly();
    }
}
