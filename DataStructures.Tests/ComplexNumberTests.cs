using DataStructures.Library;
using Xunit;

namespace DataStructures.Tests
{
    public class ComplexNumberTests
    {
        // Complex number contains two components, a real and an imaginary component
        // it is displayed in the form a+bi where a is the real part and b is the imaginary part
        // abs(a + bi) == sqrt(a**2 + b**2)
        // (a + bi) + (c + di) == (a + c) + (b + d)i
        // (a + bi) * (c + di) == (a * c - b * d) + (a * d + b * c)i
        // -(a + bi) == (-a) + (-b)i

        [Fact]
        public void ComplexNumberShouldHaveARealAndImaginaryComponent()
        {
            var realComponent = 1d;
            var imaginaryComponent = 2d;

            var complexNumber = new ComplexNumber(realComponent, imaginaryComponent);

            Assert.Equal(realComponent, complexNumber.realComponent);
            Assert.Equal(imaginaryComponent, complexNumber.imaginaryComponent);
        }

        [Theory]
        [InlineData(3, 4, 5)]
        public void Abs_ReturnsAppropriateValue(double realComponent, double imaginaryComponent, double expected)
        {
            var complexNumber = new ComplexNumber(realComponent, imaginaryComponent);

            var result = complexNumber.Abs();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(3, 4, 5, 6, 8, 10)]
        public void Add_ReturnsAppropriateComplexNumber(double r1, double i1, double r2, double i2, double er, double ei)
        {
            var complexNumberOne = new ComplexNumber(r1, i1);
            var complexNumberTwo = new ComplexNumber(r2, i2);

            var complexResult = complexNumberOne.Add(complexNumberTwo);

            Assert.Equal(er, complexResult.realComponent);
            Assert.Equal(ei, complexResult.imaginaryComponent);
        }

        [Theory]
        [InlineData(3, 4, 5, 6, -9, 38)]
        public void Multiply_ReturnsAppropriateComplexNumber(double r1, double i1, double r2, double i2, double er, double ei)
        {
            var complexNumberOne = new ComplexNumber(r1, i1);
            var complexNumberTwo = new ComplexNumber(r2, i2);

            var complexResult = complexNumberOne.Multiply(complexNumberTwo);

            Assert.Equal(er, complexResult.realComponent);
            Assert.Equal(ei, complexResult.imaginaryComponent);
        }


        [Theory]
        [InlineData(3, 4, -3, -4)]
        public void Negative_ReturnsAppropriateComplexNumber(double r1, double i1, double er, double ei)
        {
            var complexNumber = new ComplexNumber(r1, i1);

            var complexResult = complexNumber.Negative();

            Assert.Equal(er, complexResult.realComponent);
            Assert.Equal(ei, complexResult.imaginaryComponent);
        }
    }
}
