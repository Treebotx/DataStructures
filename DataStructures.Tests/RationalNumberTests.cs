using DataStructures.Library;
using System;
using Xunit;

namespace DataStructures.Tests
{
    public class RationalNumberTests
    {
        [Theory]
        [InlineData(1, 2, 1)]
        [InlineData(10, 11, 10)]
        [InlineData(3, 4, 3)]
        [InlineData(-3, 4, -3)]
        [InlineData(3, -4, -3)]
        [InlineData(4, 3, 4)]
        public void NumeratorShouldBeTheFirstParameterInTheConstructor(int numerator, int denominator, int expected)
        {
            var rational = new RationalNumber(numerator, denominator);

            Assert.Equal(expected, rational.Numerator);
        }

        [Theory]
        [InlineData(1, 2, 2)]
        [InlineData(10, 11, 11)]
        [InlineData(3, 4, 4)]
        [InlineData(3, -4, 4)]
        public void DenominatorShouldBeTheSecondParameterInTheConstructor(int numerator, int denominator, int expected)
        {
            var rational = new RationalNumber(numerator, denominator);

            Assert.Equal(expected, rational.Denominator);
        }

        [Theory]
        [InlineData(2, 4, 1)]
        [InlineData(10, 100, 1)]
        [InlineData(12, 16, 3)]
        [InlineData(1032, 1976, 129)]
        [InlineData(10, 10, 1)]
        [InlineData(-2, 4, -1)]
        [InlineData(5000, 2500, 2)]
        [InlineData(400, 300, 4)]
        public void NumeratorShouldBeAutomaticallyReducedIfRequired(int numerator, int denominator, int expectedNumerator)
        {
            var rational = new RationalNumber(numerator, denominator);

            Assert.Equal(expectedNumerator, rational.Numerator);
        }

        [Theory]
        [InlineData(1, 4)]
        [InlineData(11, 100)]
        [InlineData(3, 4)]
        public void NumeratorShouldNotChangeIfAlreadyReduced(int numerator, int denominator)
        {
            var rational = new RationalNumber(numerator, denominator);

            Assert.Equal(numerator, rational.Numerator);
        }

        [Theory]
        [InlineData(2, 4, 2)]
        [InlineData(10, 100, 10)]
        [InlineData(12, 16, 4)]
        [InlineData(1032, 1976, 247)]
        [InlineData(10, 10, 1)]
        [InlineData(5000, 2500, 1)]
        [InlineData(400, 300, 3)]
        public void DenominatorShouldBeAutomaticallyReducedIfRequired(int numerator, int denominator, int expectedDenominator)
        {
            var rational = new RationalNumber(numerator, denominator);

            Assert.Equal(expectedDenominator, rational.Denominator);
        }


        [Theory]
        [InlineData(1, 4)]
        [InlineData(11, 100)]
        [InlineData(3, 4)]
        public void DenominatorShouldNotChangeIfAlreadyReduced(int numerator, int denominator)
        {
            var rational = new RationalNumber(numerator, denominator);

            Assert.Equal(denominator, rational.Denominator);
        }


        [Fact]
        public void ShouldThrowWithInvalidOperationExceptionIfDenominatorIsZero()
        {
            Assert.Throws<InvalidOperationException>(() => new RationalNumber(4, 0));
        }

        // Add - numerator result = n1 * d2 + n2 * d1 - reduced
        [Theory]
        [InlineData(1, 3, 1, 3, 2)]
        [InlineData(1, 2, 3, 4, 5)]
        [InlineData(3, 1, 1, 3, 10)]
        [InlineData(1, 3, -1, 3, 0)]
        [InlineData(1, 2, -1, 4, 1)]
        [InlineData(1, 2, -3, 4, -1)]
        public void Add_ReturnsAppropriateNumerator(int n1, int d1, int n2, int d2, int expected)
        {
            var rational = new RationalNumber(n1, d1).Add(new RationalNumber(n2, d2));

            Assert.Equal(expected, rational.Numerator);
        }

        // Add - denominator result = d1 * d2 - reduced
        [Theory]
        [InlineData(1, 3, 1, 3, 3)]
        [InlineData(1, 2, 3, 4, 4)]
        [InlineData(3, 1, 1, 3, 3)]
        [InlineData(1, 3, -1, 3, 1)]
        [InlineData(1, 2, -1, 4, 4)]
        [InlineData(1, 2, -3, 4, 4)]
        public void Add_ReturnsAppropriateDenominator(int n1, int d1, int n2, int d2, int expected)
        {
            var rational = new RationalNumber(n1, d1).Add(new RationalNumber(n2, d2));

            Assert.Equal(expected, rational.Denominator);
        }

        // Subtract - numerator result = n1 * d2 - n2 * d1 - reduced
        [Theory]
        [InlineData(1, 3, 1, 3, 0)]
        [InlineData(1, 2, 3, 4, -1)]
        [InlineData(3, 1, 1, 3, 8)]
        [InlineData(1, 3, -1, 3, 2)]
        [InlineData(1, 2, -1, 4, 3)]
        [InlineData(1, 2, -3, 4, 5)]
        public void Subtract_ReturnsAppropriateNumerator(int n1, int d1, int n2, int d2, int expected)
        {
            var rational = new RationalNumber(n1, d1).Subtract(new RationalNumber(n2, d2));

            Assert.Equal(expected, rational.Numerator);
        }

        // Add - denominator result = d1 * d2 - reduced
        [Theory]
        [InlineData(1, 3, 1, 3, 1)]
        [InlineData(1, 2, 3, 4, 4)]
        [InlineData(3, 1, 1, 3, 3)]
        [InlineData(1, 3, -1, 3, 3)]
        [InlineData(1, 2, -1, 4, 4)]
        [InlineData(1, 2, -3, 4, 4)]
        public void Subtract_ReturnsAppropriateDenominator(int n1, int d1, int n2, int d2, int expected)
        {
            var rational = new RationalNumber(n1, d1).Subtract(new RationalNumber(n2, d2));

            Assert.Equal(expected, rational.Denominator);
        }

        // Multiply - numerator result = n1 * n2 - reduced
        [Theory]
        [InlineData(1, 3, 1, 3, 1)]
        [InlineData(1, 2, 3, 4, 3)]
        [InlineData(3, 1, 1, 3, 1)]
        [InlineData(1, 3, -1, 3, -1)]
        [InlineData(1, 2, -1, 4, -1)]
        [InlineData(1, 2, -3, 4, -3)]
        public void Multiply_ReturnsAppropriateNumerator(int n1, int d1, int n2, int d2, int expected)
        {
            var rational = new RationalNumber(n1, d1).Multiply(new RationalNumber(n2, d2));

            Assert.Equal(expected, rational.Numerator);
        }

        // Multiply - denominator result = d1 * d2 - reduced
        [Theory]
        [InlineData(1, 3, 1, 3, 9)]
        [InlineData(1, 2, 3, 4, 8)]
        [InlineData(3, 1, 1, 3, 1)]
        [InlineData(1, 3, -1, 3, 9)]
        [InlineData(1, 2, -1, 4, 8)]
        [InlineData(1, 2, -3, 4, 8)]
        public void Multiply_ReturnsAppropriateDenominator(int n1, int d1, int n2, int d2, int expected)
        {
            var rational = new RationalNumber(n1, d1).Multiply(new RationalNumber(n2, d2));

            Assert.Equal(expected, rational.Denominator);
        }

        // Divide - numerator result = n1 * d2 - reduced
        [Theory]
        [InlineData(1, 3, 1, 3, 1)]
        [InlineData(1, 2, 3, 4, 2)]
        [InlineData(3, 1, 1, 3, 9)]
        [InlineData(1, 3, -1, 3, -1)]
        [InlineData(1, 2, -1, 4, -2)]
        [InlineData(1, 2, -3, 4, -2)]
        public void Divide_ReturnsAppropriateNumerator(int n1, int d1, int n2, int d2, int expected)
        {
            var rational = new RationalNumber(n1, d1).Divide(new RationalNumber(n2, d2));

            Assert.Equal(expected, rational.Numerator);
        }

        // Divide - denominator result = d1 * n2 - reduced
        [Theory]
        [InlineData(1, 3, 1, 3, 1)]
        [InlineData(1, 2, 3, 4, 3)]
        [InlineData(3, 1, 1, 3, 1)]
        [InlineData(1, 3, -1, 3, 1)]
        [InlineData(1, 2, -1, 4, 1)]
        [InlineData(1, 2, -3, 4, 3)]
        public void Divide_ReturnsAppropriateDenominator(int n1, int d1, int n2, int d2, int expected)
        {
            var rational = new RationalNumber(n1, d1).Divide(new RationalNumber(n2, d2));

            Assert.Equal(expected, rational.Denominator);
        }
    }
}
