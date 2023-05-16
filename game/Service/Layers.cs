namespace game;

// Depth in [0.00f; 0.60f]
// Lower values are rendered on top of higher values
internal static class Layers
{
    public static readonly float Text = 0.05f;
    public static readonly float UI = 0.10f;
    public static readonly float UIBackground = 0.15f;
    public static readonly float HUD = 0.20f;
    public static readonly float Creatures = 0.50f;
    public static readonly float Tiles = 0.60f;
}