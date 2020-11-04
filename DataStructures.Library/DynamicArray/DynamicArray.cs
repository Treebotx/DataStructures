using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Library
{
    public class DynamicArray<T> : IDynamicArray<T>
    {
        private T[] _array;
        private int _increaseFactor = 2;

        public DynamicArray()
            : this(16) { }

        public DynamicArray(int capacity)
        {
            _array = new T[capacity];
        }

        public int Length { get; private set; } = 0;

        public bool IsEmpty => (Length == 0);

        public void Add(T item)
        {
            if (Length == _array.Length)
            {
                var newArray = new T[_array.Length * _increaseFactor];

                for (int i = 0; i < _array.Length; i++) newArray[i] = _array[i];

                _array = newArray;
            }

            _array[Length] = item;

            Length++;
        }

        public void Clear()
        {
            Length = 0;
        }

        public bool Contains(T item)
        {
            //foreach (var i in _array)
            //{
            //    if (i.Equals(item)) return true;
            //}

            //return false;

            return IndexOf(item) != -1;
        }

        public T Get(int index)
        {
            if (index >= Length) throw new IndexOutOfRangeException();
            return _array[index];
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Length; i++)
            {
                if (item.Equals(_array[i])) return i;
            }

            return -1;
        }

        public bool Remove(T item)
        {
            var index = IndexOf(item);

            if (index == -1) return false;

            RemoveAt(index);

            return true;
        }

        public T RemoveAt(int index)
        {
            if (index >= Length) throw new IndexOutOfRangeException();

            var result = _array[index];

            for (var i = index; i < Length; i++)
            {
                _array[i] = _array[i + 1];
            }

            Length--;
            return result;
        }

        public void Set(int index, T item)
        {
            if (index >= Length) throw new IndexOutOfRangeException();
            _array[index] = item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Length; i++)
            {
                yield return _array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            //if (Length == 0) return "[]";

            var sb = new StringBuilder();
            sb.Append("[ ");
            for (int i = 0; i < Length; i++)
            {
                sb.Append(_array[i]);
                if (i < (Length - 1)) sb.Append(", ");
            }
            sb.Append(" ]");

            return sb.ToString();
        }
    }
}