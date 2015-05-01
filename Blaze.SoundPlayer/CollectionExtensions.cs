using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundPlayer
{
    static class CollectionExtensions
    {

        static public IList<int> IndexOfAll<T>(this IList<T> meself, Predicate<T> condition)
        {
            var res = new List<int>(10); 
            for (var ii = 0; ii < meself.Count; ++ii)
            {
                if (condition(meself[ii]))
                    res.Add(ii);
            }
            return res;
        }
    }
}
