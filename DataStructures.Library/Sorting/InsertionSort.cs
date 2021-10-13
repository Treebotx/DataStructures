using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Library.Sorting
{
    public class InsertionSort<T> : ISorting<T> where T : IComparable<T>
    {
        public void Sort(IList<T> listToSort)
        {
            for (var i = 1; i < listToSort.Count; i++)
            {
                for (var j = i; j > 0 && listToSort[j].CompareTo(listToSort[j - 1]) < 0; j--)
                {
                    SwapItems(listToSort, j, j - 1);
                }
            }
        }

        private void SwapItems(IList<T> list, int i, int j)
        {
            var t = list[i];
            list[i] = list[j];
            list[j] = t;
        }
    }
}
