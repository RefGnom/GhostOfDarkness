using Microsoft.Xna.Framework.Graphics;

namespace Core.MapItems.Interfaces;

public interface IComponent
{
    public void Update(float deltaTime);

    public void Draw(SpriteBatch spriteBatch, float scale);
}