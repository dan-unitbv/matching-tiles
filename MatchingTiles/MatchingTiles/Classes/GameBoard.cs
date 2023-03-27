using System;
using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;

namespace MatchingTiles
{
    public class GameBoard
    {
        private List<List<string>> Board { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        public GameBoard(int rows, int columns)
        {
            Board = new List<List<string>>();
            Rows = rows;
            Columns = columns;
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            var halfTilesList = TileData.Tiles.GetRange(0, Rows * Columns / 2);
            var allTilesList = halfTilesList.Concat(halfTilesList).ToList();

            for (int i = 0; i < Rows; i++)
            {
                List<string> auxList = new List<string>();

                for (int j = 0; j < Columns; j++)
                {
                    auxList.Add(allTilesList[i * Columns + j].Path);
                }

                Board.Add(auxList);
            }

            Random random = new Random();
            Shuffles.ShuffleBoard<string>(random, Board);
            TileData.TilesPaths = Board;
        }

        public bool AreMatching(List<KeyValuePair<Button, Button>> tiles)
        {
            return tiles[0].Value.DataContext == tiles[1].Value.DataContext;
        }

        public void HideIfMatching(List<KeyValuePair<Button, Button>> tiles)
        {
            tiles[0].Key.Visibility = System.Windows.Visibility.Hidden;
            tiles[0].Value.Visibility = System.Windows.Visibility.Hidden;
            tiles[1].Key.Visibility = System.Windows.Visibility.Hidden;
            tiles[1].Value.Visibility = System.Windows.Visibility.Hidden;

        }

        public void HideIfNotMatching(List<KeyValuePair<Button, Button>> tiles)
        {
            tiles[0].Value.Visibility = System.Windows.Visibility.Hidden;
            tiles[1].Value.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
