using System;

namespace MatchingTiles
{
    public class Tile
    {
        public int Id { get; set; }
        public string Path { get; set; }

        public Tile(int id, string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

            Id = id;
            Path = path;
        }
    }
}