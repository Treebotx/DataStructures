using DataStructures.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataStructures.Tests
{
    public class PrimeNumberCalculatorTests
    {
        [Theory]
        [InlineData(1, new int[] { })]
        [InlineData(2, new int[] { 2 })]
        [InlineData(3, new int[] { 3 })]
        [InlineData(4, new int[] { 2, 2 })]
        [InlineData(5, new int[] { 5 })]
        [InlineData(6, new int[] { 2, 3 })]
        [InlineData(7, new int[] { 7 })]
        [InlineData(8, new int[] { 2, 2, 2 })]
        [InlineData(9, new int[] { 3, 3 })]
        [InlineData((2 * 2 * 3 * 3 * 5 * 7 * 11 * 11 * 13), new int[] { 2, 2, 3, 3, 5, 7, 11, 11, 13 })]
        public void FactorsOfXContainsArray(int factor, int[] expected)
        {
            var results = PrimeNumberCalculator.PrimeFactorsOf(factor);

            Assert.Equal(expected, results);
        }
    }
}
