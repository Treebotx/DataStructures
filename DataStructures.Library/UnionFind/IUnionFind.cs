namespace DataStructures.Library
{
    public interface IUnionFind<T>
    {
        // The number of elements
        int Size { get; }
        // The number of components/sets int his union find
        int ComponentCount { get; }
        // Add a new item to the list of elements
        void Add(T item);
        // Find which component/set item belongs to
        int FindComponent(T item);
        // Return whether or not item1 and item2 are in the same component/set
        bool Connected(T item1, T item2);
        // Returns the size of the component/set item belongs to
        int ComponentSize(T item);
        // Combine the components/sets containing item1 and item2
        void Union(T item1, T item2);
    }
}
