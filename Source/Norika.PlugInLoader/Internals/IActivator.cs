using System;

namespace Norika.PlugInLoader.Internals
{
    internal interface IActivator
    {
        T CreateInstance<T>(object[] constructorArguments) where T : class;

        object CreateInstance(Type type, object[] constructorArguments);
        
        T CreateInstance<T>() where T : class;

        object CreateInstance(Type type);
    }
}