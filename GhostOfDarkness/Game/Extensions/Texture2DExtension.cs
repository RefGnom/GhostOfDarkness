﻿using Microsoft.Xna.Framework.Graphics;

namespace game;

internal static class Texture2DExtension
{
    public static int[,] TransformToColorsArray(this Texture2D texture)
    {
        var result = new int[texture.Width, texture.Height];
        var data = new int[texture.Width * texture.Height];
        texture.GetData(data);
        for (int i = 0; i < data.Length; i++)
        {
            var x = i % texture.Width;
            var y = i / texture.Width;
            result[x, y] = data[i];
        }
        return result;
    }
}