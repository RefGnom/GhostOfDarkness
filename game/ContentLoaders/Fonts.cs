using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal static class Fonts
{
    public static SpriteFont Arial { get; private set; }
    public static SpriteFont TimesNewRoman { get; private set; }
    public static SpriteFont Debug { get; private set; }

    public static void Load(ContentManager content)
    {
        Arial = content.Load<SpriteFont>("Fonts\\Arial");
        TimesNewRoman = content.Load<SpriteFont>("Fonts\\TimesNewRoman");
        Debug = content.Load<SpriteFont>("Fonts\\Debug");
    }
}