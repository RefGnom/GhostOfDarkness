using Game.Graphics;
using Microsoft.Xna.Framework;
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
            [],
            [],
            [],
            1,
            1,
            [],
            null);
    }

    public static ISpriteBatch GetSpriteBatch() => Substitute.For<ISpriteBatch>();

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

        private TestGame()
        {
            var graphics = new GraphicsDeviceManager(this);
            graphics.ApplyChanges();
            GraphicsDeviceOriginal = graphics.GraphicsDevice;
        }
    }
}