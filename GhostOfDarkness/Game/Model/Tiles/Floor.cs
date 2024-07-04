using Core.DependencyInjection;
using Game.Graphics;
using Game.Service;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Game.Interfaces.IDrawable;

namespace game;

[DiIgnore]
internal class Floor : IDrawable
{
    private readonly Texture2D texture;
    private readonly Vector2 position;

    public Floor(Vector2 position)
    {
        this.position = position;
        texture = Textures.Floor;
    }

    public Floor(Vector2 position, string name)
    {
        this.position = position;
        texture = Textures.GetFloorTexture(name);
    }

    public void Draw(ISpriteBatch spriteBatch, float scale)
    {
        spriteBatch.Draw(texture, position, null, Color.Gray, 0, Vector2.Zero, 0.5f, SpriteEffects.None, Layers.Floor);
    }
}