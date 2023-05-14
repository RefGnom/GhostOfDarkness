using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace game;

internal class TextInput : IDrawable
{
    private readonly Texture2D background;
    private readonly Vector2 position;
    private readonly StringBuilder text;

    public string Text => text.ToString();

    public TextInput(Texture2D background, Vector2 position)
    {
        this.background = background;
        this.position = position;
        text = new();
        KeyboardController.GameWindow.TextInput += InputText;
    }

    public void Delete()
    {
        KeyboardController.GameWindow.TextInput -= InputText;
    }

    private void InputText(object sender, TextInputEventArgs e)
    {
        if (e.Key == Keys.Back)
        {
            if (text.Length > 0)
                text.Remove(text.Length - 1, 1);
        }
        else if (IsValid(e.Character))
        {
            text.Append(e.Character);
        }
    }

    private bool IsValid(char symbol)
    {
        return symbol >= 'a' && symbol <= 'z'
            || symbol >= 'A' && symbol <= 'Z'
            || symbol == ' '
            || symbol >= '0' && symbol <= '9';
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var scale = GameManager.Instance.Game.WindowWidth / 1920f;
        var position = this.position * scale;
        var textSize = FontsManager.TimesNewRoman.MeasureString(Text);
        var textPosition = position + new Vector2(textSize.Y, background.Height / 2 - textSize.Y / 2) * scale;
        spriteBatch.Draw(background, position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, Layers.UI);
        spriteBatch.DrawString(FontsManager.TimesNewRoman, Text, textPosition, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, Layers.Text);
    }
}