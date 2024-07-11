using Core.Extensions;
using Game.Controllers;
using Game.Graphics;
using Game.Interfaces;
using Game.Service;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.View.UI;

public class ScrollBar : IComponent
{
    private readonly Vector2 position;
    private readonly Texture2D scrollBarTexture;
    private readonly ScrollBox scrollBox;

    public ScrollBar(Vector2 position, Texture2D scrollBarTexture, Texture2D scrollBoxTexture, Point? boxIndent = null)
    {
        this.position = position;
        this.scrollBarTexture = scrollBarTexture;
        var outerBounds = scrollBarTexture.Bounds.Shift(position);
        scrollBox = new ScrollBox(outerBounds, scrollBoxTexture, position, boxIndent ?? Point.Zero);
        //scrollBox.CustomScale = new Vector2(5f, 0.45f);
    }

    public void Update(float deltaTime)
    {
        scrollBox.Update(deltaTime);
    }

    public void Draw(ISpriteBatch spriteBatch, float scale)
    {
        spriteBatch.Draw(scrollBarTexture, position * scale, scale, Layers.Ui);
        scrollBox.Draw(spriteBatch, scale);
    }

    private class ScrollBox : IComponent
    {
        private readonly Vector2 selectedScale = new Vector2(1.02f, 1.02f);

        private readonly Texture2D texture;
        private readonly Rectangle outerBounds;
        private readonly Point indent;
        private StickyCursor stickyCursor;
        private Vector2 position;
        private Vector2 customScale;
        private Vector2 origin;

        public Vector2 CustomScale
        {
            get => customScale;
            set
            {
                customScale = value;
                origin = texture.Bounds.Center.ToVector2() * customScale;
                stickyCursor = new StickyCursor(outerBounds, GetBounds(), indent);
            }
        }

        public ScrollBox(Rectangle outerBounds, Texture2D texture, Vector2 position, Point? indent = null)
        {
            this.texture = texture;
            this.indent = indent ?? Point.Zero;
            this.position = position + this.indent.ToVector2();
            this.outerBounds = outerBounds;
            CustomScale = Vector2.One;
        }

        public void Update(float deltaTime)
        {
            stickyCursor.Update();

            if (stickyCursor.IsStuck)
            {
                position = stickyCursor.InnerBounds.Location.ToVector2();
            }
        }

        public void Draw(ISpriteBatch spriteBatch, float scale)
        {
            stickyCursor.SetScale(scale);
            var vectorScale = stickyCursor.Selected ? selectedScale : Vector2.One;
            vectorScale = vectorScale * scale * CustomScale;
            spriteBatch.Draw(texture, position * scale, null, Color.White, 0, origin, vectorScale, SpriteEffects.None, Layers.Ui);
        }

        private Rectangle GetBounds()
        {
            var size = new Vector2(texture.Width, texture.Height) * CustomScale;
            return new Rectangle(position.ToPoint(), size.ToPoint());
        }
    }
}