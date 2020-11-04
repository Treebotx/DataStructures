using System.Collections.Generic;

namespace DataStructures.Library
{
    public interface ILinkedList<T> : IEnumerable<T>
    {
        int Length { get; }
        bool IsEmpty { get; }

        void Clear();
        void Add(T item);
        void AddFirst(T item);
        void AddLast(T item);
        T PeekFirst();
        T PeekLast();
        T RemoveFirst();
        T RemoveLast();
        T RemoveAt(int index);
        bool Remove(T item);
        int IndexOf(T item);
        bool Contains(T item);
    }
}
