using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Library
{
    public class MyQueue<T> : IMyQueue<T>
    {
        private LinkedList<T> _list = new LinkedList<T>();
        public int Length { get; private set; } = 0;

        public bool IsEmpty => Length == 0;

        public void Offer(T item)
        {
            _list.AddLast(item);
            Length++;
        }

        public T Peek()
        {
            if (IsEmpty) throw new InvalidOperationException();
            return _list.First.Value;
        }

        public T Poll()
        {
            var value = Peek();
            _list.RemoveFirst();
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