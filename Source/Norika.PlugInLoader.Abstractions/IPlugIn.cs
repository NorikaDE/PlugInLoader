using System;
using System.Collections.Generic;

namespace Norika.PlugInLoader.Abstractions
{
    public interface IPlugIn
    {
        string Name { get; }

        void Load();

        T Load<T>() where T : class;
        
        IList<T> LoadMultiple<T>() where T : class;
    }
}
