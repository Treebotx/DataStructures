using DataStructures.Library;
using System;
using Xunit;

namespace DataStructures.Tests
{
    public class FenwickTreeTests
    {
        [Fact]
        public void FenwickTreeThrowsArgumentNullExceptionWhenValuesArrayIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new FenwickTree(null));
        }

        [Fact]
        public void Sum_ThrowsArgumentExceptionIfRightIsLessThanLeft()
        {
            var ft = new FenwickTree(10);
            
            Assert.Throws<ArgumentException>(() => ft.Sum(2, 1));
        }

        [Theory]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 1, 6, 21)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 1, 5, 15)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 1, 4, 10)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 1, 3, 6)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 1, 2, 3)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 1, 1, 1)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 3, 4, 7)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 4, 5, 9)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 6, 6, 6)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 5, 5, 5)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 4, 4, 4)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 3, 3, 3)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 2, 2, 2)]
        public void Sum_CorrectlySumsPositiveValues(long[] values, int left, int right, long expected)
        {
            var ft = new FenwickTree(values);

            Assert.Equal(expected, ft.Sum(left, right));
        }

        [Theory]
        [InlineData(new long[] { 0, -1, -2, -3, -4, -5, -6 }, 1, 6, -21)]
        [InlineData(new long[] { 0, -1, -2, -3, -4, -5, -6 }, 1, 5, -15)]
        [InlineData(new long[] { 0, -1, -2, -3, -4, -5, -6 }, 1, 4, -10)]
        [InlineData(new long[] { 0, -1, -2, -3, -4, -5, -6 }, 1, 3, -6)]
        [InlineData(new long[] { 0, -1, -2, -3, -4, -5, -6 }, 1, 2, -3)]
        [InlineData(new long[] { 0, -1, -2, -3, -4, -5, -6 }, 1, 1, -1)]
        [InlineData(new long[] { 0, -1, -2, -3, -4, -5, -6 }, 3, 4, -7)]
        [InlineData(new long[] { 0, -1, -2, -3, -4, -5, -6 }, 4, 5, -9)]
        [InlineData(new long[] { 0, -1, -2, -3, -4, -5, -6 }, 6, 6, -6)]
        [InlineData(new long[] { 0, -1, -2, -3, -4, -5, -6 }, 5, 5, -5)]
        [InlineData(new long[] { 0, -1, -2, -3, -4, -5, -6 }, 4, 4, -4)]
        [InlineData(new long[] { 0, -1, -2, -3, -4, -5, -6 }, 3, 3, -3)]
        [InlineData(new long[] { 0, -1, -2, -3, -4, -5, -6 }, 2, 2, -2)]
        [InlineData(new long[] { 0, -76871, -164790 }, 1, 1, -76871)]
        [InlineData(new long[] { 0, -76871, -164790 }, 1, 2, -241661)]
        [InlineData(new long[] { 0, -76871, -164790 }, 2, 2, -164790)]
        public void Sum_CorrectlySumsNegativeValues(long[] values, int left, int right, long expected)
        {
            var ft = new FenwickTree(values);

            Assert.Equal(expected, ft.Sum(left, right));
        }

        [Theory]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 6, 6)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 5, 5)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 4, 4)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 3, 3)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 2, 2)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 1, 1)]
        public void Get_CorrectlyGetsValueAtIndex(long[] values, int index, long expected)
        {
            var ft = new FenwickTree(values);

            Assert.Equal(expected, ft.Get(index));
        }

        [Theory]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 1, 6, 7)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 2, 2, 4)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 3, 20, 23)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 4, 100, 104)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 5, 0, 5)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 6, 99, 105)]
        public void Add_CorrectlyAddsValuesToIndex(long[] values, int index, int valueToAdd, long expected)
        {
            var ft = new FenwickTree(values);
            var expectedTotal = ft.Sum(1, (values.Length - 1)) + valueToAdd;

            ft.Add(index, valueToAdd);

            Assert.Equal(expected, ft.Get(index));
            Assert.Equal(expectedTotal, ft.Sum(1, (values.Length - 1)));
        }

        [Theory]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 1, 6)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 2, 2)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 3, 20)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 4, 100)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 5, 0)]
        [InlineData(new long[] { 0, 1, 2, 3, 4, 5, 6 }, 6, 99)]
        public void Set_SetsValueAtIndex(long[] values, int index, long value)
        {
            var ft = new FenwickTree(values);

            ft.Set(index, value);

            Assert.Equal(value, ft.Get(index));
        }
    }
}
