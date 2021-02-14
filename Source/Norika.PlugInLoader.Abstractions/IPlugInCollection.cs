using System.Collections.Generic;

namespace Norika.PlugInLoader.Abstractions
{
    public interface IPlugInCollection : IReadOnlyCollection<IPlugIn>
    {
        void LoadAll();
        
        ICollection<T> LoadAll<T>() where T : class;
    }
}
