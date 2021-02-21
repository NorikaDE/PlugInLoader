using System;
using System.Collections.Generic;
using System.Linq;

namespace Norika.PlugInLoader.UnitTests
{
    public static class CollectionExtension
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            var array = collection.ToArray();
            foreach (var entry in array)
            {
                action.Invoke(entry);
            }
        }
    }
}