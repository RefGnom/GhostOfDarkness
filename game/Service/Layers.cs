namespace game.Service;

// Depth in [0.0f; 0.6f]
// Lower values are rendered on top of higher values
internal static class Layers
{
    public static readonly float UI = 0.1f; 
    public static readonly float Creatures = 0.5f;
    public static readonly float Tiles = 0.6f;
}