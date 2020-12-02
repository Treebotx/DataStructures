using DataStructures.Library;
using System.Linq;
using Xunit;

namespace DataStructures.Tests
{
    public class TrieTests
    {
        [Theory]
        [InlineData(new object[] { new string[] { "ab" } })]
        [InlineData(new object[] { new string[] { "abc", "abgl", "cdf", "abcd", "lmn" } })]
        [InlineData(new object[] { new string[] { "abgl", "a", "xxxxxxxxxxxx" } })]
        public void DoesWordExist_ReturnsTrueIfWordHasBeenAdded(string[] array)
        {
            var trie = new Trie();

            foreach (var item in array) trie.Add(item);

            foreach (var item in array) Assert.True(trie.DoesWordExist(item));
        }

        [Theory]
        [InlineData(new object[] { new string[] { "ab" }, "a" })]
        [InlineData(new object[] { new string[] { "abc", "abgl", "cdf", "abcd", "lmn" }, "xxx" })]
        [InlineData(new object[] { new string[] { "abgl", "a", "xxxxxxxxxxxx" }, "ab" })]
        public void DoesWordExist_ReturnsFalseIfWordHasNotBeenAdded(string[] array, string wordToCheck)
        {
            var trie = new Trie();

            foreach (var item in array) trie.Add(item);

            Assert.False(trie.DoesWordExist(wordToCheck));
        }

        [Theory]
        [InlineData(new object[] { new string[] { "a" }, "a" })]
        [InlineData(new object[] { new string[] { "ab" }, "a" })]
        [InlineData(new object[] { new string[] { "abc", "abgl", "cdf", "abcd", "lmn" }, "c" })]
        [InlineData(new object[] { new string[] { "abc", "abgl", "cdf", "abcd", "lmn" }, "lmn" })]
        [InlineData(new object[] { new string[] { "abgl", "a", "xxxxxxxxxxxx" }, "x" })]
        public void DoesPrefixExist_ReturnsTrueIfAWordWithPrefixPassedExists(string[] array, string prefixToCheck)
        {
            var trie = new Trie();

            foreach (var item in array) trie.Add(item);

            Assert.True(trie.DoesPrefixExist(prefixToCheck));
        }

        [Theory]
        [InlineData(new object[] { new string[] { "a" }, "x" })]
        [InlineData(new object[] { new string[] { "ab" }, "abvfd" })]
        [InlineData(new object[] { new string[] { "abc", "abgl", "cdf", "abcd", "lmn" }, "yyy" })]
        [InlineData(new object[] { new string[] { "abc", "abgl", "cdf", "abcd", "lmn" }, "cdfg" })]
        [InlineData(new object[] { new string[] { "abgl", "a", "xxxxxxxxxxxx" }, "xxxxxxxxxxxxx" })]
        public void DoesPrefixExist_ReturnsFalseIfAWordWithPrefixPassedDoesNotExists(string[] array, string prefixToCheck)
        {
            var trie = new Trie();
            foreach (var item in array) trie.Add(item);

            Assert.False(trie.DoesPrefixExist(prefixToCheck));
        }

        [Theory]
        [InlineData(new object[] { new string[] { "a" }, "a" })]
        [InlineData(new object[] { new string[] { "ab" }, "ab" })]
        [InlineData(new object[] { new string[] { "abc", "abgl", "cdf", "abcd", "lmn" }, "cdf" })]
        [InlineData(new object[] { new string[] { "abc", "abgl", "cdf", "abcd", "lmn" }, "lmn" })]
        [InlineData(new object[] { new string[] { "abc", "abgl", "cdf", "abcd", "lmn" }, "abc" })]
        [InlineData(new object[] { new string[] { "abgl", "a", "xxxxxxxxxxxx" }, "xxxxxxxxxxxx" })]
        public void RemoveWord_WordDoesNotExistAfterRemoval(string[] array, string wordToRemove)
        {
            var trie = new Trie();
            foreach (var item in array) trie.Add(item);

            trie.RemoveWord(wordToRemove);

            Assert.False(trie.DoesWordExist(wordToRemove));
        }

        [Theory]
        [InlineData(new object[] { new string[] { "abc", "abgl", "cdf", "abcd", "lmn" }, "abc" })]
        public void RemoveWord_PrefixStillExistsIfAnotherWordWithThatAsAPrefixExists(string[] array, string wordToRemove)
        {
            var trie = new Trie();
            foreach (var item in array) trie.Add(item);

            trie.RemoveWord(wordToRemove);

            Assert.True(trie.DoesPrefixExist(wordToRemove));
        }

        [Theory]
        [InlineData(new object[] { new string[] { "a" }, "a" })]
        [InlineData(new object[] { new string[] { "ab" }, "a" })]
        [InlineData(new object[] { new string[] { "abc", "abgl", "cdf", "abcd", "lmn" }, "abc" })]
        [InlineData(new object[] { new string[] { "abc", "abgl", "cdf", "abcd", "lmn" }, "lmn" })]
        [InlineData(new object[] { new string[] { "abc", "abgl", "cdf", "abcd", "lmn" }, "a" })]
        [InlineData(new object[] { new string[] { "abgl", "a", "xxxxxxxxxxxx" }, "xxxxxxxxxxx" })]
        public void RemovePrefix_PrefixesAreRemoved(string[] array, string prefixToRemove)
        {
            var trie = new Trie();
            foreach (var item in array) trie.Add(item);

            trie.RemovePrefix(prefixToRemove);

            Assert.False(trie.DoesPrefixExist(prefixToRemove));
        }

        [Theory]
        [InlineData(new object[] { new string[] { "a" }, "a", new string[] { "a" } })]
        [InlineData(new object[] { new string[] { "ab" }, "a", new string[] { "ab" } })]
        [InlineData(new object[] { new string[] { "abc", "abgl", "cdf", "abcd", "lmn" }, "abc", new string[] { "abc", "abcd" } })]
        [InlineData(new object[] { new string[] { "abc", "abgl", "cdf", "abcd", "lmn" }, "lmn", new string[] { "lmn" } })]
        [InlineData(new object[] { new string[] { "abc", "abgl", "cdf", "abcd", "lmn" }, "a", new string[] { "abc", "abgl", "abcd" } })]
        [InlineData(new object[] { new string[] { "abgl", "a", "xxxxxxxxxxxx" }, "xxxxxxxxxxx", new string[] { "xxxxxxxxxxxx" } })]
        public void RemovePrefix_AllWordsWithAGivenPrefixAreRemoved(string[] array, string prefixToRemove, string[] wordsRemoved)
        {
            var trie = new Trie();
            foreach (var item in array) trie.Add(item);

            trie.RemovePrefix(prefixToRemove);

            foreach (var item in wordsRemoved) Assert.False(trie.DoesWordExist(item));
        }

        [Theory]
        [InlineData(new object[] { new string[] { } })]
        [InlineData(new object[] { new string[] { "a" } })]
        [InlineData(new object[] { new string[] { "ab" } })]
        [InlineData(new object[] { new string[] { "abc", "abgl", "cdf", "abcd", "lmn" } })]
        [InlineData(new object[] { new string[] { "abgl", "a", "xxxxxxxxxxxx" } })]
        public void Enumerator_ReturnsAllWordsAddedToTheTrie(string[] array)
        {
            var trie = new Trie();
            foreach (var item in array) trie.Add(item);

            var count = 0;
            foreach (var item in trie)
            {
                count++;
                Assert.Contains(item, array);
            }
            Assert.Equal(array.Length, count);
        }

        [Theory]
        [InlineData(new object[] { new string[] { "a" }, "a", new string[] { "a" } })]
        [InlineData(new object[] { new string[] { "ab" }, "a", new string[] { "ab" } })]
        [InlineData(new object[] { new string[] { "abc", "abgl", "cdf", "abcd", "lmn" }, "abc", new string[] { "abc", "abcd" } })]
        [InlineData(new object[] { new string[] { "abc", "abgl", "cdf", "abcd", "lmn" }, "a", new string[] { "abc", "abgl", "abcd" } })]
        [InlineData(new object[] { new string[] { "abgl", "a", "xxxxxxxxxxxx", "x" }, "x", new string[] { "x", "xxxxxxxxxxxx" } })]
        public void GetAllWithPrefix_ReturnsAllWordsAddedWithTheGivenPrefix(string[] array, string prefix, string[] wordsWithPrefix)
        {
            var trie = new Trie();
            foreach (var item in array) trie.Add(item);

            var result = trie.GetAllWithPrefix(prefix).ToList();

            Assert.Equal(wordsWithPrefix.Length, result.Count);
            foreach (var item in wordsWithPrefix) Assert.Contains(item, result);
        }
    }
}
