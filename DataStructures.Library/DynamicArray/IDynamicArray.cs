using System.Collections.Generic;

namespace DataStructures.Library
{
    public interface IDynamicArray<T> : IEnumerable<T>
    {
        int Length { get; }
        bool IsEmpty { get; }
        T Get(int index);
        void Set(int index, T item);
        void Clear();
        void Add(T item);
        T RemoveAt(int index);
        bool Remove(T item);
        int IndexOf(T item);
        bool Contains(T item);
    }
}
