using Microsoft.Xna.Framework.Graphics;

namespace MapLibrary;

public interface IComponent
{
    public void Update(float deltaTime);

    public void Draw(SpriteBatch spriteBatch, float scale);
}