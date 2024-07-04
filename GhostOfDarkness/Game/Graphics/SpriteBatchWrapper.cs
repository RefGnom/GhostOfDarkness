using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

public class SpriteBatchWrapper : SpriteBatch, ISpriteBatch
{
    public SpriteBatchWrapper(GraphicsDevice graphicsDevice) : base(graphicsDevice)
    {
    }

    public SpriteBatchWrapper(GraphicsDevice graphicsDevice, int capacity) : base(graphicsDevice, capacity)
    {
    }
}