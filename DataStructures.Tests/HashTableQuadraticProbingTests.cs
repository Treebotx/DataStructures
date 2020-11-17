using DataStructures.Library;
using System;
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

        // ToDo: Make sure tombstones successfully removed on resize
        // ToDo: Tombstones are 'compressed' when skipped in a find
        // 
    }
}
