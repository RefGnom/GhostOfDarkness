using Microsoft.Xna.Framework;

namespace Core.Extensions;

public static class ColorExtensions
{
    public static Color WithAlpha(this Color color, byte alpha)
    {
        color.A = alpha;
        return color;
    }
    public static Color WithColorDelta(this Color color, byte delta)
    {
        color.R += delta;
        color.G += delta;
        color.B += delta;
        return color;
    }
}