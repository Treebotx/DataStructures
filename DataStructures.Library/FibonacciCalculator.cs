using System;
using System.Collections.Generic;

namespace DataStructures.Library
{
    public static class FibonacciCalculator
    {
        

        public static Func<long, long> Fibonacci = Memoizer.Memoize((long n) =>
        {
            if (n <= 2) return 1;
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        });

        public static long CalculateFibonacci(long n)
        {
            var memo = new Dictionary<long, long>();
            return CalcFibonacci(n, memo);
        }

        private static long CalcFibonacci(long n, IDictionary<long, long> memo)
        {
            if (memo.TryGetValue(n, out var value)) return value;
            if (n <= 2) return 1;

            var result = CalcFibonacci(n - 1, memo) + CalcFibonacci(n - 2, memo);

            memo.Add(n, result);
            return result;
        }
    }
}
