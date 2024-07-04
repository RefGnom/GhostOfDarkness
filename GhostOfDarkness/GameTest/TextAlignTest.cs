using Game.Enums;
using Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NSubstitute;

namespace GameTest;

public class TextAlignTest : TestBase
{
    private Rectangle defaultBounds;

    [SetUp]
    public void Setup()
    {
        defaultBounds = new Rectangle(100, 100, 400, 200);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 5)]
    [TestCase(5, 5)]
    public void TestWithoutAlign(int indentX, int indentY)
    {
        var expectedPosition = defaultBounds.Center.ToVector2();
        AssertCommonTest(0, indentX, indentY, expectedPosition);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 5)]
    [TestCase(5, 5)]
    public void TestAlignUp(int indentX, int indentY)
    {
        var expectedY = defaultBounds.Top + indentY;
        var expectedX = defaultBounds.Center.X;
        AssertCommonTest(Align.Up, indentX, indentY, new Vector2(expectedX, expectedY));
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 5)]
    [TestCase(5, 5)]
    public void TestAlignDown(int indentX, int indentY)
    {
        var expectedY = defaultBounds.Bottom - indentY;
        var expectedX = defaultBounds.Center.X;
        AssertCommonTest(Align.Down, indentX, indentY, new Vector2(expectedX, expectedY));
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 5)]
    [TestCase(5, 5)]
    public void TestAlignLeft(int indentX, int indentY)
    {
        var expectedY = defaultBounds.Center.Y;
        var expectedX = defaultBounds.Left + indentX;
        AssertCommonTest(Align.Left, indentX, indentY, new Vector2(expectedX, expectedY));
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 5)]
    [TestCase(5, 5)]
    public void TestAlignRight(int indentX, int indentY)
    {
        var expectedY = defaultBounds.Center.Y;
        var expectedX = defaultBounds.Right - indentX;
        AssertCommonTest(Align.Right, indentX, indentY, new Vector2(expectedX, expectedY));
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 5)]
    [TestCase(5, 5)]
    public void TestFullAlign(int indentX, int indentY)
    {
        var expectedPosition = defaultBounds.Center.ToVector2();
        AssertCommonTest(Align.Up | Align.Down | Align.Left | Align.Right, indentX, indentY, expectedPosition);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 5)]
    [TestCase(5, 5)]
    public void TestAlignUpLeft(int indentX, int indentY)
    {
        var expectedY = defaultBounds.Top + indentY;
        var expectedX = defaultBounds.Left + indentX;
        AssertCommonTest(Align.Up | Align.Left, indentX, indentY, new Vector2(expectedX, expectedY));
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 5)]
    [TestCase(5, 5)]
    public void TestAlignUpRight(int indentX, int indentY)
    {
        var expectedY = defaultBounds.Top + indentY;
        var expectedX = defaultBounds.Right - indentX;
        AssertCommonTest(Align.Up | Align.Right, indentX, indentY, new Vector2(expectedX, expectedY));
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 5)]
    [TestCase(5, 5)]
    public void TestAlignDownLeft(int indentX, int indentY)
    {
        var expectedY = defaultBounds.Bottom - indentY;
        var expectedX = defaultBounds.Left + indentX;
        AssertCommonTest(Align.Down | Align.Left, indentX, indentY, new Vector2(expectedX, expectedY));
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 5)]
    [TestCase(5, 5)]
    public void TestAlignDownRight(int indentX, int indentY)
    {
        var expectedY = defaultBounds.Bottom - indentY;
        var expectedX = defaultBounds.Right - indentX;
        AssertCommonTest(Align.Down | Align.Right, indentX, indentY, new Vector2(expectedX, expectedY));
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 5)]
    [TestCase(5, 5)]
    public void TestAlignUpLeftRight(int indentX, int indentY)
    {
        var expectedY = defaultBounds.Top + indentY;
        var expectedX = defaultBounds.Center.X;
        AssertCommonTest(Align.Up | Align.Left | Align.Right, indentX, indentY, new Vector2(expectedX, expectedY));
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 5)]
    [TestCase(5, 5)]
    public void TestAlignDownLeftRight(int indentX, int indentY)
    {
        var expectedY = defaultBounds.Bottom - indentY;
        var expectedX = defaultBounds.Center.X;
        AssertCommonTest(Align.Down | Align.Left | Align.Right, indentX, indentY, new Vector2(expectedX, expectedY));
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 5)]
    [TestCase(5, 5)]
    public void TestAlignLeftUpDown(int indentX, int indentY)
    {
        var expectedY = defaultBounds.Center.Y;
        var expectedX = defaultBounds.Left + indentX;
        AssertCommonTest(Align.Left | Align.Up | Align.Down, indentX, indentY, new Vector2(expectedX, expectedY));
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 5)]
    [TestCase(5, 5)]
    public void TestAlignRightUpDown(int indentX, int indentY)
    {
        var expectedY = defaultBounds.Center.Y;
        var expectedX = defaultBounds.Right - indentX;
        AssertCommonTest(Align.Right | Align.Up | Align.Down, indentX, indentY, new Vector2(expectedX, expectedY));
    }

    private void AssertCommonTest(Align align, int indentX, int indentY, Vector2 expectedPosition)
    {
        const string defaultMessage = ""; // Всегда должна быть пустой, т.к. SpriteFont - не мок, а настоящий класс, но кастрированный
        var font = SubstituteProvider.GetFont();
        var text = new Text(defaultBounds, defaultMessage, font, align, indentX, indentY);

        var spriteBatch = SubstituteProvider.GetSpriteBatch();
        const float scale = 1;
        text.Draw(spriteBatch, scale);
        spriteBatch.Received().DrawString(font,
            defaultMessage,
            expectedPosition,
            Arg.Any<Color>(),
            Arg.Any<float>(),
            Arg.Any<Vector2>(),
            scale,
            Arg.Any<SpriteEffects>(),
            Arg.Any<float>()
        );
    }
}