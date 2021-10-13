using System;
using System.Collections.Generic;

namespace DataStructures.Library.Sorting
{
    public class MergeSort<T> : ISorting<T> where T : IComparable<T>
    {
        public void Sort(IList<T> listToSort)
        {
            var sortedList = InnerSort(listToSort, 0, listToSort.Count);

            CopyTo(sortedList, listToSort);
        }

        private void CopyTo(IList<T> from, IList<T> to)
        {
            for (var i = 0; i < from.Count; i++) to[i] = from[i];
        }

        private IList<T> InnerSort(IList<T> list, int start, int length)
        {
            if (length == 0) return new List<T>();
            if (length == 1) return new List<T> { list[start] };

            var newLength = length / 2;

            var left = InnerSort(list, start, newLength);
            var right = InnerSort(list, start + newLength, length - newLength);

            var result = MergeLists(left, right);

            return result;
        }

        private IList<T> MergeLists(IList<T> left, IList<T> right)
        {
            var result = new List<T>();

            var leftIndex = 0;
            var rightIndex = 0;

            while (leftIndex < left.Count)
            {
                if (rightIndex < right.Count)
                {
                    if (left[leftIndex].IsLessThan(right[rightIndex]) )
                    {
                        result.Add(left[leftIndex++]);
                    }
                    else
                    {
                        result.Add(right[rightIndex++]);
                    }
                }
                else
                {
                    result.Add(left[leftIndex++]);
                }
            }

            while (rightIndex < right.Count)
            {
                result.Add(right[rightIndex++]);
            }

            return result;
        }
    }
}
