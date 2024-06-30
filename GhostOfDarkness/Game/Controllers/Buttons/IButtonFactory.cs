using System;
using Game.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Controllers.Buttons;

public interface IButtonFactory
{
    Button CreateButtonWithText(Texture2D texture, Vector2 position, string text, Align align = Align.Center, int indent = 0);

    Button CreateButtonWithText(
        Texture2D texture,
        Vector2 position,
        string text,
        float buttonLayer,
        float textLayer,
        Align align = Align.Center,
        int indent = 0
    );

    RadioButton CreateSaveButton(
        Texture2D disabledTexture,
        Texture2D enabledTexture,
        Vector2 position,
        string saveName,
        int difficulty,
        TimeSpan time
    );
}