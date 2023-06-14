using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal static class HintManager
{
    private static string currentMessage = null;
    private static readonly SpriteFont font = Fonts.TimesNewRoman;

    public static void Show(string message)
    {
        currentMessage = message;
    }

    public static void Hide() 
    {
        currentMessage = null;
    }

    public static void Draw(SpriteBatch spriteBatch, float scale)
    {
        if (currentMessage is null)
            return;
        var position = new Vector2(960, 1040);
        var origin = font.MeasureString(currentMessage) / 2;
        spriteBatch.DrawString(font, currentMessage, position * scale, Color.White, 0, origin, scale, SpriteEffects.None, Layers.Text);
    }
}