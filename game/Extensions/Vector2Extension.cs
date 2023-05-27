﻿using Microsoft.Xna.Framework;
using System;

namespace game;

internal static class Vector2Extension
{
    /// <summary>
    /// </summary>
    /// <returns>angle in radians</returns>
    public static float ToAngle(this Vector2 direction)
    {
        var (x, y) = direction;
        var angle = -MathF.Atan(x / y);
        if (y >= 0)
            angle += MathF.PI;
        if (angle < 0)
            angle += 2 * MathF.PI;
        return angle;
    }
}