using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace game.Managers;

internal static class FontsManager
{
    public static SpriteFont Arial { get; private set; }

    public static void Load(ContentManager content)
    {
        Arial = content.Load<SpriteFont>("Fonts\\Arial");
    }
}