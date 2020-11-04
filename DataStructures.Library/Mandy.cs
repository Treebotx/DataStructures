using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Library
{
    public class Mandy
    {
        private readonly int maxLoop = 100;
        private readonly double magnitudeLimit = 2;

        public void Run()
        {
            var constant = new ComplexNumber(0.5, 0.3);

            var cn = new ComplexNumber(0, 0);

            var loopCount = IterationCountAt(cn, constant);

            Console.WriteLine($"{cn}, {constant} loopCount: {loopCount}");

            var complexNumbers = ComplexNumbersAt(cn, constant);

            foreach (var complexNumber in complexNumbers)
            {
                Console.WriteLine($"{complexNumber}");
            }
        }

        public int IterationCountAt(ComplexNumber square, ComplexNumber constant)
        {
            var cn = square;

            int loopCount = 0;
            while (loopCount < maxLoop && cn.Abs() < magnitudeLimit)
            {
                cn = cn.Multiply(cn).Add(constant);
                loopCount++;

                var magnitude = cn.Abs();
                Console.WriteLine($"abs({cn}) == {magnitude}");
            }

            return loopCount;
        }

        public List<ComplexNumber> ComplexNumbersAt(ComplexNumber square, ComplexNumber constant)
        {
            var cn = square;

            var result = new List<ComplexNumber>();

            int loopCount = 0;
            while (loopCount < maxLoop && cn.Abs() < magnitudeLimit)
            {
                result.Add(cn);
                cn = cn.Multiply(cn).Add(constant);
                loopCount++;
            }

            return result;
        }
    }
}
