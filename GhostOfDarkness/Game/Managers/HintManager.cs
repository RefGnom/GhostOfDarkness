﻿using Game.ContentLoaders;
using Game.Graphics;
using Game.Service;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Managers;

internal static class HintManager
{
    private static string currentMessage;
    private static readonly SpriteFont font = Fonts.Buttons;

    public static void Show(string message)
    {
        currentMessage = message;
    }

    public static void Hide()
    {
        currentMessage = null;
    }

    public static void Draw(ISpriteBatch spriteBatch, float scale)
    {
        if (currentMessage is null)
        {
            return;
        }

        var position = new Vector2(960, 1040);
        var origin = font.MeasureString(currentMessage) / 2;
        spriteBatch.DrawString(font, currentMessage, position * scale, Color.White, 0, origin, scale, SpriteEffects.None, Layers.Text);
    }
}