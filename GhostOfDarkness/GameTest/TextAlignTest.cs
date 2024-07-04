using game;
using Game.Enums;
using Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NSubstitute;
using Unmockable;

namespace GameTest;

public class TextAlignTest : TestBase
{
    private Rectangle defaultBounds;
    private const string defaultMessage = ""; // Всегда должен быть пустой, т.к. SpriteFont - не мок, а настоящий класс, но кастрированный

    [SetUp]
    public void Setup()
    {
        defaultBounds = new Rectangle(100, 100, 400, 200);
    }

    [Test]
    public void TestWithoutAlignIndentXIsZeroIndentYIsZero()
    {
        var font = SubstituteProvider.GetFont();
        var text = new Text(defaultBounds, defaultMessage, font, Align.Up);
        var expectedPosition = defaultBounds.Center.ToVector2();

        //var spriteBatch = SubstituteProvider.GetSpriteBatch();

        var spriteBatch = Interceptor.For<SpriteBatch>()
            .Setup(x => x.DrawString(font, defaultMessage, expectedPosition, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, Layers.Text));

        spriteBatch.Ex
        text.Draw(spriteBatch, 1);
        spriteBatch.Received().DrawString(font, defaultMessage, expectedPosition, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, Layers.Text);
    }
}