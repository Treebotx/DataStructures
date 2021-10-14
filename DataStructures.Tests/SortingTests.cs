using DataStructures.Library.Sorting;
using System;
using Xunit;

namespace DataStructures.Tests
{
    public abstract class SortTestsBase
    {
        public abstract ISorting<int> GetSortingInstance();

        [Theory]
        [InlineData(new int[] { 1, 2, 3 })]
        [InlineData(new int[] { 3, 2, 1 })]
        [InlineData(new int[] { 12, 14, 13, 11, 16, 10, 18, 17 })]
        [InlineData(new int[] { 7, 11, 10, 5, 12, 4, 18, 15 })]
        [InlineData(new int[] { 50, 23, 9, 18, 61, 32 })]
        [InlineData(new int[] { 1, 12, 9, 5, 6, 10 })]
        public void SortReturnsDataInOrder(int[] array)
        {
            var sort = GetSortingInstance();

            var expected = new int[array.Length];
            array.CopyTo(expected, 0);
            Array.Sort(expected);

            sort.Sort(array);

            Assert.Equal(expected, array);
        }
    }

    public class InbuiltSortTests : SortTestsBase
    {
        public override ISorting<int> GetSortingInstance()
        {
            return new InbuiltSort<int>();
        }
    }

    public class BubbleSortTests : SortTestsBase
    {
        public override ISorting<int> GetSortingInstance()
        {
            return new BubbleSort<int>();
        }
    }

    public class MergeSortTests : SortTestsBase
    {
        public override ISorting<int> GetSortingInstance()
        {
            return new MergeSort<int>();
        }
    }

    public class InsertionSortTests : SortTestsBase
    {
        public override ISorting<int> GetSortingInstance()
        {
            return new InsertionSort<int>();
        }
    }

    public class QuickSortTests : SortTestsBase
    {
        public override ISorting<int> GetSortingInstance()
        {
            return new QuickSort<int>();
        }
    }

    public class SelectionSortTests : SortTestsBase
    {
        public override ISorting<int> GetSortingInstance()
        {
            return new SelectionSort<int>();
        }
    }

    public class HeapSortTests : SortTestsBase
    {
        public override ISorting<int> GetSortingInstance()
        {
            return new HeapSort<int>();
        }
    }
}
