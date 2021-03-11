using System;
using System.Collections.Generic;
using System.Linq;

namespace MinBy
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static TSource MinBy<TSource, TKey>(
         this IEnumerable<TSource> source,
         Func<TSource, TKey> keySelector,
         IComparer<TKey> comparer = null)
        {
            // argument validation and defaulting for comparer
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (comparer == null) comparer = Comparer<TKey>.Default;

            // TODO: Replace the following bad code with your code
            // return source.Aggregate((min, x) => comparer.Compare(keySelector(x), keySelector(min)) < 0 ? x : min); // bad, calls keyselector multiple times

            // return source.OrderBy(keySelector, comparer).First(); // bad, sorts entire sequence to find MinBy

            // TKey min = source.Min(keySelector);
            // return source.First(x => keySelector(x).Equals(min)); // bad, iterates twice, calls keySelector twice, doesn't use comparer

            
            return Helper(source.Skip(1), keySelector, source.First(), keySelector(source.First()), comparer);
            
        }

        public static TSource Helper<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            TSource min,
            TKey minKey,
            IComparer<TKey> comparer = null
        )
        {
            if(!source.Any())
            {
                return min;
            }

            TKey compareValue = keySelector(source.First());

            if(comparer.Compare(compareValue, minKey) < 0)
            {
                min = source.First();
                minKey = compareValue;
            }

            return Helper(source.Skip(1), keySelector, min, minKey, comparer);
        }
    }
}
