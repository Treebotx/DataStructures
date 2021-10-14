using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Library.Sorting
{
    public class HeapSort<T> : ISorting<T> where T : IComparable<T>
    {
        private IList<T> _list;

        public void Sort(IList<T> listToSort)
        {
            _list = listToSort;

            BuildMaxHeap();

            DoHeapSort();
        }

        private void DoHeapSort()
        {
            for (int i = _list.Count - 1; i >= 0; i--)
            {
                _list.SwapElements(0, i);

                Heapify(i, 0);
            }
        }

        private void BuildMaxHeap()
        {
            var n = _list.Count;

            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(n, i);
            }
        }

        private void Heapify(int size, int node)
        {
            var largest = node;
            var l = 2 * node + 1;
            var r = 2 * node + 2;

            if (l < size && _list[l].IsGreaterThan(_list[largest])) largest = l;
            if (r < size && _list[r].IsGreaterThan(_list[largest])) largest = r;

            if (largest != node)
            {
                _list.SwapElements(node, largest);

                Heapify(size, largest);
            }
        }
    }
}
