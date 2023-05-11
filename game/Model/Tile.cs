using game.Interfaces;
using Microsoft.Xna.Framework;

namespace game.Model;

internal class Tile
{
    public readonly Vector2 Position;
    public readonly int Size;
    public IEntity Entity { get; set; }

    public Tile(int x, int y, int size)
    {
        Position = new Vector2(x, y);
        Size = size;
    }
}