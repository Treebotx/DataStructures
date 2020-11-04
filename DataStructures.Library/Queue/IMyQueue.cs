using System.Collections.Generic;

namespace DataStructures.Library
{
    public interface IMyQueue<T> : IEnumerable<T>
    {
        int Length { get; }
        bool IsEmpty { get; }
        T Peek();
        T Poll();
        void Offer(T item);
    }
}
