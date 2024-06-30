using Core.Extensions;
using Game.ContentLoaders;
using Game.Enums;
using Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Controllers.Buttons;

public class ButtonFactory : IButtonFactory
{
    public Button CreateButtonWithText(Texture2D texture, Vector2 position, string text, Align align = Align.Center, int indent = 0)
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
        Align align = Align.Center,
        int indent = 0
    )
    {
        var button = new Button(texture, position, buttonLayer);
        var bounds = texture.Bounds.Shift(position);
        button.AddDrawable(new Text(bounds, text, align, indent, Fonts.Buttons, textLayer));
        return button;
    }
}