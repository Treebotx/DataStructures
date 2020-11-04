using System.Collections.Generic;

namespace DataStructures.Library
{
    public interface IMyStack<T> : IEnumerable<T>
    {
        int Length { get; }
        bool IsEmpty { get; }
        void Push(T item);
        T Pop();
        T Peek();
    }
}
