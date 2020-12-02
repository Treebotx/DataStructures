using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Library
{
    public class Trie : IEnumerable<string>
    {
        private class TrieNode
        {
            public Dictionary<char, TrieNode> Children { get; set; } = new Dictionary<char, TrieNode>();
            public bool EndOfWord { get; set; } = false;
        }

        private TrieNode _root = new TrieNode();

        public void Add(string word)
        {
            AddWord(_root, 0, word.ToCharArray());
        }

        private void AddWord(TrieNode node, int index, char[] word)
        {
            if (index == word.Length)
            {
                node.EndOfWord = true;
                return;
            }

            TrieNode child;
            if (node.Children.ContainsKey(word[index]))
            {
                child = node.Children[word[index]];
            }
            else
            {
                child = new TrieNode();
                node.Children.Add(word[index], child);
            }

            AddWord(child, index + 1, word);
        }

        public bool DoesPrefixExist(string prefix)
        {
            return PrefixExists(_root, 0, prefix.ToCharArray());
        }

        private bool PrefixExists(TrieNode node, int index, char[] prefixArray)
        {
            if (!node.Children.ContainsKey(prefixArray[index])) return false;
            if (index == prefixArray.Length - 1) return true;
            return PrefixExists(node.Children[prefixArray[index]], index + 1, prefixArray);
        }

        public bool DoesWordExist(string word)
        {
            return WordExists(_root, 0, word.ToCharArray());
        }

        private bool WordExists(TrieNode node, int index, char[] wordArray)
        {
            if (index == wordArray.Length) return node.EndOfWord;
            if (!node.Children.ContainsKey(wordArray[index])) return false;
            return WordExists(node.Children[wordArray[index]], index + 1, wordArray);
        }

        public void RemoveWord(string word)
        {
            DeleteWord(_root, 0, word.ToCharArray());
        }

        private void DeleteWord(TrieNode node, int index, char[] wordArray)
        {
            if (index == wordArray.Length)
            {
                node.EndOfWord = false;
            }
            else
            {
                DeleteWord(node.Children[wordArray[index]], index + 1, wordArray);
                var child = node.Children[wordArray[index]];
                if (child.Children.Count == 0) node.Children.Remove(wordArray[index]);
            }
        }

        public void RemovePrefix(string prefix)
        {
            DeletePrefix(_root, 0, prefix.ToCharArray());
        }

        private void DeletePrefix(TrieNode node, int index, char[] prefixArray)
        {
            if (index < prefixArray.Length)
            {
                if (node.Children.ContainsKey(prefixArray[index]))
                {
                    var child = node.Children[prefixArray[index]];
                    DeletePrefix(child, index + 1, prefixArray);
                    if (child.Children.Count == 0) node.Children.Remove(prefixArray[index]);

                }
                return;
            }

            foreach (var n in node.Children.Values)
            {
                DeletePrefix(n, index + 1, prefixArray);
            }
            node.Children.Clear();
        }

        public IEnumerable<string> GetAllWithPrefix(string prefix)
        {
            return GetWithPrefix(_root, 0, prefix.ToCharArray(), "");
        }

        private IEnumerable<string> GetWithPrefix(TrieNode node, int index, char[] prefixArray, string output)
        {
            if (index < prefixArray.Length)
            {
                foreach (var item in GetWithPrefix(node.Children[prefixArray[index]], index + 1, prefixArray, output + prefixArray[index])) yield return item;
            }
            else
            {
                if (node.EndOfWord) yield return output;

                foreach (var key in node.Children.Keys)
                {
                    foreach (var item in GetWithPrefix(node.Children[key], index + 1, prefixArray, output + key)) yield return item;
                }
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var item in GetAllWithPrefix("")) yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
