using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class Floor : IDrawable
{
    private readonly Texture2D texture;
    private readonly Vector2 position;

    public Floor(Vector2 position)
    {
        this.position = position;
        texture = TexturesManager.Floor;
    }

    public Floor(Vector2 position, string name)
    {
        this.position = position;
        texture = TexturesManager.GetFloorTexture(name);
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        spriteBatch.Draw(texture, position, null, Color.Gray, 0, Vector2.Zero, 0.5f, SpriteEffects.None, Layers.Tiles);
    }
}