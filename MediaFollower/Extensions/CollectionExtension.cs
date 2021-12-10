using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaFollower.Extensions
{
    internal static class CollectionExtension
    {
        public static void AddRange<T>(this ICollection<T> collection, T[] items)
        {
            foreach(var item in items)
            {
                collection.Add(item);
            }
        }
    }
}
