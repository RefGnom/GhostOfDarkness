using System.Text;
using Game.ContentLoaders;
using Game.Controllers.InputServices;
using Game.Graphics;
using Game.Service;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using IDrawable = Game.Interfaces.IDrawable;

namespace Game.Controllers;

internal class TextInput : IDrawable
{
    private readonly Texture2D background;
    private readonly Vector2 position;
    private readonly IKeyboardService keyboardService;
    private readonly StringBuilder text;

    private static SpriteFont Font => Fonts.Common24;
    public string Text => text.ToString();

    public TextInput(Texture2D background, Vector2 position, IKeyboardService keyboardService)
    {
        this.background = background;
        this.position = position;
        this.keyboardService = keyboardService;
        text = new StringBuilder();
    }

    public void Enable()
    {
        keyboardService.GetGameWindow().TextInput += InputText;
    }

    public void Disable()
    {
        keyboardService.GetGameWindow().TextInput -= InputText;
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

    private static bool IsValid(char symbol) => char.IsLetter(symbol) || char.IsDigit(symbol) || symbol == ' ';

    private bool IsHavePlace()
    {
        var textSize = Font.MeasureString(Text);
        return textSize.X <= background.Width - 2 * textSize.Y;
    }

    public void Draw(ISpriteBatch spriteBatch, float scale)
    {
        var positionWithScale = this.position * scale;
        var textSize = Font.MeasureString(Text);
        var textPosition = positionWithScale + new Vector2(textSize.Y, background.Height / 2f - textSize.Y / 2) * scale;
        spriteBatch.Draw(background, positionWithScale, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, Layers.Ui);
        spriteBatch.DrawString(Font, Text, textPosition, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, Layers.Text);
    }
}