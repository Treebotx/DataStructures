using System;
using System.Collections.Generic;

namespace DataStructures.Library.Sorting
{
    public class BubbleSort<T> : ISorting<T> where T : IComparable<T>
    {
        public void Sort(IList<T> listToSort)
        {
            var sorted = false;
            var offset = 0;

            while (!sorted)
            {
                sorted = true;
                for (var i = 1; i < (listToSort.Count - offset); i++)
                {
                    if (listToSort[i].IsLessThan(listToSort[i - 1]))
                    {
                        listToSort.SwapElements(i, i - 1);
                        sorted = false;
                    }
                }
                offset++;
            }
        }
    }
}
