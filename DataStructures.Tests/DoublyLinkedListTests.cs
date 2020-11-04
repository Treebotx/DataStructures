using DataStructures.Library;
using System;
using Xunit;

namespace DataStructures.Tests
{
    public class DoublyLinkedListTests
    {
        [Fact]
        public void DoublyLinkedListStartsEmptyWithALengthOfZero()
        {
            var d = new DoublyLinkedList<int>();

            Assert.Equal(0, d.Length);
            Assert.True(d.IsEmpty);
        }

        [Fact]
        public void AddFirst_IncrementsLength()
        {
            var d = new DoublyLinkedList<int>();
            var expected = d.Length + 1;

            d.AddFirst(1);

            Assert.Equal(expected, d.Length);
        }

        [Fact]
        public void AddLast_IncrementsLength()
        {
            var d = new DoublyLinkedList<int>();
            var expected = d.Length + 1;

            d.AddLast(1);

            Assert.Equal(expected, d.Length);
        }

        [Fact]
        public void Add_IncrementsLength()
        {
            var d = new DoublyLinkedList<int>();
            var expected = d.Length + 1;

            d.Add(1);

            Assert.Equal(expected, d.Length);
        }

        [Theory]

        [InlineData(new int[] { }, 555)]
        [InlineData(new int[] { 1 }, 555)]
        [InlineData(new int[] { 1, 2 }, 555)]
        [InlineData(new int[] { 5, 4, 3, 2, 1 }, 555)]
        public void Add_AddsTheItemToTheList(int[] array, int itemToAdd)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);

            d.Add(itemToAdd);

            Assert.True(d.Contains(itemToAdd));
        }

        [Fact]
        public void PeekFirst_ThrowsAnExceptionWhenEmpty()
        {
            var d = new DoublyLinkedList<int>();

            Assert.Throws<InvalidOperationException>(() => d.PeekFirst());
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 2 }, 1)]
        [InlineData(new int[] { 5, 4, 3, 2, 1 }, 5)]
        public void PeekFirst_ReturnsFirstItemInTheList(int[] array, int expected)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);

            var result = d.PeekFirst();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void PeekLast_ThrowsAnExceptionWhenEmpty()
        {
            var d = new DoublyLinkedList<int>();

            Assert.Throws<InvalidOperationException>(() => d.PeekLast());
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 2 }, 2)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 5)]
        public void PeekLast_ReturnsLastItemInTheList(int[] array, int expected)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);

            var result = d.PeekLast();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1, 2 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        public void AddFirst_AddsAnItemToTheBeginningOfTheList(int[] array)
        {
            var expected = 555;

            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);

            d.AddFirst(expected);

            var result = d.PeekFirst();
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1, 2 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        public void AddLast_AddsAnItemToTheEndOfTheList(int[] array)
        {
            var expected = 555;

            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);

            d.AddLast(expected);

            var result = d.PeekLast();
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1, 2 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        public void Clear_SetsLengthToZeroAndMakesListEmpty(int[] array)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);

            d.Clear();

            Assert.Equal(0, d.Length);
            Assert.True(d.IsEmpty);
        }

        [Theory]
        [InlineData(new int[] { }, 1, -1)]
        [InlineData(new int[] { 1 }, 1, 0)]
        [InlineData(new int[] { 1 }, 555, -1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 5, 4)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 1, 0)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 3, 2)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 555, -1)]
        public void IndexOf_ReturnsIndexOfFirstOccuranceOfItemOrMinusOneIfNotFound(int[] array, int itemToFind, int expected)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);

            var result = d.IndexOf(itemToFind);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 5)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 3)]
        public void Contains_ReturnsTrueIfAnOccuranceOfItemIsFound(int[] array, int itemToFind)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);

            Assert.True(d.Contains(itemToFind));
        }

        [Theory]
        [InlineData(new int[] { }, 1)]
        [InlineData(new int[] { 1 }, 555)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 555)]
        public void Contains_ReturnsFalseIfAnOccuranceOfItemIsNotFound(int[] array, int itemToFind)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);

            Assert.False(d.Contains(itemToFind));
        }

        [Fact]
        public void RemoveLast_EmptysListThatOnlyContainsOneItem()
        {
            var d = new DoublyLinkedList<int>();
            d.Add(5);

            d.RemoveLast();

            Assert.True(d.IsEmpty);
        }

        [Fact]
        public void RemoveLast_ThrowsInvalidOperationExceptionWhenEmpty()
        {
            var d = new DoublyLinkedList<int>();

            Assert.Throws<InvalidOperationException>(() => d.RemoveLast());
        }

        [Theory]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        public void RemoveLast_DecreasesLengthOfListByOne(int[] array)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);
            var expected = d.Length - 1;

            d.RemoveLast();

            Assert.Equal(expected, d.Length);
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        public void RemoveLast_ReturnsTheLastItemInTheArray(int[] array)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);
            
            d.AddLast(555);

            Assert.Equal(555, d.RemoveLast());
        }

        [Theory]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        public void RemoveLast_RemovesTheLastItemInTheArray(int[] array)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);
            d.AddLast(555);

            d.RemoveLast();

            Assert.Equal(array[array.Length - 1], d.PeekLast());
        }

        [Fact]
        public void RemoveFirst_EmptysListThatOnlyContainsOneItem()
        {
            var d = new DoublyLinkedList<int>();
            d.Add(5);

            d.RemoveFirst();

            Assert.True(d.IsEmpty);
        }

        [Fact]
        public void RemoveFirst_ThrowsInvalidOperationExceptionWhenEmpty()
        {
            var d = new DoublyLinkedList<int>();

            Assert.Throws<InvalidOperationException>(() => d.RemoveFirst());
        }

        [Theory]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        public void RemoveFirst_DecreasesLengthOfListByOne(int[] array)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);
            var expected = d.Length - 1;

            d.RemoveFirst();

            Assert.Equal(expected, d.Length);
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        public void RemoveFirst_ReturnsTheFirstItemInTheArray(int[] array)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);
            d.AddFirst(555);

            Assert.Equal(555, d.RemoveFirst());
        }

        [Theory]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        public void RemoveFirst_RemovesTheFirstItemInTheArray(int[] array)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);
            d.AddFirst(555);

            d.RemoveFirst();

            Assert.Equal(array[0], d.PeekFirst());
        }

        [Fact]
        public void Remove_EmptysListThatOnlyContainsOneItem()
        {
            var d = new DoublyLinkedList<int>();
            d.Add(5);

            d.Remove(5);

            Assert.True(d.IsEmpty);
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 3)]
        public void Remove_DecreasesLengthOfListByOne(int[] array, int itemToRemove)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);
            var expected = d.Length - 1;

            d.Remove(itemToRemove);

            Assert.Equal(expected, d.Length);
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 3)]
        public void Remove_ReturnsTrueIfItemWasInTheList(int[] array, int itemToRemove)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);
            
            Assert.True(d.Remove(itemToRemove));
        }

        [Theory]
        [InlineData(new int[] { }, 555)]
        [InlineData(new int[] { 1 }, 555)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 555)]
        public void Remove_ReturnsFalseIfItemWasNotInTheList(int[] array, int itemToRemove)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);

            Assert.False(d.Remove(itemToRemove));
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 3)]
        public void Remove_RemovesTheItemFromTheList(int[] array, int itemToRemove)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);

            d.Remove(itemToRemove);

            Assert.False(d.Contains(itemToRemove));
        }

        [Fact]
        public void RemoveAt_EmptysListThatOnlyContainsOneItem()
        {
            var d = new DoublyLinkedList<int>();
            d.Add(5);

            d.RemoveAt(0);

            Assert.True(d.IsEmpty);
        }

        [Theory]
        [InlineData(new int[] { 1 }, 0)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 2)]
        public void RemoveAt_DecreasesLengthOfListByOne(int[] array, int indexToRemove)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);
            var expected = d.Length - 1;

            d.RemoveAt(indexToRemove);

            Assert.Equal(expected, d.Length);
        }

        [Theory]
        [InlineData(new int[] { 1 }, 0, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 3, 4)]
        public void RemoveAt_ReturnsItemAtTheIndexRemoved(int[] array, int indexToRemove, int expected)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);

            var result = d.RemoveAt(indexToRemove);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new int[] { }, 0)]
        [InlineData(new int[] { 1 }, -1)]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1 }, 555)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, -1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 5)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 555)]
        public void RemoveAt_ThrowsIndexOutOfRangeExceptionIfTheIndexIsInvalid(int[] array, int indexToRemove)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);

            Assert.Throws<IndexOutOfRangeException>(() => d.RemoveAt(indexToRemove));
        }

        [Theory]
        [InlineData(new int[] { 1 }, 0, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 3, 4)]
        public void RemoveAt_RemovesTheItemFromTheList(int[] array, int indexToRemove, int itemToRemove)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);

            d.RemoveAt(indexToRemove);

            Assert.False(d.Contains(itemToRemove));
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        [InlineData(new int[] { })]
        public void CanUseIterator(int[] array)
        {
            var d = new DoublyLinkedList<int>();
            foreach (var item in array) d.Add(item);

            var i = 0;
            foreach (var item in d)
            {
                Assert.Equal(array[i], item);
                i++;
            }

            Assert.Equal(i, array.Length);
        }
    }
}
