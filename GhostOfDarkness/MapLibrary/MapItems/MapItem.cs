using Microsoft.Xna.Framework;

namespace MapLibrary;

public abstract class MapItem
{
    public Vector2 Position { get; init; }
    public float Rotation { get; init; }
}