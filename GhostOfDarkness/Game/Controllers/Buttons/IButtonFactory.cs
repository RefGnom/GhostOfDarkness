using Core.Saves;
using Game.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Controllers.Buttons;

public interface IButtonFactory
{
    Button CreateButtonWithText(Texture2D texture, Vector2 position, string text, Align align = 0, int indentX = 0, int indentY = 0);

    Button CreateButtonWithText(
        Texture2D texture,
        Vector2 position,
        string text,
        float buttonLayer,
        float textLayer,
        Align align = 0,
        int indentX = 0,
        int indentY = 0
    );

    RadioButton CreateSaveButton(Texture2D disabledTexture, Texture2D enabledTexture, Vector2 position, SaveInfo saveInfo);
}