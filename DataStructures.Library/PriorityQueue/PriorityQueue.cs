using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataStructures.Library
{
    public class PriorityQueue<T> : IPriorityQueue<T> where T : IComparable, IComparable<T>
    {
        private List<T> _heap;

        public int Size => _heap.Count;
        public bool IsEmpty => Size == 0;

        public PriorityQueue() : this(16) { }

        public PriorityQueue(int capacity)
        {
            _heap = new List<T>(capacity);
        }

        public PriorityQueue(ICollection<T> collection)
        {
            _heap = new List<T>(collection);
            Heapify();
        }

        private void Heapify()
        {
            for (int i = Math.Max(0, (Size / 2) - 1); i >= 0; i--) Sink(i);
        }

        public void Add(T item)
        {
            _heap.Add(item);

            Swim(Size - 1);
        }

        public void Clear()
        {
            _heap.Clear();
        }

        public bool Contains(T item)
        {
            return IndexContaining(item) != -1;
        }

        public T Peek()
        {
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
            return _heap.IndexOf(item);
        }

        private void RemoveIndex(int index)
        {
            Swap(index, _heap.Count - 1);
            _heap.RemoveAt(_heap.Count - 1);

            Sink(index);
            Swim(index);
        }

        private void Swim(int index)
        {
            if (index == 0) return;

            var parent = Parent(index);
            if (!Less(parent, index))
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

            if (!Less(index, s))
            {
                Swap(index, s);
                Sink(s);
            }
        }

        private void Swap(int i, int j)
        {
            var k = _heap[i];
            _heap[i] = _heap[j];
            _heap[j] = k;
        }

        private bool Less(int i, int j)
        {
            if (j >= Size) return true;
            if (i >= Size) return false;
            return (_heap[i].CompareTo(_heap[j]) <= 0);
        }

        public bool IsItMinHeap()
        {
            for (int i = 0; i < ((Size + 1) / 2); i++)
            {
                if (!Less(i, LeftChild(i))) return false;
                if (!Less(i, RightChild(i))) return false;
            }
            return true;
        }
    }
}