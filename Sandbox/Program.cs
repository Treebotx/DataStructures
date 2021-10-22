using DataStructures.Library;
using DataStructures.Library.Sorting;
using System;
using System.Collections.Generic;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Fib 1: {FibonacciCalculator.Fibonacci(1)}");
            Console.WriteLine($"Fib 2: {FibonacciCalculator.Fibonacci(2)}");
            Console.WriteLine($"Fib 3: {FibonacciCalculator.Fibonacci(3)}");
            Console.WriteLine($"Fib 4: {FibonacciCalculator.Fibonacci(4)}");
            Console.WriteLine($"Fib 5: {FibonacciCalculator.Fibonacci(5)}");
            Console.WriteLine($"Fib 6: {FibonacciCalculator.Fibonacci(6)}");
            Console.WriteLine($"Fib 7: {FibonacciCalculator.Fibonacci(7)}");
            Console.WriteLine($"Fib 8: {FibonacciCalculator.Fibonacci(8)}");
            Console.WriteLine($"Fib 50: {FibonacciCalculator.Fibonacci(50)}");
            return;
            var list = new List<int> { 5, 4, 3, 2, 1 };
            var sorter = new InbuiltSort<int>();
            foreach (var item in list) Console.Write($"{item} : ");
            Console.WriteLine();
            sorter.Sort(list);
            foreach (var item in list) Console.Write($"{item} : ");
            Console.WriteLine();

            var trie = new Trie();
            trie.Add("abc");
            trie.Add("abgl");
            trie.Add("cdf");
            trie.Add("abcd");
            trie.Add("lmn");

            Console.WriteLine($"prefix ab {trie.DoesPrefixExist("ab")}");
            Console.WriteLine($"prefix lo {trie.DoesPrefixExist("lo")}");
            Console.WriteLine($"prefix lmn {trie.DoesPrefixExist("lmn")}");
            Console.WriteLine($"prefix cdf {trie.DoesPrefixExist("cdf")}");
            Console.WriteLine($"prefix ghi {trie.DoesPrefixExist("ghi")}");

            Console.WriteLine($"word lo {trie.DoesWordExist("lo")}");
            Console.WriteLine($"word ab {trie.DoesWordExist("ab")}");
            Console.WriteLine($"word lmn {trie.DoesWordExist("lmn")}");
            Console.WriteLine($"word cdf {trie.DoesWordExist("cdf")}");
            Console.WriteLine($"word ghi {trie.DoesWordExist("ghi")}");

            foreach (var item in trie.GetAllWithPrefix(""))
            {
                Console.WriteLine($" prefix: {item}");
            }

            //trie.RemoveWord("abc");

            //Console.WriteLine($"word abc {trie.DoesWordExist("abc")}");
            //Console.WriteLine($"word abgl {trie.DoesWordExist("abgl")}");
            //Console.WriteLine($"word abcd {trie.DoesWordExist("abcd")}");
            //trie.RemoveWord("abgl");

            //Console.WriteLine($"word abc {trie.DoesWordExist("abc")}");
            //Console.WriteLine($"word abgl {trie.DoesWordExist("abgl")}");
            //Console.WriteLine($"word abcd {trie.DoesWordExist("abcd")}");
            //trie.RemoveWord("abcd");

            //Console.WriteLine($"word abc {trie.DoesWordExist("abc")}");
            //Console.WriteLine($"word abgl {trie.DoesWordExist("abgl")}");
            //Console.WriteLine($"word abcd {trie.DoesWordExist("abcd")}");

            //Console.ReadLine();
            // Returns the value of the least significant bit (LSB)
            // lsb(108) = lsb(0b1101100) =     0b100 = 4
            // lsb(104) = lsb(0b1101000) =    0b1000 = 8
            // lsb(96)  = lsb(0b1100000) =  0b100000 = 32
            // lsb(64)  = lsb(0b1000000) = 0b1000000 = 64
            //int lsb(int i)
            //{
            //    return i & -i;
            //}

            //int nextPowerOfTwo(int value)
            //{
            //    var x = Math.Ceiling(Math.Log(value, 2));
            //    return (int)(Math.Pow(2, x));
            //}

            //var sa1 = new SuffixArray("KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
            //var sa2 = new SuffixArray("ABABABAABB");
            //var sa3 = new SuffixArray("ABBABAABAA");
            //var sa4 = new SuffixArray("BAAAAB0ABAAAAB1BABA2ABA3AAB4BBBB5BB");
            //var sa5 = new SuffixArray("ABCDE");
            //var sa6 = new SuffixArray("AZAZA");

            //var substrings = sa6.GetAllSubstrings();
            //Console.WriteLine($"Count of substrings: {substrings.Count}");
            //foreach (var item in substrings) Console.WriteLine(item);
            ////for (int i = 2, k = 0; i < 10; i++, k++) Console.WriteLine($"i:{i}, k:{k}");

            //Console.WriteLine(sa1);
            //Console.WriteLine(sa2);
            //Console.WriteLine(sa3);
            //Console.WriteLine(sa4);
            //Console.WriteLine(sa5);

            //new Mandy().Run();

            //PrimeNumberRunner.Run();

            //var x = 5;
            //Console.WriteLine(6.IsPrime());

            //var x = nextPowerOfTwo(17);
            //Console.WriteLine(x);

            //var bst = new BinarySearchTree<int>(new int[] { 7, 0, 3, 6, 2, 1, 9, 4, 5, 8 });

            //Console.WriteLine($"Height: {bst.Height}");
            //Console.Write("Pre Order Traversal: ");
            //foreach (var i in bst.Traverse(TreeTraversalOrder.PRE_ORDER)) Console.Write($"{i}, ");
            //Console.Write("\nIn  Order Traversal: ");
            //foreach (var i in bst) Console.Write($"{i}, ");
            //Console.Write("\nPostOrder Traversal: ");
            //foreach (var i in bst.Traverse(TreeTraversalOrder.POST_ORDER)) Console.Write($"{i}, ");
            //Console.Write("\nLvl Order Traversal: ");
            //foreach (var i in bst.Traverse(TreeTraversalOrder.LEVEL_ORDER)) Console.Write($"{i}, ");

            //Console.WriteLine($"lsb(108) = 4: {lsb(108)}");
            //Console.WriteLine($"lsb(104) = 8: {lsb(104)}");
            //Console.WriteLine($"lsb(96) = 32: {lsb(96)}");
            //Console.WriteLine($"lsb(64) = 64: {lsb(64)}");
        }
    }
}
