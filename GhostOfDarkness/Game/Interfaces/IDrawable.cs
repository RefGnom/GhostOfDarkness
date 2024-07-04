using Game.Graphics;

namespace Game.Interfaces;

public interface IDrawable
{
    void Draw(ISpriteBatch spriteBatch, float scale);
}