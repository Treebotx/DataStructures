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

        [Theory]
        [InlineData("A", 1)]
        [InlineData("AA", 3)]
        [InlineData("AZ", 3)]
        [InlineData("ABABBAB", 28)]
        [InlineData("AZAZA", 15)]
        [InlineData("KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK", 1275)]
        public void CountOfAllSubstrings(string text, int expected)
        {
            var sa = new SuffixArray(text);

            Assert.Equal(expected, sa.CountOfAllSubstrings());
        }

        [Theory]
        [InlineData("A", new string[] { "A" })]
        [InlineData("AA", new string[] { "A", "AA", "A" })]
        [InlineData("AZ", new string[] { "A", "AZ", "Z" })]
        [InlineData("AZAZA", new string[] { "A", "AZ", "AZA", "AZAZ", "AZAZA", "Z", "ZA", "ZAZ", "ZAZA", "A", "AZ", "AZA", "Z", "ZA", "A" })]
        public void GetAllSubstrings_ReturnsAllSubstrings(string text, string[] expected)
        {
            var sa = new SuffixArray(text);

            var subStrings = sa.GetAllSubstrings();

            Assert.Equal(expected.Length, subStrings.Count);

            foreach (var item in expected) Assert.Contains(item, subStrings);
            foreach (var item in subStrings) Assert.Contains(item, expected);
        }

        [Theory]
        [InlineData("A", 1)]
        [InlineData("AA", 2)]
        [InlineData("AZ", 3)]
        [InlineData("AAA", 3)]
        [InlineData("AAAA", 4)]
        [InlineData("AAZA", 8)]
        [InlineData("ABABBAB", 19)]
        [InlineData("ABCDEFG", 28)]
        [InlineData("AZAZA", 9)]
        [InlineData("ABCDE", 15)]
        public void CountOfUniqueSubstrings(string text, int expected)
        {
            var sa = new SuffixArray(text);

            Assert.Equal(expected, sa.CountOfUniqueSubstrings());
        }

        [Theory]
        [InlineData("A", new string[] { "A" })]
        [InlineData("AA", new string[] { "A", "AA" })]
        [InlineData("AZ", new string[] { "A", "AZ", "Z" })]
        [InlineData("ABCD", new string[] { "ABCD", "ABC", "AB", "A", "BCD", "BC", "B", "CD", "C", "D" })]
        [InlineData("AZAZA", new string[] { "A", "AZ", "AZA", "AZAZ", "AZAZA", "Z", "ZA", "ZAZ", "ZAZA" })]
        public void GetUniqueSubstrings_ReturnsAllSubstrings(string text, string[] expected)
        {
            var sa = new SuffixArray(text);

            var subStrings = sa.GetUniqueSubstrings();

            Assert.Equal(expected.Length, subStrings.Count);

            foreach (var item in expected) Assert.Contains(item, subStrings);
            foreach (var item in subStrings) Assert.Contains(item, expected);
        }
    }
}
