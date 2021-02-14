using System;

namespace Norika.PlugInLoader.Abstractions
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class PlugInAssemblyAttribute : Attribute 
    {
        public string Name { get; }
        
        public PlugInAssemblyAttribute(string plugInName)
        {
            Name = plugInName;
        }
    }
    
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class PlugInAssemblyDockAttribute : Attribute 
    {
        public string Key { get; }
        
        public PlugInAssemblyDockAttribute(string key)
        {
            Key = key;
        }
    }
}
