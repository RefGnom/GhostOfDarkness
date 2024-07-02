using System.Text;
using game;
using Game.ContentLoaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using IDrawable = game.IDrawable;

namespace Game.Controllers;

internal class TextInput : IDrawable
{
    private readonly Texture2D background;
    private readonly Vector2 position;
    private readonly StringBuilder text;

    private static SpriteFont Font => Fonts.Common24;
    public string Text => text.ToString();

    public TextInput(Texture2D background, Vector2 position)
    {
        this.background = background;
        this.position = position;
        text = new StringBuilder();
    }

    public void Enable()
    {
        KeyboardController.GameWindow.TextInput += InputText;
    }

    public void Disable()
    {
        KeyboardController.GameWindow.TextInput -= InputText;
    }

    public void Clear()
    {
        text.Clear();
    }

    private void InputText(object sender, TextInputEventArgs e)
    {
        if (e.Key == Keys.Back)
        {
            if (text.Length > 0)
            {
                text.Remove(text.Length - 1, 1);
            }
        }
        else if (IsValid(e.Character) && IsHavePlace())
        {
            text.Append(e.Character);
        }
    }

    private static bool IsValid(char symbol) => symbol >= 'a' && symbol <= 'z'
                                                || symbol >= 'A' && symbol <= 'Z'
                                                || symbol >= 'а' && symbol <= 'я'
                                                || symbol >= 'А' && symbol <= 'Я'
                                                || symbol == ' '
                                                || symbol >= '0' && symbol <= '9';

    private bool IsHavePlace()
    {
        var textSize = Font.MeasureString(Text);
        return textSize.X <= background.Width - 2 * textSize.Y;
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        var position = this.position * scale;
        var textSize = Font.MeasureString(Text);
        var textPosition = position + new Vector2(textSize.Y, background.Height / 2 - textSize.Y / 2) * scale;
        spriteBatch.Draw(background, position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, Layers.UI);
        spriteBatch.DrawString(Font, Text, textPosition, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, Layers.Text);
    }
}