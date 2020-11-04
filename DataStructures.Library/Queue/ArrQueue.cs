using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Library
{
    public class ArrQueue<T> : IMyQueue<T>
    {
        private T[] _array;
        private int _start = 0;
        private int _end = 0;
        private int _increaseFactor = 2;

        public ArrQueue() : this(16) { }

        public ArrQueue(int initialCapacity)
        {
            _array = new T[initialCapacity];
        }

        public int Length { get; private set; } = 0;

        public bool IsEmpty => Length == 0;
        private bool IsFull => Length == _array.Length;

        public void Offer(T item)
        {
            //if (!isEmpty && start == end) throw new InvalidOperationException();
            //if (!IsEmpty && _start == _end)
            if (IsFull)
            {
                ResizeAndCopyArray();
            }

            _array[_end] = item;
            _end++;
            if (_end == _array.Length) _end = 0;
            Length++;
        }

        private void ResizeAndCopyArray()
        {
            var newArr = new T[_array.Length * _increaseFactor];

            var j = 0;
            for (var i = _start; i < _array.Length; i++) newArr[j++] = _array[i];
            for (var i = 0; i < _end; i++) newArr[j++] = _array[i];

            _start = 0;
            _end = _array.Length;

            _array = newArr;
        }

        public T Peek()
        {
            if (IsEmpty) throw new InvalidOperationException();
            return _array[_start];
        }

        public T Poll()
        {
            var value = Peek();
            _start++;
            if (_start == _array.Length) _start = 0;

            Length--;
            return value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (!IsEmpty) yield return Poll();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
