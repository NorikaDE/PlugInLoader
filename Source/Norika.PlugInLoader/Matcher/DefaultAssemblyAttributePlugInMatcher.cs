using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Norika.PlugInLoader.Abstractions;

namespace Norika.PlugInLoader.Matcher
{
    internal class DefaultAssemblyAttributePlugInMatcher<T> : IPlugInMatcher where T : Attribute
    {
        public bool VerifyMatches(IAssemblyMetadata assembly)
        {
            ICollection<CustomAttributeData> customAttributeDataList = assembly.GetCustomAttributeData();
            return customAttributeDataList.Any(c =>
                typeof(T).FullName?.Equals(c.AttributeType.FullName, StringComparison.OrdinalIgnoreCase) == true);
        }
    }
}
