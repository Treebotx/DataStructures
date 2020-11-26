using System;

namespace DataStructures.Library
{
    // A Fenwick Tree implementation.
    // Implementation is ONE based. Index [0] is not used.
    // Sum(x, y) returns the sum of elements from x to y inclusive.
    public class FenwickTree
    {
        private int _length;
        private long[] _tree;

        public FenwickTree(int size)
        {
            _length = size + 1;
            _tree = new long[_length];
        }

        // Construct a Fenwick Tree with initial values.
        // values[] should be ONE BASED. values[0] is not used.
        public FenwickTree(long[] values)
        {
            if (values == null) throw new ArgumentNullException();

            _length = values.Length;
            _tree = new long[_length];

            for (var i = 1; i < _length; i++)
            {
                _tree[i] += values[i];
                var parent = i + lsb(i);
                if (parent < _length) _tree[parent] += _tree[i];
            }
        }

        // Calculates sum of the interval from left to right
        public long Sum(int left, int right)
        {
            if (right < left) throw new ArgumentException("right should be >= left");

            return PrefixSum(right) - PrefixSum(left - 1);
        }

        // Get the value at position index
        public long Get(int index)
        {
            return Sum(index, index);
        }

        // Add 'value' to index 'index'
        public void Add(int index, long value)
        {
            while (index < _length)
            {
                _tree[index] += value;
                index += lsb(index);
            }
        }

        // Set index 'index' to be equal to 'value'
        public void Set(int index, long value)
        {
            Add(index, value - Sum(index, index));
        }


        // Calculates sum from position 1 to i
        private long PrefixSum(int i)
        {
            long sum = 0;

            while (i != 0)
            {
                sum += _tree[i];
                i &= ~lsb(i); // Equivalent to i -= lsb(i)
            }

            return sum;
        }

        private static int lsb(int i)
        {
            return i & -i;
        }
    }
}
