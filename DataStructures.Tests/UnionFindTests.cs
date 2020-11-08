using DataStructures.Library;
using Xunit;

namespace DataStructures.Tests
{
    public class UnionFindTests
    {
        [Fact]
        public void NewUnionFindHasASizeOfZero()
        {
            var uf = new UnionFind<int>();

            Assert.Equal(0, uf.Size);
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 })]
        public void InitializingUnionFindWithArraySetsSizeAndComponentCountToSizeOfArray(int[] array)
        {
            var uf = new UnionFind<int>(array);

            Assert.Equal(array.Length, uf.Size);
            Assert.Equal(array.Length, uf.ComponentCount);
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 })]
        public void InitializingUnionFindWithArraySetsSizeSizeOfEachComponentToOne(int[] array)
        {
            var uf = new UnionFind<int>(array);

            Assert.Equal(array.Length, uf.Size);
            Assert.Equal(array.Length, uf.ComponentCount);
        }

        [Fact]
        public void Add_AddingAnItemToANewUnionFindSetsSizeAndComponentCountToOne()
        {
            var uf = new UnionFind<int>();

            uf.Add(555);

            Assert.Equal(1, uf.Size);
            Assert.Equal(1, uf.ComponentCount);
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 })]
        public void Add_AddingItemsIncreasesSizeAndComponentCountByOne(int[] array)
        {
            var uf = new UnionFind<int>(array);
            var expectedSize = uf.Size + 1;
            var expectedComponentCount = uf.ComponentCount + 1;

            uf.Add(555);

            Assert.Equal(expectedSize, uf.Size);
            Assert.Equal(expectedComponentCount, uf.ComponentCount);
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 })]
        public void FindComponent_ComponentForANewItemShouldBeTheSizeOfTheUnionFind(int[] array)
        {
            var uf = new UnionFind<int>(array);
            var expected = uf.Size;

            uf.Add(555);

            Assert.Equal(expected, uf.FindComponent(555));
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 })]
        public void ComponentSize_ComponentSizeForANewItemShouldBeOne(int[] array)
        {
            var uf = new UnionFind<int>(array);

            uf.Add(555);

            Assert.Equal(1, uf.ComponentSize(555));
        }

        [Theory]
        [InlineData(new int[] { 1, 2 }, 1, 2)]
        [InlineData(new int[] { 1, 2 }, 2, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, 3, 6)]
        public void ComponentSize_ShouldBeIncreasedByOneAfterBeingAddedTo(int[] array, int item1, int item2)
        {
            var uf = new UnionFind<int>(array);
            var expected = uf.ComponentSize(item1) + 1;

            uf.Union(item1, item2);

            Assert.Equal(expected, uf.ComponentSize(item1));
        }

        [Theory]
        [InlineData(new int[] { 1, 2 }, 1, 2)]
        [InlineData(new int[] { 1, 2 }, 2, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, 3, 6)]
        public void Union_JoiningTwoItemsShouldDecreaseComponentCountByOne(int[] array, int item1, int item2)
        {
            var uf = new UnionFind<int>(array);
            var expected = uf.ComponentCount - 1;

            uf.Union(item1, item2);

            Assert.Equal(expected, uf.ComponentCount);
        }

        [Theory]
        [InlineData(new int[] { 1, 2 }, 1, 2)]
        [InlineData(new int[] { 1, 2 }, 2, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, 3, 6)]
        public void Union_IfNoUnionsSetUpSecondItemShouldBelongToFirstItemsComponent(int[] array, int item1, int item2)
        {
            var uf = new UnionFind<int>(array);

            uf.Union(item1, item2);

            Assert.Equal(uf.FindComponent(item1), uf.FindComponent(item2));
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2 }, new int[] { 3 }, 2, 3)]
        [InlineData(new int[] { 1, 2 }, new int[] { 1 }, new int[] { 2 }, 2, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 }, 3, 6)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 2, 3, 4, 5 }, new int[] { 6 }, 3, 6)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 2, 3 }, new int[] { 4, 5, 6 }, 6, 3)]
        public void Union_SmallestComponentShouldBeAddedToLargestComponent(int[] array, int[] component1, int[] component2, int item1, int item2)
        {
            var uf = new UnionFind<int>(array);
            UnioniseElementsFromArray(uf, component1);
            UnioniseElementsFromArray(uf, component2);

            uf.Union(item1, item2);

            Assert.Equal(uf.FindComponent(item1), uf.FindComponent(item2));
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2 }, new int[] { 3 }, 2, 3)]
        [InlineData(new int[] { 1, 2 }, new int[] { 1 }, new int[] { 2 }, 2, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 }, 3, 6)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 2, 3, 4, 5 }, new int[] { 6 }, 3, 6)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 2, 3 }, new int[] { 4, 5, 6 }, 6, 3)]
        public void Union_ComponentSizeOfItemBeingJoinedToShouldBeIncreasedByComponentSizeOfItemBeingAdded(int[] array, int[] component1, int[] component2, int item1, int item2)
        {
            var uf = new UnionFind<int>(array);
            UnioniseElementsFromArray(uf, component1);
            UnioniseElementsFromArray(uf, component2);
            var expected = component1.Length + component2.Length;

            uf.Union(item1, item2);

            Assert.Equal(expected, uf.ComponentSize(item1));
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 }, 2, 3)]
        [InlineData(new int[] { 1, 2 }, new int[] { 1, 2 }, 2, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 1, 2, 3 }, 3, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 2, 3, 4, 5 }, 3, 4)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 2, 3 }, 2, 3)]
        public void Union_ComponentSizeShouldNotChangeIfItemsAlreadyInTheSameComponent(int[] array, int[] component, int item1, int item2)
        {
            var uf = new UnionFind<int>(array);
            UnioniseElementsFromArray(uf, component);
            var expected = component.Length;

            uf.Union(item1, item2);

            Assert.Equal(expected, uf.ComponentSize(item1));
        }
        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 }, 2, 3)]
        [InlineData(new int[] { 1, 2 }, new int[] { 1, 2 }, 2, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 1, 2, 3 }, 3, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 2, 3, 4, 5 }, 3, 4)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 2, 3 }, 2, 3)]
        public void Union_ComponentCountShouldNotChangeIfItemsAlreadyInTheSameComponent(int[] array, int[] component, int item1, int item2)
        {
            var uf = new UnionFind<int>(array);
            UnioniseElementsFromArray(uf, component);
            var expected = uf.ComponentCount;

            uf.Union(item1, item2);

            Assert.Equal(expected, uf.ComponentCount);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2 }, 1, 2)]
        [InlineData(new int[] { 1, 2 }, new int[] { 1, 2 }, 2, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 1, 2, 3, 4, 5, 6 }, 3, 6)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 2, 5 }, 2, 5)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 2, 3 }, 2, 3)]
        public void Connected_ReturnsTrueIfItemsAreInTheSameComponent(int[] array, int[] component, int item1, int item2)
        {
            var uf = new UnionFind<int>(array);
            UnioniseElementsFromArray(uf, component);
            uf.Union(item1, item2);

            Assert.True(uf.Connected(item1, item2));
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2 }, 1, 3)]
        [InlineData(new int[] { 1, 2 }, new int[] { }, 2, 1)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 1, 2, 5, 6 }, 3, 4)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 2, 5 }, 1, 5)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 2, 3 }, 3, 6)]
        public void Connected_ReturnsFalseIfItemsAreNotInTheSameComponent(int[] array, int[] component, int item1, int item2)
        {
            var uf = new UnionFind<int>(array);
            UnioniseElementsFromArray(uf, component);

            Assert.False(uf.Connected(item1, item2));
        }

        private void UnioniseElementsFromArray(UnionFind<int> uf, int[] elementsToUnionise)
        {
            if (elementsToUnionise.Length == 1) return;

            for (var i = 1; i < elementsToUnionise.Length; i++)
            {
                uf.Union(elementsToUnionise[i - 1], elementsToUnionise[i]);
            }
        }

        // Following tests modified from github.com/williamfiset/Algorithms
        [Fact]
        public void ComponentChangesCorrectly()
        {
            var uf = new UnionFind<int>(new int[] { 0, 1, 2, 3, 4 });

            Assert.Equal(5, uf.ComponentCount);

            uf.Union(0, 1);
            Assert.Equal(4, uf.ComponentCount);

            uf.Union(1, 0);
            Assert.Equal(4, uf.ComponentCount);

            uf.Union(1, 2);
            Assert.Equal(3, uf.ComponentCount);

            uf.Union(0, 2);
            Assert.Equal(3, uf.ComponentCount);

            uf.Union(2, 1);
            Assert.Equal(3, uf.ComponentCount);

            uf.Union(3, 4);
            Assert.Equal(2, uf.ComponentCount);

            uf.Union(4, 3);
            Assert.Equal(2, uf.ComponentCount);

            uf.Union(1, 3);
            Assert.Equal(1, uf.ComponentCount);

            uf.Union(4, 0);
            Assert.Equal(1, uf.ComponentCount);
        }

        [Fact]
        public void ComponentSizeChangesCorrectly()
        {

            var uf = new UnionFind<int>(new int[] { 0, 1, 2, 3, 4 });

            Assert.Equal(1, uf.ComponentSize(0));
            Assert.Equal(1, uf.ComponentSize(1));
            Assert.Equal(1, uf.ComponentSize(2));
            Assert.Equal(1, uf.ComponentSize(3));
            Assert.Equal(1, uf.ComponentSize(4));

            uf.Union(0, 1);
            Assert.Equal(2, uf.ComponentSize(0));
            Assert.Equal(2, uf.ComponentSize(1));
            Assert.Equal(1, uf.ComponentSize(2));
            Assert.Equal(1, uf.ComponentSize(3));
            Assert.Equal(1, uf.ComponentSize(4));

            uf.Union(1, 0);
            Assert.Equal(2, uf.ComponentSize(0));
            Assert.Equal(2, uf.ComponentSize(1));
            Assert.Equal(1, uf.ComponentSize(2));
            Assert.Equal(1, uf.ComponentSize(3));
            Assert.Equal(1, uf.ComponentSize(4));

            uf.Union(1, 2);
            Assert.Equal(3, uf.ComponentSize(0));
            Assert.Equal(3, uf.ComponentSize(1));
            Assert.Equal(3, uf.ComponentSize(2));
            Assert.Equal(1, uf.ComponentSize(3));
            Assert.Equal(1, uf.ComponentSize(4));

            uf.Union(0, 2);
            Assert.Equal(3, uf.ComponentSize(0));
            Assert.Equal(3, uf.ComponentSize(1));
            Assert.Equal(3, uf.ComponentSize(2));
            Assert.Equal(1, uf.ComponentSize(3));
            Assert.Equal(1, uf.ComponentSize(4));

            uf.Union(2, 1);
            Assert.Equal(3, uf.ComponentSize(0));
            Assert.Equal(3, uf.ComponentSize(1));
            Assert.Equal(3, uf.ComponentSize(2));
            Assert.Equal(1, uf.ComponentSize(3));
            Assert.Equal(1, uf.ComponentSize(4));

            uf.Union(3, 4);
            Assert.Equal(3, uf.ComponentSize(0));
            Assert.Equal(3, uf.ComponentSize(1));
            Assert.Equal(3, uf.ComponentSize(2));
            Assert.Equal(2, uf.ComponentSize(3));
            Assert.Equal(2, uf.ComponentSize(4));

            uf.Union(4, 3);
            Assert.Equal(3, uf.ComponentSize(0));
            Assert.Equal(3, uf.ComponentSize(1));
            Assert.Equal(3, uf.ComponentSize(2));
            Assert.Equal(2, uf.ComponentSize(3));
            Assert.Equal(2, uf.ComponentSize(4));

            uf.Union(1, 3);
            Assert.Equal(5, uf.ComponentSize(0));
            Assert.Equal(5, uf.ComponentSize(1));
            Assert.Equal(5, uf.ComponentSize(2));
            Assert.Equal(5, uf.ComponentSize(3));
            Assert.Equal(5, uf.ComponentSize(4));

            uf.Union(4, 0);
            Assert.Equal(5, uf.ComponentSize(0));
            Assert.Equal(5, uf.ComponentSize(1));
            Assert.Equal(5, uf.ComponentSize(2));
            Assert.Equal(5, uf.ComponentSize(3));
            Assert.Equal(5, uf.ComponentSize(4));
        }

        [Fact]
        public void ConnectivityChangesCorrectly()
        {

            int sz = 7;
            var uf = new UnionFind<int>(new int[] { 0, 1, 2, 3, 4, 5, 6 });

            for (int i = 0; i < sz; i++) Assert.True(uf.Connected(i, i));

            uf.Union(0, 2);

            Assert.True(uf.Connected(0, 2));
            Assert.True(uf.Connected(2, 0));

            Assert.False(uf.Connected(0, 1));
            Assert.False(uf.Connected(3, 1));
            Assert.False(uf.Connected(6, 4));
            Assert.False(uf.Connected(5, 0));

            for (int i = 0; i < sz; i++) Assert.True(uf.Connected(i, i));

            uf.Union(3, 1);

            Assert.True(uf.Connected(0, 2));
            Assert.True(uf.Connected(2, 0));
            Assert.True(uf.Connected(1, 3));
            Assert.True(uf.Connected(3, 1));

            Assert.False(uf.Connected(0, 1));
            Assert.False(uf.Connected(1, 2));
            Assert.False(uf.Connected(2, 3));
            Assert.False(uf.Connected(1, 0));
            Assert.False(uf.Connected(2, 1));
            Assert.False(uf.Connected(3, 2));

            Assert.False(uf.Connected(1, 4));
            Assert.False(uf.Connected(2, 5));
            Assert.False(uf.Connected(3, 6));

            for (int i = 0; i < sz; i++) Assert.True(uf.Connected(i, i));

            uf.Union(2, 5);
            Assert.True(uf.Connected(0, 2));
            Assert.True(uf.Connected(2, 0));
            Assert.True(uf.Connected(1, 3));
            Assert.True(uf.Connected(3, 1));
            Assert.True(uf.Connected(0, 5));
            Assert.True(uf.Connected(5, 0));
            Assert.True(uf.Connected(5, 2));
            Assert.True(uf.Connected(2, 5));

            Assert.False(uf.Connected(0, 1));
            Assert.False(uf.Connected(1, 2));
            Assert.False(uf.Connected(2, 3));
            Assert.False(uf.Connected(1, 0));
            Assert.False(uf.Connected(2, 1));
            Assert.False(uf.Connected(3, 2));

            Assert.False(uf.Connected(4, 6));
            Assert.False(uf.Connected(4, 5));
            Assert.False(uf.Connected(1, 6));

            for (int i = 0; i < sz; i++) Assert.True(uf.Connected(i, i));

            // Connect everything
            uf.Union(1, 2);
            uf.Union(3, 4);
            uf.Union(4, 6);

            for (int i = 0; i < sz; i++)
            {
                for (int j = 0; j < sz; j++)
                {
                    Assert.True(uf.Connected(i, j));
                }
            }
        }

        [Fact]
        public void SizeDoesNotChange()
        {
            var uf = new UnionFind<int>(new int[] { 0, 1, 2, 3, 4 });

            Assert.Equal(5, uf.Size);
            uf.Union(0, 1);
            uf.FindComponent(3);
            Assert.Equal(5, uf.Size);
            uf.Union(1, 2);
            Assert.Equal(5, uf.Size);
            uf.Union(0, 2);
            uf.FindComponent(1);
            Assert.Equal(5, uf.Size);
            uf.Union(2, 1);
            Assert.Equal(5, uf.Size);
            uf.Union(3, 4);
            uf.FindComponent(0);
            Assert.Equal(5, uf.Size);
            uf.Union(4, 3);
            uf.FindComponent(3);
            Assert.Equal(5, uf.Size);
            uf.Union(1, 3);
            Assert.Equal(5, uf.Size);
            uf.FindComponent(2);
            uf.Union(4, 0);
            Assert.Equal(5, uf.Size);
        }
    }
}
