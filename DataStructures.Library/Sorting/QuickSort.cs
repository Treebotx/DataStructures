using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Library.Sorting
{
    public class QuickSort<T> : ISorting<T> where T : IComparable<T>
    {
        private IList<T> list;

        public void Sort(IList<T> listToSort)
        {
            list = listToSort;
            InternalSort(0, listToSort.Count - 1);
        }

        private void InternalSort(int start, int end)
        {
            if (start > end) return;

            var pivotPoint = Partition(start, end);

            InternalSort(start, pivotPoint - 1);
            InternalSort(pivotPoint + 1, end);
        }

        private int Partition(int start, int end)
        {
            var pivot = end;
            var left = start;

            for (; left < end && lt(left, pivot); left++) { }
            for (var j = left; j < end; j++)
            {
                if (lt(j, pivot))
                {
                    SwapItems(left, j);
                    left++;
                }
            }

            SwapItems(left, pivot);
            return left;
        }

        private bool lt(int i, int j)
        {
            return list[i].CompareTo(list[j]) < 0;
        }

        private void SwapItems(int i, int j)
        {
            var t = list[i];
            list[i] = list[j];
            list[j] = t;
        }
    }
}
