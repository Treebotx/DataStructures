using System;

namespace DataStructures.Library
{
    public class RationalNumber
    {
        private static int GreatestCommonDivisor(int a, int b)
        {
            //while (a != b)
            //{
            //    if (a > b)
            //    {
            //        a -= b;
            //    }
            //    else
            //    {
            //        b -= a;
            //    }
            //}

            //return a;

            return (a == 0) ? b : (GreatestCommonDivisor((b % a), a));
        }

        public int Numerator { get; }
        public int Denominator { get; }

        public RationalNumber(int numerator, int denominator)
        {

            if (denominator == 0)
            {
                throw new InvalidOperationException("Denominator should not be zero");
            }

            if (denominator < 0)
            {
                numerator *= -1;
                denominator *= -1;
            }

            var gcd = GreatestCommonDivisor(Math.Abs(numerator), Math.Abs(denominator));

            this.Numerator = numerator / gcd;
            this.Denominator = denominator / gcd;
        }

        public RationalNumber Add(RationalNumber rhs)
        {
            return Add(rhs.Numerator, rhs.Denominator);
        }

        private RationalNumber Add(int rhsNumerator, int rhsDenominator)
        {
            int numerator = this.Numerator * rhsDenominator + this.Denominator * rhsNumerator;
            int denominator = this.Denominator * rhsDenominator;

            return new RationalNumber(numerator, denominator);
        }

        public RationalNumber Subtract(RationalNumber rationalNumber)
        {
            return Add(rationalNumber.Numerator, rationalNumber.Denominator * -1);
        }

        public RationalNumber Multiply(RationalNumber rhs)
        {
            return Multiply(rhs.Numerator, rhs.Denominator);
        }

        private RationalNumber Multiply(int rhsNumerator, int rhsDenominator)
        {
            return new RationalNumber(this.Numerator * rhsNumerator, this.Denominator * rhsDenominator);
        }

        public RationalNumber Divide(RationalNumber rhs)
        {
            return Multiply(rhs.Denominator, rhs.Numerator);
        }
    }
}
