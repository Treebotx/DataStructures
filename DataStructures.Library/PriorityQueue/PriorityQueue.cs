using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Library
{
    public class PriorityQueue<T> : IPriorityQueue<T> where T : IComparable, IComparable<T>
    {
        private List<T> _heap;
        private Dictionary<T, List<int>> _map;

        public int Size => _heap.Count;
        private int LastItem => Size - 1;
        public bool IsEmpty => Size == 0;

        public PriorityQueue() : this(16) { }

        public PriorityQueue(int capacity)
        {
            _heap = new List<T>(capacity);
            _map = new Dictionary<T, List<int>>();
        }

        public PriorityQueue(ICollection<T> collection)
        {

            _heap = new List<T>(collection.Count);
            _map = new Dictionary<T, List<int>>();

            foreach (var item in collection)
            {
                _heap.Add(item);
                AddToMap(item, LastItem);
            }
            if (!IsEmpty) Heapify();
        }

        private void Heapify()
        {
            for (int i = Math.Max(0, (Size / 2) - 1); i >= 0; i--) Sink(i);
        }

        public void Add(T item)
        {
            _heap.Add(item);
            AddToMap(item, LastItem);

            Swim(LastItem);
        }

        public void Clear()
        {
            _heap.Clear();
            _map.Clear();
        }

        public bool Contains(T item)
        {
            return IndexContaining(item) != -1;
        }

        public T Peek()
        {
            if (IsEmpty) throw new InvalidOperationException();
            return _heap[0];
        }

        public T Poll()
        {
            var result = Peek();

            RemoveIndex(0);

            return result;
        }

        public bool Remove(T item)
        {
            var indexToRemove = IndexContaining(item);

            if (indexToRemove == -1) return false;

            RemoveIndex(indexToRemove);

            return true;
        }

        private int Parent(int index)
        {
            return (index - 1) / 2;
        }

        private int LeftChild(int index)
        {
            return index * 2 + 1;
        }

        private int RightChild(int index)
        {
            return index * 2 + 2;
        }

        private int IndexContaining(T item)
        {
            //return _heap.IndexOf(item);
            if (!_map.ContainsKey(item)) return -1;
            return _map[item].First();
        }

        private void RemoveIndex(int index)
        {
            Swap(index, LastItem);
            RemoveFromMap(_heap[LastItem], LastItem);
            _heap.RemoveAt(LastItem);

            if (index >= Size) return;
            Sink(index);
            Swim(index);
        }

        private void Swim(int index)
        {
            if (index == 0) return;

            var parent = Parent(index);
            if (Less(index, parent))
            {
                Swap(parent, index);
                Swim(parent);
            }
        }

        private void Sink(int index)
        {
            var l = LeftChild(index);
            var r = RightChild(index);

            var s = Less(l, r) ? l : r;

            if (Less(s, index))
            {
                Swap(index, s);
                Sink(s);
            }
        }

        private void Swap(int i, int j)
        {
            if (i == j) return;
            MapSwap(i, _heap[i], j, _heap[j]);
            var k = _heap[i];
            _heap[i] = _heap[j];
            _heap[j] = k;
        }

        private void AddToMap(T item, int index)
        {
            if (!_map.ContainsKey(item)) _map[item] = new List<int>();
            _map[item].Add(index);
        }

        private void RemoveFromMap(T item, int index)
        {
            var list = _map[item];
            list.Remove(index);
            if (list.Count == 0) _map.Remove(item);
        }

        private void MapSwap(int index1, T item1, int index2, T item2)
        {
            ListAddRemove(_map[item1], index1, index2);
            ListAddRemove(_map[item2], index2, index1);
        }

        private void ListAddRemove(List<int> list, int toRemove, int toAdd)
        {
            list.Remove(toRemove);
            list.Add(toAdd);
        }

        private bool Less(int i, int j)
        {
            if (j >= Size) return true;
            if (i >= Size) return false;
            return (_heap[i].CompareTo(_heap[j]) <= 0);
        }

        public bool IsItMinHeap(int index)
        {
            if (index >= Size) return true;

            var l = LeftChild(index);
            var r = RightChild(index);

            if (!Less(index, LeftChild(index))) return false;
            if (!Less(index, RightChild(index))) return false;

            return IsItMinHeap(l) && IsItMinHeap(r);
        }
    }
}