using DataStructures.Library;
using Xunit;

namespace DataStructures.Tests
{
    public class SuffixArrayTests
    {

        [Theory]
        [InlineData("camel", new int[] { 1, 0, 3, 4, 2 })]
        [InlineData("horse", new int[] { 4, 0, 1, 2, 3 })]
        [InlineData("ABABBAB", new int[] { 5, 0, 2, 6, 4, 1, 3 })]
        [InlineData("ABBABAABAA", new int[] { 9, 8, 5, 6, 3, 0, 7, 4, 2, 1 })]
        [InlineData("ABABABAABB", new int[] { 6, 4, 2, 0, 7, 9, 5, 3, 1, 8 })]
        [InlineData("ABCDE", new int[] { 0, 1, 2, 3, 4 })]
        public void SuffixArrayReturnedShouldBeAsExpected(string text, int[] saValues)
        {
            var sa = new SuffixArray(text);

            Assert.Equal(saValues, sa.GetSuffixArray());
        }

        [Theory]
        [InlineData("ABABBAB", new int[] { 0, 2, 2, 0, 1, 3, 1 })]
        [InlineData("AZAZA", new int[] { 0, 1, 3, 0, 2 })]
        [InlineData("ABBABAABAA", new int[] { 0, 1, 2, 1, 4, 2, 0, 3, 2, 1 })]
        [InlineData("ABABABAABB", new int[] { 0, 1, 3, 5, 2, 0, 1, 2, 4, 1 })]
        public void LCPArrayReturnedShouldBeAsExpected(string text, int[] lcpValues)
        {
            var sa = new SuffixArray(text);

            Assert.Equal(lcpValues, sa.GetLongestCommonPrefixArray());
        }
    }
}
