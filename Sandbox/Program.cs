using DataStructures.Library;
using System;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {


            int nextPowerOfTwo(int value)
            {
                var x = Math.Ceiling(Math.Log(value, 2));
                return (int)(Math.Pow(2, x));
            }
            //new Mandy().Run();

            //PrimeNumberCalculator.Run();

            //var x = 5;
            //Console.WriteLine(6.IsPrime());

            var x = nextPowerOfTwo(17);
            Console.WriteLine(x);

            var bst = new BinarySearchTree<int>(new int[] { 7, 0, 3, 6, 2, 1, 9, 4, 5, 8 });

            Console.WriteLine($"Height: {bst.Height}");
            Console.Write("Pre Order Traversal: ");
            foreach (var i in bst.Traverse(TreeTraversalOrder.PRE_ORDER)) Console.Write($"{i}, ");
            Console.Write("\nIn  Order Traversal: ");
            foreach (var i in bst) Console.Write($"{i}, ");
            Console.Write("\nPostOrder Traversal: ");
            foreach (var i in bst.Traverse(TreeTraversalOrder.POST_ORDER)) Console.Write($"{i}, ");
            Console.Write("\nLvl Order Traversal: ");
            foreach (var i in bst.Traverse(TreeTraversalOrder.LEVEL_ORDER)) Console.Write($"{i}, ");
        }
    }
}
