﻿using Core.Extensions;
using Core.Saves;
using Game.ContentLoaders;
using Game.Enums;
using Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Controllers.Buttons;

public class ButtonFactory : IButtonFactory
{
    public Button CreateButtonWithText(Texture2D texture, Vector2 position, string text, Align align = Align.Left | Align.Right, int indent = 0)
    {
        var button = new Button(texture, position);
        var bounds = texture.Bounds.Shift(position);
        button.AddDrawable(new Text(bounds, text, align, indent, Fonts.Buttons));
        return button;
    }

    public Button CreateButtonWithText(
        Texture2D texture,
        Vector2 position,
        string text,
        float buttonLayer,
        float textLayer,
        Align align = Align.Left | Align.Right,
        int indent = 0
    )
    {
        var button = new Button(texture, position, buttonLayer);
        var bounds = texture.Bounds.Shift(position);
        button.AddDrawable(new Text(bounds, text, align, indent, Fonts.Buttons, textLayer));
        return button;
    }

    public RadioButton CreateSaveButton(
        Texture2D disabledTexture,
        Texture2D enabledTexture,
        Vector2 position,
        SaveInfo saveInfo
    )
    {
        var button = new RadioButton(disabledTexture, enabledTexture, position);
        var bounds = disabledTexture.Bounds.Shift(position);
        button.AddDrawable(new Text(bounds, saveInfo.Name, Align.Left | Align.Up, 0, Fonts.Common16));
        button.AddDrawable(new Text(bounds, $"Difficulty {saveInfo.Difficulty}", Align.Down | Align.Left, 0, Fonts.Common12));
        button.AddDrawable(new Text(bounds, $"Time {saveInfo.PlayTime}", Align.Down | Align.Right, 0, Fonts.Common12));
        return button;
    }
}