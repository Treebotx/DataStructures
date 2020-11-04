using DataStructures.Library;
using System;
using Xunit;

namespace DataStructures.Tests
{
    public class QueueTests
    {
        [Fact]
        public void QueueStartsEmptyWithALengthOfZero()
        {
            var q = new MyQueue<int>();

            Assert.Equal(0, q.Length);
            Assert.True(q.IsEmpty);
        }

        [Fact]
        public void Offer_SetsLengthOfEmptyStackToOneAndMakesNonEmpty()
        {
            var q = new MyQueue<int>();

            q.Offer(555);

            Assert.Equal(1, q.Length);
            Assert.False(q.IsEmpty);
        }

        [Theory]
        [InlineData(new int[] { 1, 2 }, 2)]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, 6)]
        public void Offer_AddsItemToTheEndOfTheQueue(int[] array, int expected)
        {
            var q = new MyQueue<int>();
            foreach (var item in array) q.Offer(item);

            int lastItem = 0;
            for (int i = 0; i < array.Length; i++)
            {
                lastItem = q.Poll();
            }

            Assert.Equal(expected, lastItem);
        }

        [Fact]
        public void Peek_ReturnsFirstElementInQueue()
        {
            var q = new MyQueue<int>();

            q.Offer(555);

            Assert.Equal(555, q.Peek());
        }

        [Theory]
        [InlineData(new int[] { 1, 2 }, 1, 2)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, 3, 4)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, 5, 6)]
        public void Peek_ReturnsAppropriateElementAfterMultiplePolls(int[] array, int timesToPoll, int expected)
        {
            var q = new MyQueue<int>();
            foreach (var item in array) q.Offer(item);

            for (int i = 0; i < timesToPoll; i++) q.Poll();

            Assert.Equal(expected, q.Peek());
        }

        [Fact]
        public void Peek_ThrowsInvalidOperationExceptionWhenEmpty()
        {
            var q = new MyQueue<int>();

            Assert.Throws<InvalidOperationException>(() => q.Peek());
        }

        [Fact]
        public void Poll_ReturnsFirstElementInQueue()
        {
            var q = new MyQueue<int>();

            q.Offer(555);

            Assert.Equal(555, q.Poll());
        }

        [Theory]
        [InlineData(new int[] { 1, 2 }, 2, 2)]
        [InlineData(new int[] { 1 }, 1, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, 3, 3)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, 6, 6)]
        public void Poll_MultiplePollsReturnsAppropriateElement(int[] array, int timesToPoll, int expected)
        {
            var q = new MyQueue<int>();
            foreach (var item in array) q.Offer(item);

            var result = 0;
            for (int i = 0; i < timesToPoll; i++) result = q.Poll();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Poll_ThrowsInvalidOperationExceptionWhenEmpty()
        {
            var q = new ArrQueue<int>();

            Assert.Throws<InvalidOperationException>(() => q.Poll());
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        public void CanUseIterator(int[] array)
        {
            var q = new ArrQueue<int>();
            foreach (var item in array) q.Offer(item);

            var i = 0;
            foreach (var item in q)
            {
                Assert.Equal(array[i], item);
                i++;
            }

            Assert.Equal(array.Length, i);
        }
    }
}
