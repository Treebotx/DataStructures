using DataStructures.Library;
using System;
using Xunit;

namespace DataStructures.Tests
{
    public class DynamicArrayTests
    {
        [Fact]
        public void DynamicArrayStartsWithALengthOfZero()
        {
            var dynamicArray = new DynamicArray<int>();
            Assert.Equal(0, dynamicArray.Length);
        }

        [Fact]
        public void DynamicArrayStartsEmpty()
        {
            var dynamicArray = new DynamicArray<int>();
            Assert.True(dynamicArray.IsEmpty);
        }

        [Fact]
        public void Add_ToANewArraySetsLengthToOne()
        {
            var dynamicArray = new DynamicArray<int>();

            dynamicArray.Add(5);

            Assert.Equal(1, dynamicArray.Length);
        }

        [Fact]
        public void Add_AfterAddingToNewArrayItIsNoLongerEmpty()
        {
            var dynamicArray = new DynamicArray<int>();

            dynamicArray.Add(5);

            Assert.False(dynamicArray.IsEmpty);
        }

        [Fact]
        public void Add_ToAnArrayWithItemsIncreasesLengthByOne()
        {
            var dynamicArray = new DynamicArray<int>();
            dynamicArray.Add(5);

            dynamicArray.Add(6);

            Assert.Equal(2, dynamicArray.Length);
            Assert.False(dynamicArray.IsEmpty);
        }

        [Theory]
        [InlineData(new int[] { 1, 2 }, 2)]
        [InlineData(new int[] { 1, 2, 3, 4 }, 4)]
        public void Add_AddingManyItemsSetsLengthAppropriately(int[] array, int size)
        {
            var dynamicArray = new DynamicArray<int>();
            AddArrayToDynamicArray(dynamicArray, array);

            Assert.Equal(size, dynamicArray.Length);
            Assert.False(dynamicArray.IsEmpty);
        }

        [Theory]
        [InlineData(1, new int[] { 1, 2 }, 2)]
        [InlineData(2, new int[] { 1, 2, 3, 4 }, 4)]
        public void Add_AllowsAddingPastTheCapacity(int capacity, int[] array, int size)
        {
            var dynamicArray = new DynamicArray<int>(capacity);
            AddArrayToDynamicArray(dynamicArray, array);

            Assert.Equal(size, dynamicArray.Length);
            Assert.False(dynamicArray.IsEmpty);
        }

        [Fact]
        public void Clear_SetsLengthOfArrayToZeroAndMakesItEmpty()
        {
            var dynamicArray = new DynamicArray<int>();
            dynamicArray.Add(5);
            dynamicArray.Add(6);

            dynamicArray.Clear();

            Assert.Equal(0, dynamicArray.Length);
            Assert.True(dynamicArray.IsEmpty);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, 2)]
        [InlineData(new int[] { 2 }, 2)]
        [InlineData(new int[] { 1, 2, 3, 4 }, 4)]
        public void Contains_ReturnsTrueIfItemIsInArray(int[] array, int toFind)
        {
            var dynamicArray = new DynamicArray<int>();
            AddArrayToDynamicArray(dynamicArray, array);

            var result = dynamicArray.Contains(toFind);

            Assert.True(dynamicArray.Contains(toFind));
        }

        [Theory]
        [InlineData(1, new int[] { 1, 2, 3 }, 2)]
        [InlineData(1, new int[] { 2 }, 2)]
        [InlineData(2, new int[] { 1, 2, 3, 4 }, 1)]
        public void Contains_ReturnsTrueIfItemIsInArrayAfterIncreasingCapacity(int capacity, int[] array, int toFind)
        {
            var dynamicArray = new DynamicArray<int>(capacity);
            AddArrayToDynamicArray(dynamicArray, array);

            var result = dynamicArray.Contains(toFind);

            Assert.True(result);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, 10)]
        [InlineData(new int[] { 2 }, 9)]
        [InlineData(new int[] { 1, 2, 3, 4 }, 90)]
        public void Contains_ReturnsFalseIfItemIsInArray(int[] array, int toFind)
        {
            var dynamicArray = new DynamicArray<int>();
            AddArrayToDynamicArray(dynamicArray, array);

            var result = dynamicArray.Contains(toFind);

            Assert.False(result);
        }

        [Theory]
        [InlineData(1, new int[] { 1, 2, 3 }, 2, 3)]
        [InlineData(1, new int[] { 2 }, 0, 2)]
        [InlineData(2, new int[] { 1, 2, 3, 4 }, 1, 2)]
        [InlineData(16, new int[] { 1, 2, 3, 4 }, 3, 4)]
        public void Get_ReturnsItemAtIndex(int capacity, int[] array, int index, int expected)
        {
            var dynamicArray = new DynamicArray<int>(capacity);
            AddArrayToDynamicArray(dynamicArray, array);

            var result = dynamicArray.Get(index);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, new int[] { 1, 2, 3 }, 10)]
        [InlineData(16, new int[] { 2 }, 1)]
        [InlineData(16, new int[] { 1, 2, 3, 4 }, 4)]
        public void Get_ThrowsIndexOutOfRangeExceptionIfAttemptingAccessBeyondItsLength(int capacity, int[] array, int index)
        {
            var dynamicArray = new DynamicArray<int>(capacity);
            AddArrayToDynamicArray(dynamicArray, array);

            Assert.Throws<IndexOutOfRangeException>(() => dynamicArray.Get(index));
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, 3, 2)]
        [InlineData(new int[] { 2 }, 1, -1)]
        [InlineData(new int[] { 1, 2, 3, 4 }, 1, 0)]
        [InlineData(new int[] { 1, 2, 3, 4, 2 }, 2, 1)]
        [InlineData(new int[] { 1, 1, 2, 2, 2 }, 2, 2)]
        [InlineData(new int[] { 2, 2, 2, 2, 2 }, 2, 0)]
        public void IndexOf_ReturnsTheIndexOfTheFirstOccuranceOfItemOrMinusOneIfNotFound(int[] array, int itemToFind, int expected)
        {
            var dynamicArray = new DynamicArray<int>();
            AddArrayToDynamicArray(dynamicArray, array);

            var result = dynamicArray.IndexOf(itemToFind);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void RemoveAt_IfArrayContainsOneItemItIsEmptyAfterRemove()
        {
            var dynamicArray = new DynamicArray<int>();
            var expected = 5;
            dynamicArray.Add(expected);

            var result = dynamicArray.RemoveAt(0);

            Assert.True(dynamicArray.IsEmpty);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 3, 4)]
        [InlineData(new int[] { 1 }, 0, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 4, 5)]
        public void RemoveAt_ReturnsItemAtIndex(int[] array, int indexToRemove, int expected)
        {
            var dynamicArray = new DynamicArray<int>();
            AddArrayToDynamicArray(dynamicArray, array);

            var result = dynamicArray.RemoveAt(indexToRemove);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 5)]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { }, 0)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 25)]
        public void RemoveAt_ThrowsIndexOutOfRangeIfRemovingFromBeyondLength(int[] array, int indexToRemove)
        {
            var dynamicArray = new DynamicArray<int>();
            AddArrayToDynamicArray(dynamicArray, array);

            Assert.Throws<IndexOutOfRangeException>(() => dynamicArray.RemoveAt(indexToRemove));
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 0, 4)]
        [InlineData(new int[] { 1 }, 0, 0)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 4, 4)]
        [InlineData(new int[] { 1, 2 }, 1, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 4, 7)]
        public void RemoveAt_AfterSuccessfulRemovalLengthIsDecreasedByOne(int[] array, int indexToRemove, int expected)
        {
            var dynamicArray = new DynamicArray<int>();
            AddArrayToDynamicArray(dynamicArray, array);

            dynamicArray.RemoveAt(indexToRemove);

            Assert.Equal(expected, dynamicArray.Length);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 0)]
        [InlineData(new int[] { 1 }, 0)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 4)]
        [InlineData(new int[] { 1, 2 }, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 4)]
        public void RemoveAt_ItemsAfterIndexShouldBeShiftedIntoSpaceLeft(int[] array, int indexToRemove)
        {
            var dynamicArray = new DynamicArray<int>();
            AddArrayToDynamicArray(dynamicArray, array);

            dynamicArray.RemoveAt(indexToRemove);

            for (int i = indexToRemove; i < dynamicArray.Length; i++)
            {
                Assert.Equal(array[i + 1], dynamicArray.Get(i));
            }
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 0)]
        [InlineData(new int[] { 1 }, 0)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 4)]
        [InlineData(new int[] { 1, 2 }, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 4)]
        public void RemoveAt_ItemsBeforeIndexShouldBeRemainTheSame(int[] array, int indexToRemove)
        {
            var dynamicArray = new DynamicArray<int>();
            AddArrayToDynamicArray(dynamicArray, array);

            dynamicArray.RemoveAt(indexToRemove);

            for (int i = 0; i < indexToRemove; i++)
            {
                Assert.Equal(array[i], dynamicArray.Get(i));
            }
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 1, new int[] { 2, 3, 4, 5 })]
        [InlineData(new int[] { 1 }, 1, new int[] { })]
        [InlineData(new int[] { 1, 2, 3, 4, 3 }, 3, new int[] { 1, 2, 4, 3 })]
        [InlineData(new int[] { 1, 2 }, 2, new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 2, 2, 2, 2, 2, 2 }, 2, new int[] { 1, 2, 2, 2, 2, 2, 2 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 6, new int[] { 1, 2, 3, 4, 5 })]
        public void Remove_RemovesFirstOccuranceOfItem(int[] array, int itemToRemove, int[] expectedArray)
        {
            var dynamicArray = new DynamicArray<int>();
            AddArrayToDynamicArray(dynamicArray, array);

            dynamicArray.Remove(itemToRemove);

            for (int i = 0; i < dynamicArray.Length; i++)
            {
                Assert.Equal(expectedArray[i], dynamicArray.Get(i));
            }
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 3)]
        [InlineData(new int[] { 1 }, 1)]
        public void Remove_ReturnsTrueIfItemFoundAndRemoved(int[] array, int itemToRemove)
        {
            var dynamicArray = new DynamicArray<int>();
            AddArrayToDynamicArray(dynamicArray, array);

            Assert.True(dynamicArray.Remove(itemToRemove));
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 6)]
        [InlineData(new int[] { 1 }, 3)]
        [InlineData(new int[] { }, 3)]
        public void Remove_ReturnsFalseIfItemNotFound(int[] array, int itemToRemove)
        {
            var dynamicArray = new DynamicArray<int>();
            AddArrayToDynamicArray(dynamicArray, array);

            Assert.False(dynamicArray.Remove(itemToRemove));
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 2, 6)]
        [InlineData(new int[] { 1 }, 0, 20)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 4, 10)]
        public void Set_ChangesItemAtIndexToNewItem(int[] array, int indexToChange, int newItem)
        {
            var dynamicArray = new DynamicArray<int>();
            AddArrayToDynamicArray(dynamicArray, array);

            dynamicArray.Set(indexToChange, newItem);

            Assert.Equal(newItem, dynamicArray.Get(indexToChange));
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 5)]
        [InlineData(new int[] { }, 0)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 50)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, -1)]
        public void Set_ThrowsIndexOutOfRangeExceptionWhenSettingItemAfterTheEnd(int[] array, int indexToChange)
        {
            var dynamicArray = new DynamicArray<int>();
            AddArrayToDynamicArray(dynamicArray, array);

            Assert.Throws<IndexOutOfRangeException>(() => dynamicArray.Set(indexToChange, 4));
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        [InlineData(new int[] { })]
        public void CanUseIterator(int[] array)
        {
            var dynamicArray = new DynamicArray<int>();
            AddArrayToDynamicArray(dynamicArray, array);

            var i = 0;
            foreach(var item in dynamicArray)
            {
                Assert.Equal(array[i], item);
                i++;
            }

            Assert.Equal(array.Length, i);
        }

        public void AddArrayToDynamicArray(DynamicArray<int> dynmaicArray, int[] fromArray)
        {
            foreach (var item in fromArray)
            {
                dynmaicArray.Add(item);
            }
        }
    }
}
