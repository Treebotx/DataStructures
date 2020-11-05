using DataStructures.Library;
using System;
using System.Linq;
using Xunit;

namespace DataStructures.Tests
{
    public class PriorityQueueTests
    {
        [Fact]
        public void PriorityQueueStartsEmptyWithALengthOfZero()
        {
            var pq = new PriorityQueue<int>();

            Assert.Equal(0, pq.Size);
            Assert.True(pq.IsEmpty);
        }

        [Fact]
        public void Clear_ResetsTheSizeAndMakesEmpty()
        {
            var pq = new PriorityQueue<int>();
            pq.Add(555);

            pq.Clear();

            Assert.Equal(0, pq.Size);
            Assert.True(pq.IsEmpty);
        }

        [Fact]
        public void Add_OnANewPriorityQueueSetsSizeToOneAndIsNoLongerEmpty()
        {
            var pq = new PriorityQueue<int>();

            pq.Add(555);

            Assert.Equal(1, pq.Size);
            Assert.False(pq.IsEmpty);
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3 })]
        [InlineData(new int[] { 3, 2, 3 })]
        [InlineData(new int[] { 50, 4, 3, 32, 9, 10, 1 })]
        public void Add_IncreasesSizeOfQueueByOne(int[] array)
        {
            var pq = new PriorityQueue<int>();
            foreach (var item in array) pq.Add(item);
            var expected = array.Length + 1;

            pq.Add(555);

            Assert.Equal(expected, pq.Size);
        }

        [Fact]
        public void Peek_ThrowsInvalidOperationExceptionWhenEmpty()
        {
            var pq = new PriorityQueue<int>();

            Assert.Throws<InvalidOperationException>(() => pq.Peek());
        }

        [Fact]
        public void Peek_OnAPriorityQueueWithOneElementReturnsThatElement()
        {
            var pq = new PriorityQueue<int>();

            pq.Add(555);

            Assert.Equal(555, pq.Peek());
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 2, 3 }, 1)]
        [InlineData(new int[] { 3, 2, 3 }, 2)]
        [InlineData(new int[] { 50, 4, 3, 32, 9, 10, 1 }, 1)]
        public void Peek_ReturnsSmallestElement(int[] array, int expected)
        {
            var pq = new PriorityQueue<int>();
            foreach (var item in array) pq.Add(item);

            Assert.Equal(expected, pq.Peek());
        }

        [Fact]
        public void Poll_ThrowsInvalidOperationExceptionWhenEmpty()
        {
            var pq = new PriorityQueue<int>();

            Assert.Throws<InvalidOperationException>(() => pq.Poll());
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 2, 3 }, 1)]
        [InlineData(new int[] { 3, 2, 3 }, 2)]
        [InlineData(new int[] { 50, 4, 3, 32, 9, 10, 1 }, 1)]
        public void Poll_ReturnsSmallestElement(int[] array, int expected)
        {
            var pq = new PriorityQueue<int>();
            foreach (var item in array) pq.Add(item);

            Assert.Equal(expected, pq.Poll());
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, 2)]
        [InlineData(new int[] { 3, 2, 3 }, 3)]
        [InlineData(new int[] { 50, 4, 3, 32, 9, 10, 1 }, 3)]
        public void Poll_SecondPollReturnsNextSmallestElement(int[] array, int expected)
        {
            var pq = new PriorityQueue<int>();
            foreach (var item in array) pq.Add(item);
            pq.Poll();

            Assert.Equal(expected, pq.Poll());
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 2 }, 2)]
        [InlineData(new int[] { 3, 2, 3 }, 3)]
        [InlineData(new int[] { 50, 4, 3, 32, 9, 10, 1 }, 32)]
        public void Contains_ReturnsTrueIfItemIsFound(int[] array, int itemToFind)
        {
            var pq = new PriorityQueue<int>();
            foreach (var item in array) pq.Add(item);

            Assert.True(pq.Contains(itemToFind));
        }

        [Theory]
        [InlineData(new int[] { }, 1)]
        [InlineData(new int[] { 1 }, 2)]
        [InlineData(new int[] { 1, 2 }, 555)]
        [InlineData(new int[] { 3, 2, 3 }, 555)]
        [InlineData(new int[] { 50, 4, 3, 32, 9, 10, 1 }, 555)]
        public void Contains_ReturnsFalseIfItemIsNotFound(int[] array, int itemToFind)
        {
            var pq = new PriorityQueue<int>();
            foreach (var item in array) pq.Add(item);

            Assert.False(pq.Contains(itemToFind));
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 2 }, 2)]
        [InlineData(new int[] { 3, 2, 3 }, 2)]
        [InlineData(new int[] { 50, 4, 3, 32, 9, 10, 1 }, 9)]
        public void Remove_ReturnsTrueIfItemIsRemovedFromQueue(int[] array, int itemToRemove)
        {
            var pq = new PriorityQueue<int>();
            foreach (var item in array) pq.Add(item);

            Assert.True(pq.Remove(itemToRemove));
            Assert.False(pq.Contains(itemToRemove));
        }

        [Theory]
        [InlineData(new int[] { }, 1)]
        [InlineData(new int[] { 1 }, 555)]
        [InlineData(new int[] { 1, 2 }, 555)]
        [InlineData(new int[] { 3, 2, 3 }, 555)]
        [InlineData(new int[] { 50, 4, 3, 32, 9, 10, 1 }, 555)]
        public void Remove_ReturnsFalseIfItemIsNotInTheQueue(int[] array, int itemToRemove)
        {
            var pq = new PriorityQueue<int>();
            foreach (var item in array) pq.Add(item);

            Assert.False(pq.Remove(itemToRemove));
            Assert.Equal(array.Length, pq.Size);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(555)]
        [InlineData(1000)]
        public void AddANumberOfRandomElementsAndCheckItIsMinHeap(int nbrOfElements)
        {
            var random = new Random();
            var pq = new PriorityQueue<int>(nbrOfElements);
            foreach (var i in Enumerable.Range(0, nbrOfElements)) pq.Add(random.Next());

            Assert.True(pq.IsItMinHeap());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(555)]
        [InlineData(1000)]
        public void AddANumberOfRandomElementsUsingCollectionConstructorAndCheckItIsMinHeap(int nbrOfElements)
        {
            var random = new Random();
            var collection = Enumerable.Range(0, nbrOfElements).Select(x => random.Next()).ToArray();
            var pq = new PriorityQueue<int>(collection);

            Assert.True(pq.IsItMinHeap());
        }
    }
}
