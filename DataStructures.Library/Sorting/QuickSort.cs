using System;
using System.Collections.Generic;

namespace DataStructures.Library.Sorting
{
    public class QuickSort<T> : ISorting<T> where T : IComparable<T>
    {
        private IList<T> _list;

        public void Sort(IList<T> listToSort)
        {
            _list = listToSort;
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

            for (; left < end && _list[left].IsLessThan(_list[pivot]); left++) { }
            for (var j = left; j < end; j++)
            {
                if (_list[j].IsLessThan(_list[pivot]))
                {
                    _list.SwapElements(left, j);
                    left++;
                }
            }

            _list.SwapElements(left, pivot);
            return left;
        }
    }
}
