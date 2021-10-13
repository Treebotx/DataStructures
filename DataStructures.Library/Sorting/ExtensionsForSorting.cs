using System;
using System.Collections.Generic;

namespace DataStructures.Library.Sorting
{
    public static class ExtensionsForSorting
    {
        public static void SwapElements<T>(this IList<T> list, int i, int j)
        {
            var t = list[i];
            list[i] = list[j];
            list[j] = t;
        }

        public static bool IsLessThan<T>(this T left, T right) where T : IComparable<T>
        {
            return (left.CompareTo(right) < 0);
        }
    }
}
