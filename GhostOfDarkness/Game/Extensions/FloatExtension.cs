using System;
using System.Collections.Generic;

namespace game;

internal static class FloatExtension
{
    private static readonly List<(float, string)> directions = new()
    {
        (0 * MathF.PI / 4, "N"),
        (1 * MathF.PI / 4, "NE"),
        (2 * MathF.PI / 4, "E"),
        (3 * MathF.PI / 4, "SE"),
        (4 * MathF.PI / 4, "S"),
        (5 * MathF.PI / 4, "SW"),
        (6 * MathF.PI / 4, "W"),
        (7 * MathF.PI / 4, "NW"),
    };

    public static string ToCardinalDirection(this float angle)
    {
        foreach (var (value, direction) in directions)
        {
            if (angle.InBounds(value, MathF.PI / 8)
                || angle.InBounds(value + 2 * MathF.PI, MathF.PI / 8))
                return direction;
        }
        return null;
    }

    public static bool InBounds(this float value, float target, float delta)
    {
        return target - delta <= value && value < target + delta;
    }
}