﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace DataStructures.Library
{
    public static class PrimeExtensions
    {
        public static bool IsPrime(this int number) => PrimeNumberCalculator.IsPrime(number);
    }

    public class PrimeNumberCalculator
    {
        public static void Run()
        {
            var primes = PrimeSieve(200);

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
                    Console.WriteLine($"{result} is{(IsPrime(result) ? "" : " not")} a prime");
                }
            }

            Console.WriteLine("input to end...");

            foreach (var n in EnumeratePrimes())
            {
                Console.Write(n);
                var input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input)) break;
            }
        }

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

        private static IEnumerable<int> nats(int start)
        {
            yield return start;
            foreach (var n in nats(start + 1))
            {
                yield return n;
            }
        }

        private static IEnumerable<int> sieve(IEnumerable<int> s)
        {
            var n = s.FirstOrDefault();

            if (n != default)
            {
                yield return n;

                foreach (var p in sieve(s.Skip(1).Where(x => x % n != 0)))
                {
                    yield return p;
                }
            }
        }

        public static IEnumerable<int> EnumeratePrimes()
        {
            foreach(var n in sieve(nats(2)))
            {
                yield return n;
            }
        }
    }
}
