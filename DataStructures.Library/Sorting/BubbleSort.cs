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
                    if (listToSort[i].CompareTo(listToSort[i - 1]) < 0)
                    {
                        var temp = listToSort[i];
                        listToSort[i] = listToSort[i - 1];
                        listToSort[i - 1] = temp;
                        sorted = false;
                    }
                }
                offset++;
            }
        }
    }
}
