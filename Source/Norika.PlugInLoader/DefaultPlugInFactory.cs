using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Norika.PlugInLoader.Abstractions;
using Norika.PlugInLoader.Interfaces;
using Norika.PlugInLoader.Internals;

namespace Norika.PlugInLoader
{
    internal class DefaultPlugInFactory : IPlugInFactory
    {
        private readonly IActivator _activator;

        public DefaultPlugInFactory(IActivator activator)
        {
            _activator = activator;
        }

        public IPlugIn CreatePlugIn(IAssemblyMetadata assemblyMetadata)
        {
            return new DefaultPlugIn(assemblyMetadata, GetAttribute<PlugInAssemblyAttribute>(assemblyMetadata));
        }
        
        private T GetAttribute<T>(IAssemblyMetadata assembly) where T : Attribute
        {
            ICollection<CustomAttributeData> customAttributeDataList = assembly.GetCustomAttributeData();

            CustomAttributeData attributeData = customAttributeDataList.First(c =>
                typeof(T).FullName?.Equals(c.AttributeType.FullName, StringComparison.OrdinalIgnoreCase) == true);

            var constructorArguments = attributeData.ConstructorArguments
                .Select(c => c.Value).ToArray();
            return _activator.CreateInstance<T>(constructorArguments);
        }
    }
}
