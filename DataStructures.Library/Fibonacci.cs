using System.Collections.Generic;

namespace DataStructures.Library
{
    public static class Fibonacci
    {
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
