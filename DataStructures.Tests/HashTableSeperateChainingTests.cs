using DataStructures.Library;
using System;
using System.Linq;
using Xunit;

namespace DataStructures.Tests
{
    public class HashTableSeperateChainingTests
    {
        [Fact]
        public void NewHashTableHasSizeOfZeroAndIsEmpty()
        {
            var ht = new HashTableSeperateChaining<int, int>();

            Assert.Equal(0, ht.Size);
            Assert.True(ht.IsEmpty);
        }

        [Fact]
        public void NewHashTableWithACapacityOfLessThanZeroThrowsAnError()
        {
            Assert.Throws<ArgumentException>(() => new HashTableSeperateChaining<int, int>(-1));
        }

        [Fact]
        public void NewHashTableLoadFactorShouldNotBeLessThanOrEqualToZero()
        {
            Assert.Throws<ArgumentException>(() => new HashTableSeperateChaining<int, int>(1, 0));
            Assert.Throws<ArgumentException>(() => new HashTableSeperateChaining<int, int>(1, -1));
        }

        [Fact]
        public void NewHashTableLoadFactorShouldNotBeMoreThanOne()
        {
            Assert.Throws<ArgumentException>(() => new HashTableSeperateChaining<int, int>(1, 1.2));
            new HashTableSeperateChaining<int, int>(1, 1);
        }

        [Fact]
        public void Insert_InsertingToANewHashTableSetsSizeToOneAndIsEmptyIsFalse()
        {
            var ht = new HashTableSeperateChaining<int, int>();

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
            var ht = new HashTableSeperateChaining<int, int>(array.Length);
            foreach (var i in array) ht.Insert(i, 1);

            Assert.Equal(array.Length, ht.Size);
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 2, 3, 4 }, 4)]
        public void Insert_InsertOnAnExistingElementChangesTheValue(int[] array, int keyToChange)
        {
            var ht = new HashTableSeperateChaining<int, int>(array.Length);
            foreach (var i in array) ht.Insert(i, 1);

            Assert.True(ht.TryGetValue(keyToChange, out int value));
            Assert.Equal(1 , value);
            ht.Insert(keyToChange, 555);

            Assert.True(ht.TryGetValue(keyToChange, out value));
            Assert.Equal(555, value);
        }

        [Theory]
        [InlineData(new int[] { 1, 11 })]
        [InlineData(new int[] { 12, 22, 32, 42 })]
        public void Insert_SuccessfullyAddsMultipleItemsThatShouldGoInTheSameBucket(int[] array)
        {
            var ht = new HashTableSeperateChaining<int, int>(10);
            foreach (var i in array) ht.Insert(i, 1);

            Assert.Equal(array.Length, ht.Size);
        }

        [Theory]
        [InlineData(new int[] { 1, 11 })]
        [InlineData(new int[] { 12, 22, 32, 42 })]
        public void Insert_SuccessfullyResizesTableWhenThresholdIsReached(int[] array)
        {
            var ht = new HashTableSeperateChaining<int, int>(array.Length, 0.25);
            foreach (var i in array) ht.Insert(i, 1);

            Assert.Equal(array.Length, ht.Size);
        }

        [Fact]
        public void TryGetValue_ReturnsTrueAndTheValueOfTheKey()
        {
            var ht = new HashTableSeperateChaining<int, int>();

            ht.Insert(1, 2);

            Assert.True(ht.TryGetValue(1, out int value));
            Assert.Equal(2, value);
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 2, 3, 4 }, 4)]
        public void TryGetValue_ReturnsTrueAndTheValueOfTheKeyRequested(int[] array, int keyToRequest)
        {
            var ht = new HashTableSeperateChaining<int, int>(array.Length);
            foreach (var i in array) ht.Insert(i, i);

            Assert.True(ht.TryGetValue(keyToRequest, out int value));
            Assert.Equal(keyToRequest, value);
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 2, 3, 4 }, 4)]
        public void TryGetValue_ReturnsTrueAndTheValueOfTheKeyRequestedAfterSuccesfulResize(int[] array, int keyToRequest)
        {
            var ht = new HashTableSeperateChaining<int, int>(array.Length, 0.25);
            foreach (var i in array) ht.Insert(i, i);

            Assert.True(ht.TryGetValue(keyToRequest, out int value));
            Assert.Equal(keyToRequest, value);
        }

        [Theory]
        [InlineData(new int[] { }, 1)]
        [InlineData(new int[] { 1 }, 2)]
        [InlineData(new int[] { 1, 2, 3, 4 }, 555)]
        public void TryGetValue_ReturnsFalseIfKeyNotInserted(int[] array, int keyToRequest)
        {
            var ht = new HashTableSeperateChaining<int, int>(array.Length);
            foreach (var i in array) ht.Insert(i, i);
            Assert.False(ht.TryGetValue(keyToRequest, out _));
        }

        [Theory]
        [InlineData(new int[] { 1, 11 })]
        [InlineData(new int[] { 12, 22, 32, 42 })]
        public void TryGetValue_SuccessfullyReturnsItemsThatShouldBeInTheSameBucket(int[] array)
        {
            var ht = new HashTableSeperateChaining<int, int>(10);
            foreach (var i in array) ht.Insert(i, i);

            foreach (var i in array)
            {
                Assert.True(ht.TryGetValue(i, out int value));
                Assert.Equal(i, value);
            }
        }

        [Theory]
        [InlineData(new object[] { new string[] { } })]
        [InlineData(new object[] { new string[] { "1", "11" } })]
        [InlineData(new object[] { new string[] { "12", "22", "32", "42" } })]
        public void TryGetValue_ThrowsArgumentNullExceptionIfTheKeyIsNull(string[] array)
        {
            var ht = new HashTableSeperateChaining<string, string>(10);
            foreach (var i in array) ht.Insert(i, i);

            Assert.Throws<ArgumentNullException>(() => ht.TryGetValue(null, out string _));
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 2, 3, 4 }, 4)]
        public void ContainsKey_ReturnsTrueIfKeyHasBeenInserted(int[] array, int keyToRequest)
        {
            var ht = new HashTableSeperateChaining<int, int>(array.Length);
            foreach (var i in array) ht.Insert(i, i);

            Assert.True(ht.ContainsKey(keyToRequest));
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 2, 3, 4 }, 4)]
        public void ContainsKey_ReturnsTrueAfterSuccesfulResize(int[] array, int keyToRequest)
        {
            var ht = new HashTableSeperateChaining<int, int>(array.Length, 0.25);
            foreach (var i in array) ht.Insert(i, i);

            Assert.True(ht.ContainsKey(keyToRequest));
        }

        [Theory]
        [InlineData(new int[] { }, 1)]
        [InlineData(new int[] { 1 }, 2)]
        [InlineData(new int[] { 1, 2, 3, 4 }, 555)]
        public void ContainsKey_ReturnsFalseIfKeyNotInserted(int[] array, int keyToRequest)
        {
            var ht = new HashTableSeperateChaining<int, int>(array.Length);
            foreach (var i in array) ht.Insert(i, i);
            Assert.False(ht.ContainsKey(keyToRequest));
        }

        [Theory]
        [InlineData(new int[] { 1, 11 })]
        [InlineData(new int[] { 12, 22, 32, 42 })]
        public void ContainsKey_SuccessfullyReturnsTrueForKeysThatShouldBeInTheSameBucket(int[] array)
        {
            var ht = new HashTableSeperateChaining<int, int>(10);
            foreach (var i in array) ht.Insert(i, i);

            foreach (var i in array)
            {
                Assert.True(ht.ContainsKey(i));
            }
        }

        [Theory]
        [InlineData(new object[] { new string[] { } })]
        [InlineData(new object[] { new string[] { "1", "11" } })]
        [InlineData(new object[] { new string[] { "12", "22", "32", "42" } })]
        public void ContainsKey_ThrowsArgumentNullExceptionIfTheKeyIsNull(string[] array)
        {
            var ht = new HashTableSeperateChaining<string, string>(10);
            foreach (var i in array) ht.Insert(i, i);

            Assert.Throws<ArgumentNullException>(() => ht.ContainsKey(null));
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 11 }, 11)]
        [InlineData(new int[] { 12, 22, 32, 42 }, 32)]
        public void TryRemove_DecreasesSizeByOneIfSuccessful(int[] array, int keyToRemove)
        {
            var ht = new HashTableSeperateChaining<int, int>();
            foreach (var i in array) ht.Insert(i, i);
            var expected = ht.Size - 1;

            ht.TryRemove(keyToRemove, out _);

            Assert.Equal(expected, ht.Size);
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 11 }, 11)]
        [InlineData(new int[] { 12, 22, 32, 42 }, 32)]
        [InlineData(new int[] { 12, 22, 32, 42 }, 12)]
        [InlineData(new int[] { 12, 22, 32, 42 }, 42)]
        public void TryRemove_KeyIsNoLongerContainedAfterSuccessfulRemoval(int[] array, int keyToRemove)
        {
            var ht = new HashTableSeperateChaining<int, int>();
            foreach (var i in array) ht.Insert(i, i);

            ht.TryRemove(keyToRemove, out _);

            Assert.False(ht.ContainsKey(keyToRemove));
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 11 }, 11)]
        [InlineData(new int[] { 12, 22, 32, 42 }, 32)]
        [InlineData(new int[] { 12, 22, 32, 42 }, 12)]
        [InlineData(new int[] { 12, 22, 32, 42 }, 42)]
        public void TryRemove_ReturnsValueOfKeyRemoved(int[] array, int keyToRemove)
        {
            var ht = new HashTableSeperateChaining<int, int>();
            foreach (var i in array) ht.Insert(i, i);

            ht.TryRemove(keyToRemove, out int value);

            Assert.Equal(keyToRemove, value);
        }

        [Theory]
        [InlineData(new int[] { 1 }, 2)]
        [InlineData(new int[] { 1, 11 }, 555)]
        [InlineData(new int[] { 12, 22, 32, 42 }, 666)]
        public void TryRemove_ReturnsFalseIfKeyNotRemoved(int[] array, int keyToRemove)
        {
            var ht = new HashTableSeperateChaining<int, int>();
            foreach (var i in array) ht.Insert(i, i);

            Assert.False(ht.TryRemove(keyToRemove, out _));
        }

        [Theory]
        [InlineData(new object[] { new string[] { } })]
        [InlineData(new object[] { new string[] { "1", "11" } })]
        [InlineData(new object[] { new string[] { "12", "22", "32", "42" } })]
        public void TryRemove_ThrowsArgumentNullExceptionIfTheKeyIsNull(string[] array)
        {
            var ht = new HashTableSeperateChaining<string, string>(10);
            foreach (var i in array) ht.Insert(i, i);

            Assert.Throws<ArgumentNullException>(() => ht.TryRemove(null, out _));
        }

        [Theory]
        [InlineData(new object[] { new string[] { } })]
        [InlineData(new object[] { new string[] { "1", "11" } })]
        [InlineData(new object[] { new string[] { "12", "22", "32", "42" } })]
        public void Clear_ResetsSizeToZeroAndIsNowEmpty(string[] array)
        {
            var ht = new HashTableSeperateChaining<string, string>(10);
            foreach (var i in array) ht.Insert(i, i);

            ht.Clear();

            Assert.Equal(0, ht.Size);
            Assert.True(ht.IsEmpty);
        }

        [Theory]
        [InlineData(new object[] { new string[] { } })]
        [InlineData(new object[] { new string[] { "1", "11" } })]
        [InlineData(new object[] { new string[] { "12", "22", "32", "42" } })]
        public void Clear_KeysNoLongerContainedInHashTable(string[] array)
        {
            var ht = new HashTableSeperateChaining<string, string>(10);
            foreach (var i in array) ht.Insert(i, i);

            ht.Clear();

            foreach (var i in array) Assert.False(ht.ContainsKey(i));
        }

        [Theory]
        [InlineData(new object[] { new string[] { } })]
        [InlineData(new object[] { new string[] { "1", "11" } })]
        [InlineData(new object[] { new string[] { "12", "22", "32", "42" } })]
        public void Clear_CanStillAddItemsAfterClear(string[] array)
        {
            var ht = new HashTableSeperateChaining<string, string>(10);
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
            var ht = new HashTableSeperateChaining<string, string>(10);
            foreach (var i in array) ht.Insert(i, i);

            var listOfKeys = ht.GetKeys().ToList();

            Assert.Equal(array.Length, listOfKeys.Count);
            foreach (var i in array) Assert.Contains(i, listOfKeys);
        }

        [Theory]
        [InlineData(new string[] { }, new int[] { })]
        [InlineData(new string[] { "1", "11" }, new int[] { 20, 21 })]
        [InlineData(new string[] { "12", "22", "32", "42" }, new int[] { 90, 100, 101, 500})]
        public void GetValues_ReturnsAllTheValuesAdded(string[] array, int[]  valueArray)
        {
            var ht = new HashTableSeperateChaining<string, int>();
            for (var i = 0; i < array.Length; i++) ht.Insert(array[i], valueArray[i]);

            var listOfValues = ht.GetValues().ToList();

            Assert.Equal(valueArray.Length, listOfValues.Count);
            foreach (var i in valueArray) Assert.Contains(i, listOfValues);

        }
    }
}
