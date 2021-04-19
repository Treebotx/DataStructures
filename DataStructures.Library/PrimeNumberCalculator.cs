using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Library
{
    public static class PrimeExtensions
    {
        public static bool IsPrime(this int number) => PrimeNumberCalculator.IsPrime(number);
    }

    public class PrimeNumberCalculator
    {
        public static bool IsPrime(int number)
        {
            if (number == 2) return true;

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }

            return true;
        }

        public static List<int> PrimeSieve(int max)
        {
            var primes = new List<int>();

            var candidates = new bool[max];

            // obvious optimisation:
            // assume 2 is prime, then just check the odds...
            primes.Add(2);

            for (int i = 3; i < max; i+=2)
            {
                if (candidates[i - 1] == false)
                {
                    primes.Add(i);
                    for (int k = (i - 1); k < max; k += i)
                    {
                        candidates[k] = true;
                    }
                }
            }

            return primes;
        }

        private static IEnumerable<int> NaturalNumbers(int start)
        {
            yield return start;
            foreach (var n in NaturalNumbers(start + 1))
            {
                yield return n;
            }
        }

        private static IEnumerable<int> Sieve(IEnumerable<int> s)
        {
            var n = s.First();

            yield return n;

            foreach (var p in Sieve(s.Skip(1).Where(x => x % n != 0)))
            {
                yield return p;
            }
        }

        public static IEnumerable<int> EnumeratePrimes()
        {
            foreach(var n in Sieve(NaturalNumbers(2)))
            {
                yield return n;
            }
        }
    }

    public class PrimeNumberRunner
    {
        public static void Run()
        {
            var primes = PrimeNumberCalculator.PrimeSieve(200);

            foreach (var p in primes)
            {
                Console.Write($"{p}, ");
            }

            Console.WriteLine();

            Console.WriteLine("no input to end...");

            while (true)
            {
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) break;

                if (int.TryParse(input, out var result))
                {
                    Console.WriteLine($"{result} is{(PrimeNumberCalculator.IsPrime(result) ? "" : " not")} a prime");
                }
            }

            Console.WriteLine("input to end...");

            foreach (var n in PrimeNumberCalculator.EnumeratePrimes())
            {
                Console.Write(n);
                var input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input)) break;
            }
        }
    }
}
