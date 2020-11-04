using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Library
{
    public class MyStack<T> : IMyStack<T>
    {
        private LinkedList<T> _list = new LinkedList<T>();

        public int Length { get; private set; }

        public bool IsEmpty => Length == 0;

        public T Peek()
        {
            if (IsEmpty) throw new InvalidOperationException();
            return _list.First.Value;
        }

        public T Pop()
        {
            if (IsEmpty) throw new InvalidOperationException();

            var value = Peek();
            _list.RemoveFirst();
            Length--;

            return value;
        }

        public void Push(T item)
        {
            _list.AddFirst(item);
            Length++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (!IsEmpty) yield return Pop();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}