using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace MatchingTiles
{
    public class TileData
    {
        public static List<Tile> Tiles { get; private set; }
        public static List<List<string>> TilesPaths { get; set; }

        static TileData()
        {
            Tiles = new List<Tile>();
            LoadCards();
        }

        public static void LoadCards()
        {
            int cardNumber = 0;
            Tiles = Directory.GetFiles(@"C:\Users\WIN10\source\repos\MatchingTiles\MatchingTiles\Resources\Cards\")
                .Select(x => new Tile(cardNumber++, x)).ToList();
            Tiles.ShuffleTiles<Tile>();
        }
    }
}