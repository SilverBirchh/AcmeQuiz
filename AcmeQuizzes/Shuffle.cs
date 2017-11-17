using System;
using System.Collections.Generic;
using System.Threading;

// Methods to shuffle a list see https://stackoverflow.com/questions/273313/randomize-a-listt?noredirect=1&lq=1
namespace AcmeQuizzes
{
    public static class Shuffle
    {
        [ThreadStatic] private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }

        public static void Randomise<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
