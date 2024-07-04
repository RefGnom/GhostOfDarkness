namespace Game.Service;

// Depth in [0.00f; 0.60f]
// Lower values are rendered on top of higher values
public static class Layers
{
    public const float ConfirmationWindowText = 0.01f;
    public const float ConfirmationWindowUi = 0.02f;
    public const float ConfirmationWindowBackground = 0.03f;
    public const float Text = 0.05f;
    public const float Ui = 0.10f;
    public const float UiBackground = 0.15f;
    public const float Background = 0.20f;
    public const float HudForeground = 0.24f;
    public const float HudBackground = 0.25f;
    public const float Creatures = 0.50f;
    public const float StaticUi = 0.54f;
    public const float Tiles = 0.55f;
    public const float Floor = 0.60f;
}