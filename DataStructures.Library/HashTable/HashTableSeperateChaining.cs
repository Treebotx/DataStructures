using System;
using System.Collections.Generic;

namespace DataStructures.Library
{
    public class HashTableSeperateChaining<TKey, TValue> : IHashTable<TKey, TValue>
    {
        private static readonly int DEFAULT_CAPACITY = 3;
        private static readonly double DEFAULT_LOAD_FACTOR = 0.75;

        private int _capacity;
        private double _loadFactor;
        private int _threshold;

        private LinkedList<HashTableEntry<TKey, TValue>>[] _table;

        public int Size { get; private set; } = 0;

        public bool IsEmpty => Size == 0;

        public HashTableSeperateChaining() : this(DEFAULT_CAPACITY, DEFAULT_LOAD_FACTOR) { }
        public HashTableSeperateChaining(int capacity) : this(capacity, DEFAULT_LOAD_FACTOR) { }
        public HashTableSeperateChaining(int capacity, double loadFactor)
        {
            if (capacity < 0) throw new ArgumentException("Capacity cannot be less than zero.");
            if (loadFactor <= 0 || loadFactor > 1.0)
                throw new ArgumentException("Load factor should be > 0 and <=1.");

            _capacity = Math.Max(DEFAULT_CAPACITY, capacity);
            _loadFactor = loadFactor;
            _threshold = (int)(_capacity * _loadFactor);

            _table = new LinkedList<HashTableEntry<TKey, TValue>>[_capacity];
        }

        public void Insert(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException("Key value cannot be null.");
            var hte = new HashTableEntry<TKey, TValue>(key, value);
            var index = Index(hte.Hash);
            AddToBucket(index, hte);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null) throw new ArgumentNullException("Key value cannot be null.");
            var index = Index(key.GetHashCode());
            var hte = FindInBucket(index, key);
            
            if (hte == null)
            {
                value = default;
                return false;
            }

            value = hte.Value;
            return true;
        }

        public bool TryRemove(TKey key, out TValue value)
        {
            if (key == null) throw new ArgumentNullException("Key value cannot be null.");
            var index = Index(key.GetHashCode());
            var hte = RemoveFromBucket(index, key);
            if (hte == null)
            {
                value = default;
                return false;
            }

            Size--;
            value = hte.Value;
            return true;
        }

        public void Clear()
        {
            foreach (var i in _table)
            {
                if (i != null) i.Clear();
            }

            Size = 0;
        }

        public bool ContainsKey(TKey key)
        {
            if (key == null) throw new ArgumentNullException("Key value cannot be null.");
            var index = Index(key.GetHashCode());
            return FindInBucket(index, key) != null;
        }

        public IEnumerable<TKey> GetKeys()
        {
            foreach (var bucket in _table)
            {
                if (bucket != null)
                {
                    foreach (var item in bucket)
                    {
                        yield return item.Key;
                    }
                }
            }
        }

        public IEnumerable<TValue> GetValues()
        {
            foreach (var bucket in _table)
            {
                if (bucket != null)
                {
                    foreach (var item in bucket)
                    {
                        yield return item.Value;
                    }
                }
            }
        }

        private int Index(int hash)
        {
            return Math.Abs(hash) % _capacity;
        }

        private void AddToBucket(int index, HashTableEntry<TKey, TValue> hte)
        {
            if (_table[index] == null) _table[index] = new LinkedList<HashTableEntry<TKey, TValue>>();

            var existingEntry = FindInBucket(index, hte.Key);

            if (existingEntry == null)
            {
                _table[index].AddLast(hte);
                if (Size > _threshold) ResizeTable();
                Size++;
            }
            else
            {
                existingEntry.Value = hte.Value;
            }
        }

        private HashTableEntry<TKey, TValue> FindInBucket(int index, TKey key)
        {
            if (key == null) return null;
            var bucket = _table[index];
            if (bucket == null) return null;
            foreach (var i in bucket) if (i.Key.Equals(key)) return i;
            return null;
        }

        private HashTableEntry<TKey, TValue> RemoveFromBucket(int index, TKey key)
        {
            if (key == null) return null;
            var bucket = _table[index];
            HashTableEntry<TKey, TValue> hteToRemove = null;
            if (bucket == null) return null;
            foreach (var i in bucket)
            {
                if (i.Key.Equals(key))
                {
                    hteToRemove = i;
                    break;
                }
            }
            if (hteToRemove == null) return null;
            bucket.Remove(hteToRemove);
            return hteToRemove;
        }

        private void ResizeTable()
        {
            var oldTable = _table;
            _capacity *= 2;
            _threshold = (int)(_capacity * _loadFactor);

            _table = new LinkedList<HashTableEntry<TKey, TValue>>[_capacity];

            foreach (var bucket in oldTable)
            {
                if (bucket == null) continue;
                foreach (var hte in bucket)
                {
                    var index = Index(hte.Hash);
                    if (_table[index] == null) _table[index] = new LinkedList<HashTableEntry<TKey, TValue>>();
                    _table[index].AddLast(hte);
                }
                bucket.Clear();
            }
        }
    }
}
