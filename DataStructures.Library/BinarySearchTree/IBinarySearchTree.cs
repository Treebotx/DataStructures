using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Library
{
    public interface IBinarySearchTree<T> : IEnumerable, IEnumerable<T>
    {
        bool IsEmpty { get; }
        int Size { get; }
        int Height { get; }
        // Add an item to the binary tree.
        // Return true if successful.
        bool Add(T item);
        // Remove an item from the binary tree, if it exists.
        // Return true if successful.
        bool Remove(T item);
        bool Contains(T item);
        IEnumerable<T> Traverse(TreeTraversalOrder order);
    }
}
