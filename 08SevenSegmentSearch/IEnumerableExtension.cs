using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._08SevenSegmentSearch
{
    public static class IEnumerableExtension
    {
        public static bool IsSubSetOf<T>(this IEnumerable<T> subset, IEnumerable<T> superSet)
        {
            return !subset.Except(superSet).Any();
        }
    }
}
