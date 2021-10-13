using System;
using System.Collections.Generic;

namespace DataStructures.Library.Sorting
{
    public class SelectionSort<T> : ISorting<T> where T : IComparable<T>
    {
        public void Sort(IList<T> listToSort)
        {
            for (var start = 0; start < listToSort.Count; start++)
            {
                var sm = start;

                for (var c = start + 1; c < listToSort.Count; c++)
                {
                    if (listToSort[c].IsLessThan(listToSort[sm])) sm = c;
                }

                listToSort.SwapElements(start, sm);
            }
        }
    }
}
