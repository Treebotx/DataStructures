using System;

namespace DataStructures.Library
{
    public class HashTableEntry<TKey, TValue> : IEquatable<HashTableEntry<TKey, TValue>>
    {
        public int Hash { get; }
        public TKey Key { get; }
        public TValue Value { get; set; }

        public HashTableEntry(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Hash = key.GetHashCode();
        }

        public bool Equals(HashTableEntry<TKey, TValue> other)
        {
            if (Hash != other.Hash) return false;
            return Key.Equals(other.Key);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            HashTableEntry<TKey, TValue> other = obj as HashTableEntry<TKey, TValue>;
            if (other == null) return false;
            else return Equals(other);
        }

        public override int GetHashCode()
        {
            return Hash;
        }

        public override string ToString()
        {
            return $"{Key} => {Value}";
        }
    }
}
