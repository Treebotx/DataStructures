using DataStructures.Library;
using System;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            // Returns the value of the least significant bit (LSB)
            // lsb(108) = lsb(0b1101100) =     0b100 = 4
            // lsb(104) = lsb(0b1101000) =    0b1000 = 8
            // lsb(96)  = lsb(0b1100000) =  0b100000 = 32
            // lsb(64)  = lsb(0b1000000) = 0b1000000 = 64
            int lsb(int i)
            {
                return i & -i;
            }

            int nextPowerOfTwo(int value)
            {
                var x = Math.Ceiling(Math.Log(value, 2));
                return (int)(Math.Pow(2, x));
            }

            var sa1 = new SuffixArray("KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
            var sa2 = new SuffixArray("ABABABAABB");
            var sa3 = new SuffixArray("ABBABAABAA");
            var sa4 = new SuffixArray("BAAAAB0ABAAAAB1BABA2ABA3AAB4BBBB5BB");
            var sa5 = new SuffixArray("ABCDE");
            //for (int i = 2, k = 0; i < 10; i++, k++) Console.WriteLine($"i:{i}, k:{k}");

            Console.WriteLine(sa1);
            Console.WriteLine(sa2);
            Console.WriteLine(sa3);
            Console.WriteLine(sa4);
            Console.WriteLine(sa5);

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

            Console.WriteLine($"lsb(108) = 4: {lsb(108)}");
            Console.WriteLine($"lsb(104) = 8: {lsb(104)}");
            Console.WriteLine($"lsb(96) = 32: {lsb(96)}");
            Console.WriteLine($"lsb(64) = 64: {lsb(64)}");
        }
    }
}
