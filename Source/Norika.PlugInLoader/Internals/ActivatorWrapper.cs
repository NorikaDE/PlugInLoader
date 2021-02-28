using System;

namespace Norika.PlugInLoader.Internals
{
    internal class ActivatorWrapper : IActivator
    {
        public T CreateInstance<T>(object [] constructorArguments) where T : class
        {
            return CreateInstance(typeof(T), constructorArguments) as T;
        }

        public object CreateInstance(Type type, object[] constructorArguments)
        {
            return Activator.CreateInstance(type, constructorArguments);
        }

        public T CreateInstance<T>() where T : class
        {
            return CreateInstance<T>(Array.Empty<object>());
        }

        public object CreateInstance(Type type)
        {
            return CreateInstance(type, Array.Empty<object>());
        }
    }
}