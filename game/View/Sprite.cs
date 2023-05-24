using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game;

internal class Sprite : IDrawable
{
    private readonly Texture2D texture;
    private Vector2 position;
    private readonly float layer;
    private readonly List<Text> texts;
    private readonly float scaleFactor;

    public Sprite(Texture2D texture, Vector2 position, float layer, float scaleFactor = 1)
    {
        this.texture = texture;
        this.position = position;
        this.layer = layer;
        texts = new();
        this.scaleFactor = scaleFactor;
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        spriteBatch.Draw(texture, position * scale, null, Color.White, 0, Vector2.Zero, scale * scaleFactor, SpriteEffects.None, layer);
        for (int i = 0; i < texts.Count; i++)
        {
            texts[i].Draw(spriteBatch, scale);
        }
    }

    public void AddText(Text text)
    {
        texts.Add(text);
    }
}