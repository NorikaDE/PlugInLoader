using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Norika.PlugInLoader.Abstractions;

namespace Norika.PlugInLoader
{
    public class DefaultPlugIn : IPlugIn
    {
        private readonly IAssemblyMetadata _loadAssembly;

        public DefaultPlugIn(IAssemblyMetadata loadAssembly, PlugInAssemblyAttribute attribute)
        {
            _loadAssembly = loadAssembly;
            Name = attribute.Name;
        }

        public string Name { get; }
        
        public void Load()
        {
            Assembly.LoadFrom(_loadAssembly.Location);
        }

        public T Load<T>() where T : class
        {
            Load();
            var allTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes());
            var targetType = allTypes
                .FirstOrDefault(t 
                    => typeof(T).IsAssignableFrom(t)
                       && !t.IsInterface
                       && t.IsClass);

            if (targetType == null)
            {
                throw new Exception();
            }
            return Activator.CreateInstance(targetType) as T;
        }
        
        public IList<T> LoadMultiple<T>() where T : class
        {
            Load();
            var allTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes());
            var targetTypes = allTypes
                .Where(t 
                    => typeof(T).IsAssignableFrom(t)
                       && !t.IsInterface
                       && t.IsClass);

            if (targetTypes == null)
            {
                throw new Exception();
            }
            return targetTypes.Select(foundType => Activator.CreateInstance(foundType) as T).ToList();
        }
    }
}
