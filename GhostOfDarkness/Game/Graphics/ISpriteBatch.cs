using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

public interface ISpriteBatch
{
    void Begin(
        SpriteSortMode sortMode = SpriteSortMode.Deferred,
        BlendState blendState = null,
        SamplerState samplerState = null,
        DepthStencilState depthStencilState = null,
        RasterizerState rasterizerState = null,
        Effect effect = null,
        Matrix? transformMatrix = null
    );

    void End();

    void Draw(
        Texture2D texture,
        Vector2 position,
        float scale,
        float layerDepth
    );

    void Draw(
        Texture2D texture,
        Vector2 position,
        Vector2 scale,
        float layerDepth
    );

    void Draw(
        Texture2D texture,
        Vector2 position,
        Rectangle? sourceRectangle,
        Color color,
        float rotation,
        Vector2 origin,
        Vector2 scale,
        SpriteEffects effects,
        float layerDepth
    );

    void Draw(
        Texture2D texture,
        Vector2 position,
        Rectangle? sourceRectangle,
        Color color,
        float rotation,
        Vector2 origin,
        float scale,
        SpriteEffects effects,
        float layerDepth
    );

    void Draw(
        Texture2D texture,
        Rectangle destinationRectangle,
        Rectangle? sourceRectangle,
        Color color,
        float rotation,
        Vector2 origin,
        SpriteEffects effects,
        float layerDepth
    );

    void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color);

    void Draw(
        Texture2D texture,
        Rectangle destinationRectangle,
        Rectangle? sourceRectangle,
        Color color
    );

    void Draw(Texture2D texture, Vector2 position, Color color);

    void Draw(Texture2D texture, Rectangle destinationRectangle, Color color);

    void DrawString(
        SpriteFont spriteFont,
        string text,
        Vector2 position,
        Color color
    );

    void DrawString(
        SpriteFont spriteFont,
        string text,
        Vector2 position,
        Color color,
        float rotation,
        Vector2 origin,
        float scale,
        SpriteEffects effects,
        float layerDepth
    );

    void DrawString(
        SpriteFont spriteFont,
        string text,
        Vector2 position,
        Color color,
        float rotation,
        Vector2 origin,
        Vector2 scale,
        SpriteEffects effects,
        float layerDepth
    );

    void DrawString(
        SpriteFont spriteFont,
        string text,
        Vector2 position,
        Color color,
        float rotation,
        Vector2 origin,
        Vector2 scale,
        SpriteEffects effects,
        float layerDepth,
        bool rtl
    );

    void DrawString(
        SpriteFont spriteFont,
        StringBuilder text,
        Vector2 position,
        Color color
    );

    void DrawString(
        SpriteFont spriteFont,
        StringBuilder text,
        Vector2 position,
        Color color,
        float rotation,
        Vector2 origin,
        float scale,
        SpriteEffects effects,
        float layerDepth
    );

    void DrawString(
        SpriteFont spriteFont,
        StringBuilder text,
        Vector2 position,
        Color color,
        float rotation,
        Vector2 origin,
        Vector2 scale,
        SpriteEffects effects,
        float layerDepth
    );

    void DrawString(
        SpriteFont spriteFont,
        StringBuilder text,
        Vector2 position,
        Color color,
        float rotation,
        Vector2 origin,
        Vector2 scale,
        SpriteEffects effects,
        float layerDepth,
        bool rtl
    );
}