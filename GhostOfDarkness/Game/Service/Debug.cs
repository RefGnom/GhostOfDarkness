﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game;

internal static class Debug
{
    private static List<(string Message, int DrawsCount)> messages = new();
    private static Vector2 topPosition;
    private static int lastWindowHeight;
    private static int drawsCount = 300;
    private static int maxLogCount = 20;

    public static void Initialize(int windowHeight)
    {
        lastWindowHeight = windowHeight;
        topPosition = new Vector2(10, windowHeight - 10);
    }

    public static void Log(object obj)
    {
        var message = obj.ToString();
        messages.Add((message, drawsCount));

        if (messages.Count > maxLogCount)
        {
            messages.RemoveAt(0);
        }
        else
        {
            var offset = Fonts.Debug.MeasureString(message);
            topPosition.Y -= offset.Y;
        }
    }

    public static void Update(int windowHeight)
    {
        if (lastWindowHeight != windowHeight)
            topPosition.Y += windowHeight - lastWindowHeight;
        lastWindowHeight = windowHeight;
    }

    public static void DrawMessages(SpriteBatch spriteBatch)
    {
        var currentPosition = topPosition;
        for (var i = 0; i < messages.Count; i++)
        {
            var (message, drawsCount) = messages[i];
            var offset = Fonts.Debug.MeasureString(message);
            messages[i] = (message, drawsCount - 1);
            if (drawsCount < 0)
            {
                messages.RemoveAt(i);
                i--;
                topPosition.Y += offset.Y;
                continue;
            }
            spriteBatch.DrawString(Fonts.Debug, message, currentPosition, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, Layers.Text);
            currentPosition.Y += offset.Y;
        }
    }
}