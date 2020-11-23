using System;

namespace DataStructures.Library
{
    public class HashTableLinearProbing<TKey, TValue> : HashTableOpenAddressingBase<TKey, TValue>
    {
        private readonly int LINEAR_FACTOR = 17;

        public HashTableLinearProbing() : base() { }
        public HashTableLinearProbing(int capacity) : base(capacity) { }
        public HashTableLinearProbing(int capacity, double loadFactor) : base(capacity, loadFactor) { }

        protected override int AdjustCapacity(int capacity)
        {
            while (GreatestCommonDivisor(LINEAR_FACTOR, capacity) != 1) capacity++;
            return capacity;
        }

        protected override int IncreaseCapacity(int capacity)
        {
            return capacity * 2;
        }

        protected override int Probe(int x)
        {
            return LINEAR_FACTOR * x;
        }

        private int GreatestCommonDivisor(int a, int b)
        {
            while (a != b)
            {
                if (a > b)
                {
                    a -= b;
                }
                else
                {
                    b -= a;
                }
            }

            return a;
        }
    }
}
