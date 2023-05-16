using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class Sprite : IDrawable
{
    private readonly Texture2D texture;
    private Vector2 position;

    public Sprite(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        spriteBatch.Draw(texture, position * scale, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, Layers.UIBackground);
    }
}