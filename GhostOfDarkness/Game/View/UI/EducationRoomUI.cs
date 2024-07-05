using Game.ContentLoaders;
using Game.Graphics;
using Game.Managers;
using Game.Service;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Game.Interfaces.IDrawable;

namespace Game.View.UI;

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

    public void Draw(ISpriteBatch spriteBatch, float scale)
    {
        spriteBatch.Draw(Textures.EducationUi, position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, Layers.StaticUi);
    }
}