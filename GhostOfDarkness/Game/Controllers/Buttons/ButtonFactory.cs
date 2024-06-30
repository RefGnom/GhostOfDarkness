using Core.Extensions;
using Core.Saves;
using Game.ContentLoaders;
using Game.Enums;
using Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Controllers.Buttons;

public class ButtonFactory : IButtonFactory
{
    public Button CreateButtonWithText(
        Texture2D texture,
        Vector2 position,
        string text,
        Align align = 0,
        int indentX = 0,
        int indentY = 0
    )
    {
        var button = new Button(texture, position);
        var bounds = texture.Bounds.Shift(position);
        button.AddDrawable(new Text(bounds, text, Fonts.Buttons, align, indentX, indentY));
        return button;
    }

    public Button CreateButtonWithText(
        Texture2D texture,
        Vector2 position,
        string text,
        float buttonLayer,
        float textLayer,
        Align align = 0,
        int indentX = 0,
        int indentY = 0
    )
    {
        var button = new Button(texture, position, buttonLayer);
        var bounds = texture.Bounds.Shift(position);
        button.AddDrawable(new Text(bounds, text, Fonts.Buttons, align, indentX, indentY, textLayer));
        return button;
    }

    public RadioButton CreateSaveButton(
        Texture2D disabledTexture,
        Texture2D enabledTexture,
        Vector2 position,
        SaveInfo saveInfo
    )
    {
        const int indentX = 25;
        const int indentY = 20;
        var button = new RadioButton(disabledTexture, enabledTexture, position);
        var bounds = disabledTexture.Bounds.Shift(position);
        button.AddDrawable(new Text(bounds, saveInfo.Name, Fonts.Common16, Align.Left | Align.Up, indentX, indentY));
        button.AddDrawable(new Text(bounds, $"Difficulty {saveInfo.Difficulty}", Fonts.Common12, Align.Down | Align.Left, indentX, indentY));
        button.AddDrawable(new Text(bounds, $"Time {saveInfo.PlayTime}", Fonts.Common12, Align.Down | Align.Right, indentX, indentY));
        return button;
    }
}