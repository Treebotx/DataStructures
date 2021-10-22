using System;
using System.Collections.Generic;

namespace DataStructures.Library
{
    public static class Memoizer
    {
        public static Func<TArg, TResult> Memoize<TArg, TResult>(Func<TArg, TResult> func)
        {
            var memo = new Dictionary<TArg, TResult>();

            return arg =>
            {
                if (memo.TryGetValue(arg, out TResult value)) return value;
                value = func(arg);
                memo.Add(arg, value);
                return value;
            };
        }
    }

    public static class MemoizerExtensions
    {
        public static Func<TArg, TResult> Memoize<TArg, TResult>(this Func<TArg, TResult> func)
        {
            return Memoizer.Memoize(func);
        }
    }
}
