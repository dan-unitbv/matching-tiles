using System;
using System.Collections.Generic;

namespace MatchingTiles
{
    public static class Shuffles
    {
        private static Random randomizer = new Random();

        public static void ShuffleTiles<T>(this IList<T> list)
        {
            int listSize = list.Count;

            while (listSize > 1)
            {
                listSize--;

                int randomIndex = randomizer.Next(listSize + 1);

                T aux = list[randomIndex];
                list[randomIndex] = list[listSize];
                list[listSize] = aux;
            }
        }

        public static void ShuffleBoard<T>(Random random, List<List<T>> list2D)
        {
            int listInListSize = list2D[0].Count;

            for (int i = list2D.Count * listInListSize - 1; i > 0; i--)
            {
                int rowI = i / listInListSize;
                int columnI = i % listInListSize;

                int j = random.Next(i + 1);
                int rowJ = j / listInListSize;
                int columnJ = j % listInListSize;

                T aux = list2D[rowI][columnI];
                list2D[rowI][columnI] = list2D[rowJ][columnJ];
                list2D[rowJ][columnJ] = aux;
            }
        }
    }
}