namespace DataStructures.Library
{
    public interface IPriorityQueue<T>
    {
        int Size { get; }
        bool IsEmpty { get; }
        void Clear();
        T Peek();
        T Poll();
        bool Contains(T item);
        void Add(T item);
        bool Remove(T item);
    }
}
