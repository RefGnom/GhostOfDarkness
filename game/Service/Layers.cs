namespace game;

// Depth in [0.00f; 0.60f]
// Lower values are rendered on top of higher values
internal static class Layers
{
    public static readonly float ConfirmationWindowText = 0.01f;
    public static readonly float ConfirmationWindowUI = 0.02f;
    public static readonly float ConfirmationWindowBackground = 0.03f;
    public static readonly float Text = 0.05f;
    public static readonly float UI = 0.10f;
    public static readonly float UIBackground = 0.15f;
    public static readonly float Background = 0.20f;
    public static readonly float HUDForeground = 0.24f;
    public static readonly float HUDBackground = 0.25f;
    public static readonly float Creatures = 0.50f;
    public static readonly float Tiles = 0.60f;
    public static readonly float Floor = 0.61f;
}