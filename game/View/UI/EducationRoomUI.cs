using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class EducationRoomUI : IDrawable
{
    private static EducationRoomUI instance;
    private Vector2 position;

    private EducationRoomUI(Vector2 position)
    {
        this.position = position;
        GameManager.Instance.Drawer.Register(this);
    }

    public static void Create(Vector2 position)
    {
        instance = new EducationRoomUI(position);
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        spriteBatch.Draw(Textures.EducationUI, position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, Layers.UIBackground);
    }
}