using DataStructures.Library;
using System;
using Xunit;

namespace DataStructures.Tests
{
    public class StackTests
    {
        [Fact]
        public void StackStartsEmptyWithALengthOfZero()
        {
            var s = new MyStack<int>();

            Assert.Equal(0, s.Length);
            Assert.True(s.IsEmpty);
        }

        [Fact]
        public void Push_SetsLengthOfEmptyStackToOneAndMakesNonEmpty()
        {
            var s = new MyStack<int>();

            s.Push(1);

            Assert.Equal(1, s.Length);
            Assert.False(s.IsEmpty);
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        public void Push_IncrementsLengthOfStack(int[] array)
        {
            var s = new MyStack<int>();
            foreach (var i in array) s.Push(i);
            var expected = s.Length + 1;

            s.Push(1);

            Assert.Equal(expected, s.Length);
            Assert.False(s.IsEmpty);
        }

        [Fact]
        public void Peek_ThrowsInvalidOperationExceptionWhenEmpty()
        {
            var s = new MyStack<int>();

            Assert.Throws<InvalidOperationException>(() => s.Peek());
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        public void Peek_ReturnsLastItemAddedToStack(int[] array)
        {
            var s = new MyStack<int>();
            foreach (var i in array) s.Push(i);

            s.Push(555);

            Assert.Equal(555, s.Peek());
        }

        [Fact]
        public void Pop_ThrowsInvalidOperationExceptionWhenEmpty()
        {
            var s = new MyStack<int>();

            Assert.Throws<InvalidOperationException>(() => s.Pop());
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        public void Pop_ReturnsLastItemAddedToStack(int[] array)
        {
            var s = new MyStack<int>();
            foreach (var i in array) s.Push(i);

            s.Push(555);

            Assert.Equal(555, s.Pop());
        }

        [Theory]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        public void Pop_DecrementsLengthOfStackByOne(int[] array)
        {
            var s = new MyStack<int>();
            foreach (var i in array) s.Push(i);
            var expected = s.Length - 1;

            s.Pop();

            Assert.Equal(expected, s.Length);
        }


        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        public void Pop_AfterPopPeekShouldReturnPreviousItemPassed(int[] array)
        {
            var s = new MyStack<int>();
            foreach (var i in array) s.Push(i);
            var expected = s.Length - 1;
            s.Push(555);
            s.Push(777);

            s.Pop();

            Assert.Equal(555, s.Peek());
        }

        [Fact]
        public void Pop_StackShouldBeEmptyIfPopOnStackWithOneItem()
        {
            var s = new MyStack<int>();
            s.Push(555);
            s.Pop();

            Assert.Equal(0, s.Length);
            Assert.True(s.IsEmpty);
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        public void CanUseIterator(int[] array)
        {
            var s = new MyStack<int>();
            foreach (var item in array) s.Push(item);

            var i = array.Length;
            foreach(var item in s)
            {
                i--;
                Assert.Equal(array[i], item);
            }

            Assert.Equal(0, i);
        }
    }
}
