using Microsoft.Xna.Framework.Graphics;

namespace game;

public interface IDrawable
{
    void Draw(SpriteBatch spriteBatch, float scale);
}