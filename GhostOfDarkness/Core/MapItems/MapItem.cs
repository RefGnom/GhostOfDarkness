using Microsoft.Xna.Framework;

namespace Core;

public abstract class MapItem
{
    public Vector2 Position { get; init; }
    public float Rotation { get; init; }
}