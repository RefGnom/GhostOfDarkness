using Core.Extensions;
using Game.Controllers;
using Game.Controllers.InputServices;
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
    private readonly ScrollBounds scrollBounds;

    public ScrollBar(
        Vector2 position,
        Texture2D scrollBarTexture,
        Texture2D scrollBoxTexture,
        Point? boxIndent = null,
        ScrollBounds scrollBounds = null
    )
    {
        this.position = position;
        this.scrollBarTexture = scrollBarTexture;
        var outerBounds = scrollBarTexture.Bounds.Shift(position);
        scrollBox = new ScrollBox(outerBounds, scrollBoxTexture, position, boxIndent ?? Point.Zero);
        this.scrollBounds = scrollBounds;
    }

    public void SetBoxScale(Vector2 scale)
    {
        scrollBox.CustomScale = scale;
    }

    public void ShiftBox(float shiftValue)
    {
        scrollBox.Shift(shiftValue);
    }

    public void Update(float deltaTime)
    {
        scrollBox.Update(deltaTime);
        scrollBounds?.Update(deltaTime);
    }

    public void Draw(ISpriteBatch spriteBatch, float scale)
    {
        spriteBatch.Draw(scrollBarTexture, position * scale, scale, Layers.Ui);
        scrollBox.Draw(spriteBatch, scale);
        scrollBounds?.SetScale(scale);
    }

    private class ScrollBox : IComponent
    {
        private readonly Vector2 selectedScale = new Vector2(1.05f, 1.05f);

        private readonly Texture2D texture;
        private readonly Rectangle outerBounds;
        private readonly Point indent;
        private readonly Vector2 basePosition;
        private readonly Vector2 origin;
        private StickyCursor stickyCursor;
        private Vector2 position;
        private Vector2 customScale;

        public Vector2 CustomScale
        {
            get => customScale;
            set
            {
                customScale = value;
                var innerBounds = GetBounds();
                stickyCursor = new StickyCursor(Input.MouseService, outerBounds, innerBounds, indent);
                stickyCursor.InnerBoundsChanged += bounds => position = bounds.Center.ToVector2();
                position = stickyCursor.InnerBounds.Center.ToVector2();
            }
        }

        public ScrollBox(Rectangle outerBounds, Texture2D texture, Vector2 position, Point? indent = null)
        {
            this.texture = texture;
            this.indent = indent ?? Point.Zero;
            this.position = position;
            basePosition = position;
            origin = this.texture.Bounds.Center.ToVector2();
            this.outerBounds = outerBounds;
            CustomScale = Vector2.One;
        }

        public void Shift(float shiftValue)
        {
            stickyCursor.ShiftInnerBoundsVertical(shiftValue);
        }

        public void Update(float deltaTime)
        {
            stickyCursor.Update();

            if (stickyCursor.IsStuck)
            {
                position = stickyCursor.InnerBounds.Center.ToVector2();
            }
        }

        public void Draw(ISpriteBatch spriteBatch, float scale)
        {
            stickyCursor.SetScale(scale);
            var vectorScale = stickyCursor.Selected ? selectedScale : Vector2.One;
            vectorScale = vectorScale * scale * CustomScale;
            spriteBatch.Draw(texture, position * scale, origin, vectorScale, Layers.Ui);
        }

        private Rectangle GetBounds()
        {
            var size = new Vector2(texture.Width, texture.Height) * CustomScale;
            var boundsPosition = basePosition;
            return new Rectangle(boundsPosition.ToPoint(), size.ToPoint());
        }
    }
}