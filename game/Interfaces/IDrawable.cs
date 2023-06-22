using Microsoft.Xna.Framework.Graphics;

namespace game;

internal interface IDrawable
{
    void Draw(SpriteBatch spriteBatch, float scale);
}