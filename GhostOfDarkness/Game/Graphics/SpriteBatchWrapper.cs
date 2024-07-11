using Microsoft.Xna.Framework;
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

    public void Draw(Texture2D texture, Vector2 position, float scale, float layerDepth)
    {
        Draw(texture, position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, layerDepth);
    }

    public void Draw(Texture2D texture, Vector2 position, Vector2 scale, float layerDepth)
    {
        Draw(texture, position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, layerDepth);
    }
}