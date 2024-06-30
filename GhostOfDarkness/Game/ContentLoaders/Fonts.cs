using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal static class Fonts
{
    public static SpriteFont Buttons { get; private set; }
    public static SpriteFont Debug { get; private set; }
    public static SpriteFont Common10 { get; private set; }
    public static SpriteFont Common12 { get; private set; }
    public static SpriteFont Common14 { get; private set; }
    public static SpriteFont Common16 { get; private set; }
    public static SpriteFont Common18 { get; private set; }
    public static SpriteFont Common24 { get; private set; }

    public static void Load(ContentManager content)
    {
        Buttons = content.Load<SpriteFont>("Fonts\\Buttons");
        Debug = content.Load<SpriteFont>("Fonts\\Debug");
        Common10 = content.Load<SpriteFont>("Fonts\\Common10");
        Common12 = content.Load<SpriteFont>("Fonts\\Common12");
        Common14 = content.Load<SpriteFont>("Fonts\\Common14");
        Common16 = content.Load<SpriteFont>("Fonts\\Common16");
        Common18 = content.Load<SpriteFont>("Fonts\\Common18");
        Common24 = content.Load<SpriteFont>("Fonts\\Common24");
    }
}