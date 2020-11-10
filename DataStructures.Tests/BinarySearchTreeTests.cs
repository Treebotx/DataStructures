using DataStructures.Library;
using Xunit;

namespace DataStructures.Tests
{
    public class BinarySearchTreeTests
    {
        [Fact]
        public void NewTreeIsEmptyWithASizeAndHeightOfZero()
        {
            var bst = new BinarySearchTree<int>();

            Assert.Equal(0, bst.Size);
            Assert.Equal(0, bst.Height);
            Assert.True(bst.IsEmpty);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 })]
        public void TreeCanBeInitializedFromAnArray(int[] array)
        {
            var bst = new BinarySearchTree<int>(array);

            Assert.Equal(array.Length, bst.Size);
            Assert.False(bst.IsEmpty);
        }

        [Fact]
        public void Add_AddingToANewTreeSetsSizeAndHeightToOneAndIsNoLongerEmpty()
        {
            var bst = new BinarySearchTree<int>();

            bst.Add(555);

            Assert.Equal(1, bst.Size);
            Assert.Equal(1, bst.Height);
            Assert.False(bst.IsEmpty);
        }

        [Theory]
        [InlineData(new int[] { }, 4)]
        [InlineData(new int[] { 1, 2, 3 }, 4)]
        public void Add_ReturnsTrueIfItemIsNotCurrentlyInTheTree(int[] array, int itemToAdd)
        {
            var bst = new BinarySearchTree<int>(array);

            Assert.True(bst.Add(itemToAdd));
        }

        [Theory]
        [InlineData(new int[] { }, 4)]
        [InlineData(new int[] { 1, 2, 3 }, 4)]
        public void Add_IncreasesSizeIfItemIsNotCurrentlyInTheTree(int[] array, int itemToAdd)
        {
            var bst = new BinarySearchTree<int>(array);
            var expected = bst.Size + 1;

            bst.Add(itemToAdd);

            Assert.Equal(expected, bst.Size);
        }

        [Theory]
        [InlineData(new int[] { 3 }, 3)]
        [InlineData(new int[] { 1, 2, 3 }, 3)]
        public void Add_ReturnsFalseIfItemIsCurrentlyInTheTree(int[] array, int itemToAdd)
        {
            var bst = new BinarySearchTree<int>(array);

            Assert.False(bst.Add(itemToAdd));
        }

        [Theory]
        [InlineData(new int[] { 3 }, 3)]
        [InlineData(new int[] { 1, 2, 3 }, 3)]
        public void Add_DoesNotIncreaseSizeIfItemIsCurrentlyInTheTree(int[] array, int itemToAdd)
        {
            var bst = new BinarySearchTree<int>(array);
            var expected = bst.Size;

            bst.Add(itemToAdd);

            Assert.Equal(expected, bst.Size);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, 3)]
        [InlineData(new int[] { 1, 2, 3 }, 2)]
        [InlineData(new int[] { 1, 2, 3 }, 1)]
        [InlineData(new int[] { 2, 3, 1 }, 1)]
        [InlineData(new int[] { 2, 3, 1 }, 2)]
        [InlineData(new int[] { 2, 3, 1 }, 3)]
        [InlineData(new int[] { 3, 2, 1 }, 1)]
        [InlineData(new int[] { 3, 2, 1 }, 2)]
        [InlineData(new int[] { 3, 2, 1 }, 3)]
        public void Contain_ReturnsTrueIfItemHasBeenAddedToTheTree(int[] array, int itemToFind)
        {
            var bst = new BinarySearchTree<int>(array);

            Assert.True(bst.Contains(itemToFind));
        }

        [Theory]
        [InlineData(new int[] { }, 555)]
        [InlineData(new int[] { 1 }, 2)]
        [InlineData(new int[] { 6, 7, 9, 10, 11, 65, 92 }, 1)]
        public void Contain_ReturnsFalseIfItemHasNotBeenAddedToTheTree(int[] array, int itemToFind)
        {
            var bst = new BinarySearchTree<int>(array);

            Assert.False(bst.Contains(itemToFind));
        }

        [Theory]
        [InlineData(new int[] { }, 555)]
        [InlineData(new int[] { 1 }, 2)]
        [InlineData(new int[] { 6, 7, 9, 10, 11, 65, 92 }, 1)]
        public void Remove_ReturnsFalseIfItemIsNotInTheTree(int[] array, int itemToRemove)
        {
            var bst = new BinarySearchTree<int>(array);

            Assert.False(bst.Remove(itemToRemove));
        }

        [Theory]
        [InlineData(new int[] { 555 }, 555)]
        [InlineData(new int[] { 1, 2 }, 2)]
        [InlineData(new int[] { 6, 7, 9, 10, 11, 65, 92 }, 10)]
        public void Remove_ReturnsTrueIfItemIsSuccessfullyRemovedFromTheTree(int[] array, int itemToRemove)
        {
            var bst = new BinarySearchTree<int>(array);

            Assert.True(bst.Remove(itemToRemove));
        }

        [Theory]
        [InlineData(new int[] { 555 }, 555)]
        [InlineData(new int[] { 1, 2 }, 2)]
        [InlineData(new int[] { 6, 7, 9, 10, 11, 65, 92 }, 10)]
        public void Remove_ItemIsNoLongerContainedInTreeAfterSuccessfulRemoval(int[] array, int itemToRemove)
        {
            var bst = new BinarySearchTree<int>(array);
            bst.Remove(itemToRemove);

            Assert.False(bst.Contains(itemToRemove));
        }

        [Theory]
        [InlineData(new char[] { 'M' }, 1)]
        [InlineData(new char[] { 'M', 'J' }, 2)]
        [InlineData(new char[] { 'M', 'J', 'S' }, 2)]
        [InlineData(new char[] { 'M', 'J', 'S', 'B' }, 3)]
        [InlineData(new char[] { 'M', 'J', 'S', 'B', 'N' }, 3)]
        [InlineData(new char[] { 'M', 'J', 'S', 'B', 'N', 'Z' }, 3)]
        [InlineData(new char[] { 'M', 'J', 'S', 'B', 'N', 'Z', 'A' }, 4)]
        public void Height_TestHeightIsReturnedCorrectly(char[] array, int expectedHeight)
        {
            var bst = new BinarySearchTree<char>(array);

            Assert.Equal(expectedHeight, bst.Height);
        }

        // Expected tree for traversal tests:
        //                M
        //          J           S
        //        B           N   Z
        //      A

        [Theory]
        [InlineData(new char[] { }, new char[] { })]
        [InlineData(new char[] { 'M', 'J', 'S', 'B', 'N', 'Z', 'A' }, new char[] {'A', 'B', 'J', 'M', 'N', 'S', 'Z'})]
        public void Traverse_InOrderTraversal(char[] array, char[] expected)
        {
            var bst = new BinarySearchTree<char>(array);

            var i = 0;
            foreach (var item in bst.Traverse(TreeTraversalOrder.IN_ORDER)) Assert.Equal(expected[i++], item);
            Assert.Equal(array.Length, i);
        }

        [Theory]
        [InlineData(new char[] { }, new char[] { })]
        [InlineData(new char[] { 'M', 'J', 'S', 'B', 'N', 'Z', 'A' }, new char[] { 'M', 'J', 'B', 'A', 'S', 'N', 'Z' })]
        public void Traverse_PreOrderTraversal(char[] array, char[] expected)
        {
            var bst = new BinarySearchTree<char>(array);

            var i = 0;
            foreach (var item in bst.Traverse(TreeTraversalOrder.PRE_ORDER)) Assert.Equal(expected[i++], item);
            Assert.Equal(array.Length, i);
        }

        [Theory]
        [InlineData(new char[] { }, new char[] { })]
        [InlineData(new char[] { 'M', 'J', 'S', 'B', 'N', 'Z', 'A' }, new char[] { 'A', 'B', 'J', 'N', 'Z', 'S', 'M' })]
        public void Traverse_PostOrderTraversal(char[] array, char[] expected)
        {
            var bst = new BinarySearchTree<char>(array);

            var i = 0;
            foreach (var item in bst.Traverse(TreeTraversalOrder.POST_ORDER)) Assert.Equal(expected[i++], item);
            Assert.Equal(array.Length, i);
        }

        [Theory]
        [InlineData(new char[] { }, new char[] { })]
        [InlineData(new char[] { 'M', 'J', 'S', 'B', 'N', 'Z', 'A' }, new char[] { 'M', 'J', 'S', 'B', 'N', 'Z', 'A' })]
        public void Traverse_LevelOrderTraversal(char[] array, char[] expected)
        {
            var bst = new BinarySearchTree<char>(array);

            var i = 0;
            foreach (var item in bst.Traverse(TreeTraversalOrder.LEVEL_ORDER)) Assert.Equal(expected[i++], item);
            Assert.Equal(array.Length, i);
        }
    }
}
