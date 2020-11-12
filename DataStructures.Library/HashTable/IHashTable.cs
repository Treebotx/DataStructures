using System.Collections.Generic;

namespace DataStructures.Library
{
    public interface IHashTable<TKey, TValue>
    {
        int Size { get; }
        bool IsEmpty { get; }
        void Clear();
        bool ContainsKey(TKey key);
        void Insert(TKey key, TValue value);
        bool TryGetValue(TKey key, out TValue value);
        bool TryRemove(TKey key, out TValue value);
        IEnumerable<TKey> GetKeys();
        IEnumerable<TValue> GetValues();
    }
}
