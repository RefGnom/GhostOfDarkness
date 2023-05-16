using Microsoft.Xna.Framework.Graphics;

namespace game;

internal interface IDrawable
{
    public abstract void Draw(SpriteBatch spriteBatch, float scale);
}