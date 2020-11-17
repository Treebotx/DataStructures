using DataStructures.Library;
using System;
using System.Linq;
using Xunit;

namespace DataStructures.Tests
{
    public class HashTableQuadraticProbingTests
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
        public void NewHashTableHasSizeOfZeroAndIsEmpty()
        {
            var ht = new HashTableQuadraticProbing<int, int>();

            Assert.Equal(0, ht.Size);
            Assert.True(ht.IsEmpty);
        }

        [Fact]
        public void NewHashTableWithACapacityOfLessThanZeroThrowsAnError()
        {
            Assert.Throws<ArgumentException>(() => new HashTableQuadraticProbing<int, int>(-1));
        }

        [Fact]
        public void NewHashTableLoadFactorShouldNotBeLessThanOrEqualToZero()
        {
            Assert.Throws<ArgumentException>(() => new HashTableQuadraticProbing<int, int>(1, 0));
            Assert.Throws<ArgumentException>(() => new HashTableQuadraticProbing<int, int>(1, -1));
        }

        [Fact]
        public void NewHashTableLoadFactorShouldNotBeMoreThanOne()
        {
            Assert.Throws<ArgumentException>(() => new HashTableQuadraticProbing<int, int>(1, 1.2));
            new HashTableQuadraticProbing<int, int>(1, 1);
        }

        [Fact]
        public void Insert_InsertingToANewHashTableSetsSizeToOneAndIsEmptyIsFalse()
        {
            var ht = new HashTableQuadraticProbing<int, int>();

            ht.Insert(1, 1);

            Assert.Equal(1, ht.Size);
            Assert.False(ht.IsEmpty);
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4 })]
        public void Insert_SizeShouldEqualNumberOfElementsInserted(int[] array)
        {
            var ht = new HashTableQuadraticProbing<int, int>(array.Length);
            foreach (var i in array) ht.Insert(i, 1);

            Assert.Equal(array.Length, ht.Size);
        }

        [Theory]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4 })]
        public void Insert_CanSuccessfullyAddBeyondInitialCapacity(int[] array)
        {
            var ht = new HashTableQuadraticProbing<int, int>(array.Length - 1);
            foreach (var i in array) ht.Insert(i, i);

            Assert.Equal(array.Length, ht.Size);
            foreach (var i in array) Assert.True(ht.ContainsKey(i));
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4 })]
        public void Insert_SuccesfullyAddsMultipleItemsWithTheSameHashCode(int[] array)
        {
            var ht = new HashTableQuadraticProbing<TestKey<int>, int>(array.Length);
            foreach (var i in array)
            {
                var tk = new TestKey<int>(i);
                ht.Insert(tk, i);
            }

            Assert.Equal(array.Length, ht.Size);
        }

        [Theory]
        [InlineData(new int[] { 11 }, 11)]
        [InlineData(new int[] { 11, 5, 7, 8, 9 }, 5)]
        public void ContainsKey_IsTrueIfAKeyHasBeenAdded(int[] array, int keyToFind)
        {
            var ht = new HashTableQuadraticProbing<int, int>(array.Length);
            foreach (var i in array) ht.Insert(i, i);

            Assert.True(ht.ContainsKey(keyToFind));
        }

        [Theory]
        [InlineData(new int[] { }, 6)]
        [InlineData(new int[] { 11 }, 6)]
        [InlineData(new int[] { 11, 5, 7, 8, 9 }, 6)]
        public void ContainsKey_IsFalseIfAKeyHasNotBeenAdded(int[] array, int keyToFind)
        {
            var ht = new HashTableQuadraticProbing<int, int>(array.Length);
            foreach (var i in array) ht.Insert(i, i);

            Assert.False(ht.ContainsKey(keyToFind));
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4 })]
        public void ContainsKey_SuccesfullyFindsItemsWithTheSameHashCode(int[] array)
        {
            var ht = new HashTableQuadraticProbing<TestKey<int>, int>(array.Length);
            foreach (var i in array)
            {
                var tk = new TestKey<int>(i);
                ht.Insert(tk, i);
            }

            foreach (var i in array)
            {
                var tk = new TestKey<int>(i);
                Assert.True(ht.ContainsKey(tk));
            }
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4 })]
        public void ContainsKey_SuccesfullyReturnsFalseEvenIfKeyHasSameHashCode(int[] array)
        {
            var ht = new HashTableQuadraticProbing<TestKey<int>, int>(array.Length);
            foreach (var i in array)
            {
                var tk = new TestKey<int>(i);
                ht.Insert(tk, i);
            }

            var testKey = new TestKey<int>(555);
            Assert.False(ht.ContainsKey(testKey));
        }

        [Theory]
        [InlineData(new int[] { 11 }, 11)]
        [InlineData(new int[] { 11, 5, 7, 8, 9 }, 5)]
        public void TryGetValue_ReturnsTrueIfAKeyHasBeenAddedAndPassesBackItsValue(int[] array, int keyToFind)
        {
            var ht = new HashTableQuadraticProbing<int, int>(array.Length);
            foreach (var i in array) ht.Insert(i, i);

            Assert.True(ht.TryGetValue(keyToFind, out int value));
            Assert.Equal(keyToFind, value);
        }

        [Theory]
        [InlineData(new int[] { }, 6)]
        [InlineData(new int[] { 11 }, 6)]
        [InlineData(new int[] { 11, 5, 7, 8, 9 }, 6)]
        public void TryGetValue_ReturnsFalseIfAKeyHasNotBeenAdded(int[] array, int keyToFind)
        {
            var ht = new HashTableQuadraticProbing<int, int>(array.Length);
            foreach (var i in array) ht.Insert(i, i);

            Assert.False(ht.TryGetValue(keyToFind, out _));
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4 })]
        public void TryGetValue_SuccesfullyFindsItemsWithTheSameHashCodeAndPassesBackItsValue(int[] array)
        {
            var ht = new HashTableQuadraticProbing<TestKey<int>, int>(array.Length);
            foreach (var i in array)
            {
                var tk = new TestKey<int>(i);
                ht.Insert(tk, i);
            }

            foreach (var i in array)
            {
                var tk = new TestKey<int>(i);
                Assert.True(ht.TryGetValue(tk, out int value));
                Assert.Equal(i, value);
            }
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4 })]
        public void TryGetValue_SuccesfullyReturnsFalseEvenIfKeyHasSameHashCode(int[] array)
        {
            var ht = new HashTableQuadraticProbing<TestKey<int>, int>(array.Length);
            foreach (var i in array)
            {
                var tk = new TestKey<int>(i);
                ht.Insert(tk, i);
            }

            var testKey = new TestKey<int>(555);
            Assert.False(ht.TryGetValue(testKey, out _));
        }

        [Theory]
        [InlineData(new int[] { 0 }, 0)]
        [InlineData(new int[] { 11, 5, 7, 8, 9 }, 7)]
        public void TryRemove_ReturnsTrueIfAKeyWasRemovedAndSizeDecreasesByOne(int[] array, int keyToRemove)
        {
            var ht = new HashTableQuadraticProbing<int, int>(array.Length);
            foreach (var i in array) ht.Insert(i, i);
            var expected = ht.Size - 1;

            Assert.True(ht.TryRemove(keyToRemove, out int value));
            Assert.Equal(keyToRemove, value);
            Assert.False(ht.ContainsKey(keyToRemove));
            Assert.Equal(expected, ht.Size);
        }

        [Theory]
        [InlineData(new int[] { }, 0)]
        [InlineData(new int[] { 0 }, 555)]
        [InlineData(new int[] { 11, 5, 7, 8, 9 }, 555)]
        public void TryRemove_ReturnsFalseIfAKeyWasNotRemovedAndSizeDoesNotChange(int[] array, int keyToRemove)
        {
            var ht = new HashTableQuadraticProbing<int, int>(array.Length);
            foreach (var i in array) ht.Insert(i, i);
            var expected = ht.Size;
            Assert.False(ht.ContainsKey(keyToRemove));

            Assert.False(ht.TryRemove(keyToRemove, out int _));
            Assert.False(ht.ContainsKey(keyToRemove));
            Assert.Equal(expected, ht.Size);
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 0 })]
        [InlineData(new int[] { 11, 5, 7, 8, 9 })]
        public void Clear_SizeResetToZeroAndIsEmpty(int[] array)
        {
            var ht = new HashTableQuadraticProbing<int, int>(array.Length);
            foreach (var i in array) ht.Insert(i, i);

            ht.Clear();

            Assert.True(ht.IsEmpty);
            Assert.Equal(0, ht.Size);
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 0 })]
        [InlineData(new int[] { 11, 5, 7, 8, 9 })]
        public void Clear_KeysNoLongerContainedInHashTable(int[] array)
        {
            var ht = new HashTableQuadraticProbing<int, int>(10);
            foreach (var i in array) ht.Insert(i, i);

            ht.Clear();

            foreach (var i in array) Assert.False(ht.ContainsKey(i));
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 0 })]
        [InlineData(new int[] { 11, 5, 7, 8, 9 })]
        public void Clear_CanStillAddItemsAfterClear(int[] array)
        {
            var ht = new HashTableQuadraticProbing<int, int>(10);
            foreach (var i in array) ht.Insert(i, i);
            ht.Clear();

            foreach (var i in array) ht.Insert(i, i);

            foreach (var i in array) Assert.True(ht.ContainsKey(i));
        }

        [Theory]
        [InlineData(new object[] { new string[] { } })]
        [InlineData(new object[] { new string[] { "1", "11" } })]
        [InlineData(new object[] { new string[] { "12", "22", "32", "42" } })]
        public void GetKeys_ReturnsAllTheKeysAdded(string[] array)
        {
            var ht = new HashTableQuadraticProbing<string, string>(array.Length);
            foreach (var i in array) ht.Insert(i, i);

            var listOfKeys = ht.GetKeys().ToList();

            Assert.Equal(array.Length, listOfKeys.Count);
            foreach (var i in array) Assert.Contains(i, listOfKeys);
        }

        [Theory]
        [InlineData(new string[] { "1" }, "1")]
        [InlineData(new string[] { "1", "11" }, "11")]
        [InlineData(new string[] { "12", "22", "32", "42" }, "22")]
        public void GetKeys_SkipsRemovedKeys(string[] array, string keyToRemove)
        {
            var ht = new HashTableQuadraticProbing<string, string>(array.Length);
            foreach (var i in array) ht.Insert(i, i);
            var expected = ht.Size - 1;
            ht.TryRemove(keyToRemove, out _);

            var listOfKeys = ht.GetKeys().ToList();

            Assert.Equal(expected, listOfKeys.Count);
            Assert.DoesNotContain(keyToRemove, listOfKeys);
        }

        [Theory]
        [InlineData(new object[] { new string[] { } })]
        [InlineData(new object[] { new string[] { "1", "11" } })]
        [InlineData(new object[] { new string[] { "12", "22", "32", "42" } })]
        public void GetValues_ReturnsAllTheValuesAdded(string[] array)
        {
            var ht = new HashTableQuadraticProbing<string, string>(array.Length);
            foreach (var i in array) ht.Insert(i, i);

            var listOfValues = ht.GetValues().ToList();

            Assert.Equal(array.Length, listOfValues.Count);
            foreach (var i in array) Assert.Contains(i, listOfValues);
        }

        [Theory]
        [InlineData(new string[] { "1" }, new int[] { 555 }, "1", 555)]
        [InlineData(new string[] { "1", "11" }, new int[] { 60, 90 }, "11", 90)]
        [InlineData(new string[] { "12", "22", "32", "42" }, new int[] { 60, 90, 2, 679 }, "32", 2)]
        public void GetValues_SkipsRemovedKeys(string[] array, int[] values, string keyToRemove, int valueRemoved)
        {
            var ht = new HashTableQuadraticProbing<string, int>(array.Length);
            for (var i = 0; i < array.Length; i++) ht.Insert(array[i], values[i]);
            var expected = ht.Size - 1;
            ht.TryRemove(keyToRemove, out _);

            var listOfValues = ht.GetValues().ToList();

            Assert.Equal(expected, listOfValues.Count);
            Assert.DoesNotContain(valueRemoved, listOfValues);
        }
    }
}
