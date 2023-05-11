using game.Extensions;
using game.Interfaces;
using game.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game.Objects;

internal class Wall : IEntity
{
    private Vector2 position;
    public Rectangle Hitbox => HiboxManager.Wall.Shift(position);
    public bool CanCollided => true;

    public Wall(Vector2 position)
    {
        this.position = position;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(TexturesManager.Wall, position, null, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
    }
}