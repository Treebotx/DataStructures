using DataStructures.Library;
using System;
using Xunit;

namespace DataStructures.Tests
{
    public class HashTableEntryTests
    {
        private class TestKey<T> : IEquatable<TestKey<T>>
        {
            public T Key { get; }
            public int Hash => 20;

            public TestKey(T value) => Key = value;

            public override int GetHashCode()
            {
                return Hash;
            }

            public bool Equals(TestKey<T> other)
            {
                return Key.Equals(other.Key);
            }

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                if (ReferenceEquals(this, obj)) return true;
                TestKey<T> other = obj as TestKey<T>;
                if (other == null) return false;
                else return Equals(other);
            }
        }

        [Fact]
        public void EntryHasKeyValueAndHash()
        {
            var hte = new HashTableEntry<int, int>(5, 6);

            Assert.Equal(5, hte.Key);
            Assert.Equal(6, hte.Value);
            Assert.Equal(hte.Key.GetHashCode(), hte.Hash);
        }

        [Fact]
        public void Equals_ReturnsTrueIfBothEntriesAreTheSame()
        {
            var hte = new HashTableEntry<int, int>(5, 6);
            var hte2 = hte;

            Assert.True(hte.Equals(hte2));
        }

        [Fact]
        public void Equals_ReturnsTrueIfBothEntriesContainTheSameValues()
        {
            var hte = new HashTableEntry<int, int>(5, 6);
            var hte2 = new HashTableEntry<int, int>(5, 6);

            Assert.True(hte.Equals(hte2));
        }

        [Fact]
        public void Equals_ReturnsFalseIfHashMatchesButKeysDont()
        {
            var key1 = new TestKey<int>(5);
            var key2 = new TestKey<int>(6);

            var hte = new HashTableEntry<TestKey<int>, int>(key1, 6);
            var hte2 = new HashTableEntry<TestKey<int>, int>(key2, 6);

            Assert.Equal(key1.GetHashCode(), key2.GetHashCode());
            Assert.False(hte.Equals(hte2));
        }
    }
}
