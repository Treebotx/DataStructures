using System;

namespace DataStructures.Library
{
    public class HashTableQuadraticProbing<TKey, TValue> : HashTableOpenAddressingBase<TKey, TValue>
    {
        public HashTableQuadraticProbing() { }
        public HashTableQuadraticProbing(int capacity) : base(capacity) { }
        public HashTableQuadraticProbing(int capacity, double loadFactor) : base(capacity, loadFactor) { }

        private int nextPowerOfTwo(int value)
        {
            var x = Math.Ceiling(Math.Log(value, 2));
            return (int)Math.Pow(2, x);
        }

        protected override int AdjustCapacity(int capacity)
        {
            return nextPowerOfTwo(capacity);
        }

        protected override int IncreaseCapacity(int capacity)
        {
            return capacity *= 2;
        }

        protected override int Probe(int x)
        {
            return (x * x + x) / 2;
        }
    }
}
