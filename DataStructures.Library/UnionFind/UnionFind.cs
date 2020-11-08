using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Library
{
    public class UnionFind<T> : IUnionFind<T>
    {
        // Dictionary of elements and ids.
        private Dictionary<T, int> _elements;
        private List<int> _parents;
        // number of elements contained in a component
        private List<int> _count;

        public UnionFind() : this(16) { }

        public UnionFind(int size)
        {
            _elements = new Dictionary<T, int>();
            _parents = new List<int>(size);
            _count = new List<int>(size);
        }

        public UnionFind(ICollection<T> collection) : this(collection.Count)
        {
            foreach (var item in collection)
            {
                Add(item);
            }
        }

        public int Size => _elements.Count;

        public int ComponentCount { get; private set; }

        public void Add(T item)
        {
            _parents.Add(Size);
            _count.Add(1);
            _elements[item] = Size;
            ComponentCount++;
        }

        public int ComponentSize(T item)
        {
            return _count[FindComponent(item)];
        }

        public bool Connected(T item1, T item2)
        {
            return FindComponent(item1) == FindComponent(item2);
        }

        private int FindRootComponent(int index)
        {
            if (index == _parents[index]) return index;

            var rootComponent =  FindRootComponent(_parents[index]);

            _parents[index] = rootComponent;

            return rootComponent;
        }

        public int FindComponent(T item)
        {
            var index = _elements[item];
            return FindRootComponent(index);
        }

        public void Union(T item1, T item2)
        {
            var index1 = FindComponent(item1);
            var index2 = FindComponent(item2);

            if (index1 == index2) return;

            var size1 = ComponentSize(item1);
            var size2 = ComponentSize(item2);

            if (size1 < size2)
            {
                _parents[index1] = index2;
                _count[index2] += size1;
            }
            else
            {
                _parents[index2] = index1;
                _count[index1] += size2;
            }

            ComponentCount--;
        }
    }
}
