using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Norika.PlugInLoader.Abstractions;

namespace Norika.PlugInLoader
{
    [ExcludeFromCodeCoverage]
    internal class AssemblyAbstraction : IAssemblyMetadata
    {
        private readonly Assembly _assembly;

        public AssemblyAbstraction(Assembly assembly)
        {
            _assembly = assembly;
        }

        public string Location => _assembly.Location;
        public string FullName => _assembly.FullName;
        public byte[] GetPublicKey() => _assembly.GetName().GetPublicKey();
        
        public ICollection<CustomAttributeData> GetCustomAttributeData() => _assembly.GetCustomAttributesData();
        
        public Assembly GetAssembly()
        {
            return _assembly;
        }
    }
}
