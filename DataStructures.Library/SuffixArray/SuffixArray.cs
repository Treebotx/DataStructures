using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Library
{
    public class SuffixArray
    {
        private char[] _text;
        private int _length => _text.Length;
        private int[] _suffixArray;
        private int[] _longestCommonPrefixArray;

        private bool _constructedSA = false;
        private bool _constructedLCP = false;

        public SuffixArray(string text)
        {
            _text = text.ToCharArray();
        }

        public int[] GetSuffixArray()
        {
            BuildSuffixArray();
            return _suffixArray;
        }

        public int[] GetLongestCommonPrefixArray()
        {
            BuildLCPArray();
            return _longestCommonPrefixArray;
        }

        private void BuildSuffixArray()
        {
            if (_constructedSA) return;
            ConstructSuffixArray();
            _constructedSA = true;
        }

        private void BuildLCPArray()
        {
            if (_constructedLCP) return;
            BuildSuffixArray();

            Kasai();
            //ConstructLCP();

            _constructedLCP = true;
        }

        public List<string> GetAllSubstrings()
        {
            var result = new List<string>();

            for (var i = 0; i < _length; i++)
            {
                for (var j = 1; j <= _length - i; j++)
                {
                    result.Add(new string(_text, i, j));
                }
            }

            return result;
        }

        public int CountOfAllSubstrings()
        {
            return _length * (_length + 1) / 2;
        }

        // Use Kasai algorithm to build the LCP array
        // http://www.mi.fu-berlin.de/wiki/pub/ABI/RnaSeqP4/suffix-array.pdf
        private void Kasai()
        {
            _longestCommonPrefixArray = new int[_length];

            var inv = new int[_length];

            for (int i = 0; i < _length; i++) inv[_suffixArray[i]] = i;

            for (int i = 0, length = 0; i < _length; i++)
            {
                if (inv[i] > 0)
                {
                    int k = _suffixArray[inv[i] - 1];
                    while ((i + length < _length) && (k + length < _length) && _text[i + length] == _text[k + length]) length++;
                    _longestCommonPrefixArray[inv[i]] = length;
                    if (length > 0) length--;
                }
            }
        }

        private void ConstructLCP()
        {
            _longestCommonPrefixArray = new int[_length];

            for (int i = 1, j = 0; i < _length; i++, j++)
            {
                var length = 0;
                var x = _suffixArray[i];
                var y = _suffixArray[j];
                while ((x + length < _length) && (y + length < _length) && _text[x + length] == _text[y + length]) length++;
                _longestCommonPrefixArray[i] = length;
            }
        }

        private class Suffix : IComparable<Suffix>
        {
            public int Index => _index;
            private int _index, _length;
            private char[] _text;

            public Suffix(char[] text, int index)
            {
                _length = text.Length - index;
                _index = index;
                _text = text;
            }

            public int CompareTo(Suffix other)
            {
                if (this == other) return 0;
                var minLength = Math.Min(_length, other._length);
                for (var i = 0; i < minLength; i++)
                {
                    if (_text[_index + i] < other._text[other._index + i]) return -1;
                    if (_text[_index + i] > other._text[other._index + i]) return +1;
                }
                return _length - other._length;
            }

            public override string ToString()
            {
                return new string(_text, _index, _length);
            }
        }

        private void ConstructSuffixArray()
        {
            _suffixArray = new int[_length];

            var suffixes = new Suffix[_length];

            for (var i = 0; i < _length; i++) suffixes[i] = new Suffix(_text, i);

            Array.Sort(suffixes);

            for (int i = 0; i < _length; i++) _suffixArray[i] = suffixes[i].Index;
        }

        public override string ToString()
        {
            BuildLCPArray();

            var sb = new StringBuilder();
            sb.Append($"{"i", 8} {"SA", 8} {"LCP", 8} {"Suffix"}\n");

            for (var i = 0; i < _length; i++)
            {
                var suffixLength = _length - _suffixArray[i];
                var suffixArray = new char[suffixLength];

                for (int j = _suffixArray[i], k = 0; j < _length; j++, k++) suffixArray[k] = _text[j];

                var suffix = new string(suffixArray);
                sb.Append($"{i, 8} {_suffixArray[i], 8} {_longestCommonPrefixArray[i], 8} {suffix}\n");
            }

            return sb.ToString();
        }
    }
}
