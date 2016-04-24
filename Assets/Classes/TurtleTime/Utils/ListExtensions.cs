using System;
using System.Collections.Generic;

namespace TurtleTime
{
    /// <summary>
    /// Extension methods for List objects
    /// </summary>
    static class ListExtensions
    {
        public static IEnumerable<T> GetElementsInRandomOrder<T>(this List<T> list)
        {
            Random random = new Random();
            // TODO: Investigate better randomness
            List<int> indexes = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                indexes.Add(i);
            }
            for (int i = 0; i < list.Count; i++)
            {
                int a = random.Next(0, list.Count);
                int b = random.Next(0, list.Count);
                if (a == b)
                {
                    continue;
                }
                // In case it's not clear, this is XOR in-place swap :)
                indexes[a] = indexes[a] ^ indexes[b];
                indexes[b] = indexes[a] ^ indexes[b];
                indexes[a] = indexes[a] ^ indexes[b];
            }
            for (int i = 0; i < list.Count; i++)
            {
                yield return list[indexes[i]];
            }
        }

        public static void Swap<T>(this List<T> list, int index1, int index2)
        {
            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
    }
}
