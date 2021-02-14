using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Norika.PlugInLoader.Abstractions;

namespace Norika.PlugInLoader
{
    public class DefaultPlugInCollection : ReadOnlyCollection<IPlugIn>, IPlugInCollection
    {
        public DefaultPlugInCollection(IList<IPlugIn> list) : base(list) { }
        public void LoadAll()
        {
            foreach (var plugIn in Items)
            {
                plugIn.Load();
            }
        }

        public ICollection<T> LoadAll<T>() where T : class
        {
            return Items.SelectMany(plugIn => plugIn.LoadMultiple<T>()).ToList();
        }
    }
}
