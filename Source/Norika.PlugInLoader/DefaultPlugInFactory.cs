using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Norika.PlugInLoader.Abstractions;
using Norika.PlugInLoader.Interfaces;

namespace Norika.PlugInLoader
{
    internal class DefaultPlugInFactory : IPlugInFactory
    {
        public IPlugIn CreatePlugIn(IAssemblyMetadata assemblyMetadata)
        {
            return new DefaultPlugIn(assemblyMetadata, GetAttribute<PlugInAssemblyAttribute>(assemblyMetadata));
        }
        
        private static T GetAttribute<T>(IAssemblyMetadata assembly) where T : Attribute
        {
            ICollection<CustomAttributeData> customAttributeDataList = assembly.GetCustomAttributeData();

            CustomAttributeData attributeData = customAttributeDataList.First(c =>
                typeof(T).FullName?.Equals(c.AttributeType.FullName, StringComparison.OrdinalIgnoreCase) == true);

            var constructorArguments = attributeData.ConstructorArguments
                .Select(c => c.Value).ToArray();
            return Activator.CreateInstance(typeof(T), constructorArguments) as T;
        }
    }
}
