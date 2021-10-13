using System;
using System.Collections.Generic;

namespace DataStructures.Library.Sorting
{
    public class InsertionSort<T> : ISorting<T> where T : IComparable<T>
    {
        public void Sort(IList<T> listToSort)
        {
            for (var i = 1; i < listToSort.Count; i++)
            {
                for (var j = i; j > 0 && listToSort[j].IsLessThan(listToSort[j - 1]); j--)
                {
                    listToSort.SwapElements(j, j - 1);
                }
            }
        }
    }
}
