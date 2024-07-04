﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NSubstitute;

namespace GameTest;

public static class SubstituteProvider
{
    private static readonly GraphicsDevice defaultGraphicsDevice;

    static SubstituteProvider()
    {
        defaultGraphicsDevice = TestGame.GraphicsDeviceOriginal;
    }

    public static Texture2D GetTexture2D(int width, int height)
    {
        return Substitute.For<Texture2D>(new object[]
        {
            defaultGraphicsDevice,
            width,
            height
        });
    }

    public static SpriteFont GetFont()
    {
        var texture = GetTexture2D(1, 1);
        return new SpriteFont(texture,
            new List<Rectangle>(),
            new List<Rectangle>(),
            new List<char>(),
            1,
            1,
            new List<Vector3>(),
            null);
    }

    public static SpriteBatchMock GetSpriteBatch() => Substitute.For<SpriteBatchMock>(new object[]
    {
        defaultGraphicsDevice
    });

    private class TestGame : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDevice GraphicsDeviceOriginal = null!;

        static TestGame()
        {
            if (GraphicsDeviceOriginal == null!)
            {
                _ = new TestGame();
            }
        }

        public TestGame()
        {
            var graphics = new GraphicsDeviceManager(this);
            graphics.ApplyChanges();
            GraphicsDeviceOriginal = graphics.GraphicsDevice;
        }
    }
}