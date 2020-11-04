using System;

namespace DataStructures.Library
{
    public class ComplexNumber
    {
        public double realComponent { get; }
        public double imaginaryComponent { get; }

        public ComplexNumber(double realComponent, double imaginaryComponent)
        {
            this.realComponent = realComponent;
            this.imaginaryComponent = imaginaryComponent;
        }

        public double Abs()
        {
            return Math.Sqrt(realComponent * realComponent + imaginaryComponent * imaginaryComponent);
        }

        public ComplexNumber Add(ComplexNumber rhs)
        {
            var newReal = realComponent + rhs.realComponent;
            var newImg = imaginaryComponent + rhs.imaginaryComponent;

            return new ComplexNumber(newReal, newImg);
        }

        public ComplexNumber Multiply(ComplexNumber rhs)
        {
            // (a + bi) * (c + di) == (a * c - b * d) + (a * d + b * c)i
            var newReal = realComponent * rhs.realComponent - imaginaryComponent * rhs.imaginaryComponent;
            var newImg = realComponent * rhs.imaginaryComponent + imaginaryComponent * rhs.realComponent;

            return new ComplexNumber(newReal, newImg);
        }

        public ComplexNumber Negative()
        {
            return new ComplexNumber(-realComponent, -imaginaryComponent);
        }

        public override string ToString()
        {
            return $"{realComponent} + {imaginaryComponent}i";
        }
    }
}