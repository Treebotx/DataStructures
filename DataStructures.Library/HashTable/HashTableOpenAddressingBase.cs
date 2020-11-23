using System;
using System.Collections.Generic;

namespace DataStructures.Library
{
    public abstract class HashTableOpenAddressingBase<TKey, TValue> : IHashTable<TKey, TValue>
    {
        private static readonly int DEFAULT_CAPACITY = 8;
        private static readonly double DEFAULT_LOADFACTOR = 0.45;

        private int _capacity;
        private double _loadFactor;
        private int _threshold;
        private int _tombStoneCount;
        private int _actualSize;
        private HashTableEntry<TKey, TValue>[] _table;

        public int Size => _actualSize - _tombStoneCount;

        public bool IsEmpty => Size == 0;

        public HashTableOpenAddressingBase() : this(DEFAULT_CAPACITY, DEFAULT_LOADFACTOR) { }

        public HashTableOpenAddressingBase(int capacity) : this(capacity, DEFAULT_LOADFACTOR) { }

        public HashTableOpenAddressingBase(int capacity, double loadFactor)
        {
            if (capacity < 0) throw new ArgumentException("Capacity cannot be less than zero");
            if (loadFactor <= 0) throw new ArgumentException("Load Factor cannot be less than or equal to zero");
            if (loadFactor > 1.0) throw new ArgumentException("Load Factor cannot be greater than one");

            _capacity = Math.Max(DEFAULT_CAPACITY, capacity);
            _capacity = AdjustCapacity(_capacity);
            _loadFactor = loadFactor;
            _threshold = (int)(_capacity * _loadFactor);

            _table = new HashTableEntry<TKey, TValue>[_capacity];
        }

        protected abstract int IncreaseCapacity(int capacity);
        protected abstract int AdjustCapacity(int capacity);
        protected abstract int Probe(int x);

        public void Insert(TKey key, TValue value)
        {
            var index = FindKey(key);
            if (_table[index] == null)
            {
                if (_actualSize >= _threshold) ResizeTable();
                index = FindKey(key);
                _table[index] = new HashTableEntry<TKey, TValue>(key, value);
                _actualSize++;
            }
            else
            {
                _table[index].Value = value;
            }
        }

        public bool ContainsKey(TKey key)
        {
            var index = FindKey(key);
            return _table[index] != null;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var index = FindKey(key);

            if (_table[index] == null)
            {
                value = default;
                return false;
            }

            value = _table[index].Value;
            return true;
        }

        public bool TryRemove(TKey key, out TValue value)
        {
            var index = FindKey(key);

            if (_table[index] == null)
            {
                value = default;
                return false;
            }

            value = _table[index].Value;
            _table[index].MakeTombstone();
            _tombStoneCount++;
            return true;
        }

        public void Clear()
        {
            for (var i = 0; i < _table.Length; i++) _table[i] = null;
            _actualSize = 0;
            _tombStoneCount = 0;
        }

        public IEnumerable<TKey> GetKeys()
        {
            foreach (var item in _table)
            {
                if (item != null && !item.TOMBSTONE) yield return item.Key;
            }
        }

        public IEnumerable<TValue> GetValues()
        {
            foreach (var item in _table)
            {
                if (item != null && !item.TOMBSTONE) yield return item.Value;
            }
        }

        private int FindKey(TKey key)
        {
            var x = 1;
            var index = Index(key.GetHashCode());
            var i = index;
            var j = -1;
            while (_table[i] != null)
            {
                if (_table[i].Key.Equals(key) && !_table[i].TOMBSTONE) break;
                if (j == -1 && _table[i].TOMBSTONE) j = i;
                i = Index(index + Probe(x++));
            }

            if (j != -1)
            {
                _table[j] = _table[i];
                _table[i] = null;
                _tombStoneCount -= 1;
                _actualSize -= 1;
                return j;
            }

            return i;
        }

        private int Index(int hash)
        {
            return Math.Abs(hash) % _capacity;
        }

        private void ResizeTable()
        {

            _capacity = IncreaseCapacity(_capacity);
            _capacity = AdjustCapacity(_capacity);

            var oldTable = _table;
            _threshold = (int)(_capacity * _loadFactor);
            _table = new HashTableEntry<TKey, TValue>[_capacity];
            _tombStoneCount = 0;
            _actualSize = 0;

            foreach (var i in oldTable)
            {
                if (i != null && !i.TOMBSTONE)
                {
                    var index = FindKey(i.Key);
                    _table[index] = i;
                    _actualSize++;
                }
            }
        }
    }
}
