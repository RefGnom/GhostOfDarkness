using Core.Extensions;
using FluentAssertions;
using Game.Controllers;
using Game.Controllers.InputServices;
using Microsoft.Xna.Framework;
using NSubstitute;

namespace GameTest.ControllersTest;

public class StickyCursorTest : TestBase
{
    [TestCase(0, 0)]
    [TestCase(0, 9.9f)]
    [TestCase(9.9f, 0)]
    [TestCase(9.9f, 9.9f)]
    [TestCase(5, 5)]
    public void TestStuckInnerBounds(float mouseX, float mouseY)
    {
        var outerBounds = new Rectangle(0, 0, 100, 100);
        var innerBounds = new Rectangle(0, 0, 10, 10);
        var mouseService = Substitute.For<IMouseService>();

        var stickyCursor = new StickyCursor(mouseService, outerBounds, innerBounds);

        mouseService.GetWindowPosition().Returns(new Vector2(mouseX, mouseY));
        mouseService.LeftButtonClicked().Returns(true);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.IsStuck.Should().BeTrue();
    }

    [TestCase(-0.1f, 5)]
    [TestCase(10f, 5)]
    [TestCase(5, -0.1f)]
    [TestCase(5, 10f)]
    public void TestNotStuckInnerBounds(float mouseX, float mouseY)
    {
        var outerBounds = new Rectangle(0, 0, 100, 100);
        var innerBounds = new Rectangle(0, 0, 10, 10);
        var mouseService = Substitute.For<IMouseService>();

        var stickyCursor = new StickyCursor(mouseService, outerBounds, innerBounds);

        mouseService.GetWindowPosition().Returns(new Vector2(mouseX, mouseY));
        mouseService.LeftButtonClicked().Returns(true);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.IsStuck.Should().BeFalse();
    }

    [Test]
    public void TestHoldInnerBoundsOnAttachDownRightAndInnerBoundsMustBeNotChange()
    {
        var outerBounds = new Rectangle(0, 0, 100, 100);
        var innerBounds = new Rectangle(0, 0, 10, 10);
        var mouseService = Substitute.For<IMouseService>();

        var stickyCursor = new StickyCursor(mouseService, outerBounds, innerBounds);

        mouseService.GetWindowPosition().Returns(new Vector2(9.9f, 9.9f));
        mouseService.LeftButtonClicked().Returns(true);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.InnerBounds.Should().Be(innerBounds);
    }

    [Test]
    public void TestHoldInnerBoundsOnAttachUpLeftAndInnerBoundsMustBeNotChange()
    {
        var outerBounds = new Rectangle(0, 0, 100, 100);
        var innerBounds = new Rectangle(0, 0, 10, 10);
        var mouseService = Substitute.For<IMouseService>();

        var stickyCursor = new StickyCursor(mouseService, outerBounds, innerBounds);

        mouseService.GetWindowPosition().Returns(new Vector2(0f, 0f));
        mouseService.LeftButtonClicked().Returns(true);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.InnerBounds.Should().Be(innerBounds);
    }

    [Test]
    public void TestMoveInnerBoundsOnAttachUpLeftAndInnerBoundsMustBeMove()
    {
        var outerBounds = new Rectangle(0, 0, 100, 100);
        var innerBounds = new Rectangle(0, 0, 10, 10);
        var mouseService = Substitute.For<IMouseService>();

        var stickyCursor = new StickyCursor(mouseService, outerBounds, innerBounds);

        var firstMousePosition = new Vector2(2, 2);
        var secondMousePosition = new Vector2(40, 40);

        mouseService.GetWindowPosition().Returns(firstMousePosition);
        mouseService.LeftButtonClicked().Returns(true);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.InnerBounds.Should().Be(innerBounds);

        mouseService.GetWindowPosition().Returns(secondMousePosition);
        mouseService.LeftButtonClicked().Returns(false);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.InnerBounds.Should().Be(innerBounds.Shift(secondMousePosition - firstMousePosition));
    }

    [Test]
    public void TestMoveInnerBoundsBeyondOuterBoundsAndInnerBoundsMustBeNotMove()
    {
        var outerBounds = new Rectangle(0, 0, 100, 100);
        var innerBounds = new Rectangle(0, 0, 10, 10);
        var mouseService = Substitute.For<IMouseService>();

        var stickyCursor = new StickyCursor(mouseService, outerBounds, innerBounds);

        var firstMousePosition = new Vector2(2, 2);
        var secondMousePosition = new Vector2(-50, -50);

        mouseService.GetWindowPosition().Returns(firstMousePosition);
        mouseService.LeftButtonClicked().Returns(true);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.InnerBounds.Should().Be(innerBounds);

        mouseService.GetWindowPosition().Returns(secondMousePosition);
        mouseService.LeftButtonClicked().Returns(false);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.InnerBounds.Should().Be(innerBounds);
    }

    [Test]
    public void TestMoveInnerBoundsBeyondOuterBoundsAndInnerBoundsMustBeMoveOnlyVertical()
    {
        var outerBounds = new Rectangle(0, 0, 100, 100);
        var innerBounds = new Rectangle(0, 0, 10, 10);
        var mouseService = Substitute.For<IMouseService>();

        var stickyCursor = new StickyCursor(mouseService, outerBounds, innerBounds);

        var firstMousePosition = new Vector2(2, 2);
        var secondMousePosition = new Vector2(-50, 50);

        mouseService.GetWindowPosition().Returns(firstMousePosition);
        mouseService.LeftButtonClicked().Returns(true);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.InnerBounds.Should().Be(innerBounds);

        mouseService.GetWindowPosition().Returns(secondMousePosition);
        mouseService.LeftButtonClicked().Returns(false);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.InnerBounds.Should().Be(innerBounds.Shift(0, (int)(secondMousePosition.Y - firstMousePosition.Y)));
    }

    [Test]
    public void TestMoveInnerBoundsBeyondOuterBoundsAndInnerBoundsMustBeMoveOnlyHorizontal()
    {
        var outerBounds = new Rectangle(0, 0, 100, 100);
        var innerBounds = new Rectangle(0, 0, 10, 10);
        var mouseService = Substitute.For<IMouseService>();

        var stickyCursor = new StickyCursor(mouseService, outerBounds, innerBounds);

        var firstMousePosition = new Vector2(2, 2);
        var secondMousePosition = new Vector2(50, -50);

        mouseService.GetWindowPosition().Returns(firstMousePosition);
        mouseService.LeftButtonClicked().Returns(true);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.InnerBounds.Should().Be(innerBounds);

        mouseService.GetWindowPosition().Returns(secondMousePosition);
        mouseService.LeftButtonClicked().Returns(false);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.InnerBounds.Should().Be(innerBounds.Shift((int)(secondMousePosition.X - firstMousePosition.X), 0));
    }

    [Test]
    public void TestCreateStickyCursorWithIndentInnerBoundsMustBeMoveIntoOuterBoundsWithIndent()
    {
        var outerBounds = new Rectangle(0, 0, 100, 100);
        var innerBounds = new Rectangle(0, 0, 10, 10);
        var indent = new Point(5, 3);
        var mouseService = Substitute.For<IMouseService>();

        var stickyCursor = new StickyCursor(mouseService, outerBounds, innerBounds, indent);

        stickyCursor.InnerBounds.Should().Be(innerBounds.Shift(indent));
    }

    [Test]
    public void TestMoveInnerBoundsBeyondOuterBoundsWithIndentAndInnerBoundsMustBeInsideOuterBoundsWithIndentHorizontal()
    {
        var outerBounds = new Rectangle(0, 0, 100, 100);
        var innerBounds = new Rectangle(0, 0, 10, 10);
        var indent = new Point(5, 3);
        var mouseService = Substitute.For<IMouseService>();

        var stickyCursor = new StickyCursor(mouseService, outerBounds, innerBounds, indent);

        var firstMousePosition = new Vector2(7, 7);
        var secondMousePosition = new Vector2(200, 50);

        mouseService.GetWindowPosition().Returns(firstMousePosition);
        mouseService.LeftButtonClicked().Returns(true);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.InnerBounds.Should().Be(innerBounds.Shift(indent));

        mouseService.GetWindowPosition().Returns(secondMousePosition);
        mouseService.LeftButtonClicked().Returns(false);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        var y = secondMousePosition.Y - firstMousePosition.Y + indent.Y;
        stickyCursor.InnerBounds.Should().Be(new Rectangle(85, (int)y, 10, 10));
    }

    [Test]
    public void TestMoveInnerBoundsBeyondOuterBoundsWithIndentAndInnerBoundsMustBeInsideOuterBoundsWithIndentVertical()
    {
        var outerBounds = new Rectangle(0, 0, 100, 100);
        var innerBounds = new Rectangle(0, 0, 10, 10);
        var indent = new Point(5, 3);
        var mouseService = Substitute.For<IMouseService>();

        var stickyCursor = new StickyCursor(mouseService, outerBounds, innerBounds, indent);

        var firstMousePosition = new Vector2(7, 7);
        var secondMousePosition = new Vector2(10, 500);

        mouseService.GetWindowPosition().Returns(firstMousePosition);
        mouseService.LeftButtonClicked().Returns(true);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.InnerBounds.Should().Be(innerBounds.Shift(indent));

        mouseService.GetWindowPosition().Returns(secondMousePosition);
        mouseService.LeftButtonClicked().Returns(false);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.InnerBounds.Should().Be(new Rectangle(8, 87, 10, 10));
    }

    [Test]
    public void TestMoveInnerBoundsBeyondOuterBoundsWithIndentAndInnerBoundsMustBeInsideOuterBoundsWithIndent()
    {
        var outerBounds = new Rectangle(0, 0, 100, 100);
        var innerBounds = new Rectangle(0, 0, 10, 10);
        var indent = new Point(3, 9);
        var mouseService = Substitute.For<IMouseService>();

        var stickyCursor = new StickyCursor(mouseService, outerBounds, innerBounds, indent);

        var firstMousePosition = new Vector2(7, 9);
        var secondMousePosition = new Vector2(1000, 1000);

        mouseService.GetWindowPosition().Returns(firstMousePosition);
        mouseService.LeftButtonClicked().Returns(true);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.InnerBounds.Should().Be(innerBounds.Shift(indent));

        mouseService.GetWindowPosition().Returns(secondMousePosition);
        mouseService.LeftButtonClicked().Returns(false);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.InnerBounds.Should().Be(new Rectangle(90 - indent.X, 90 - indent.Y, 10, 10));
    }

    [Test]
    public void TestMouseNotMoveMustBeInnerBoundsNotMove()
    {
        var outerBounds = new Rectangle(0, 0, 100, 100);
        var innerBounds = new Rectangle(0, 0, 10, 10);
        var mouseService = Substitute.For<IMouseService>();

        var stickyCursor = new StickyCursor(mouseService, outerBounds, innerBounds);

        var firstMousePosition = new Vector2(1, 1);
        var secondMousePosition = new Vector2(10, 10);
        var positionDelta = secondMousePosition - firstMousePosition;

        mouseService.GetWindowPosition().Returns(firstMousePosition);
        mouseService.LeftButtonClicked().Returns(true);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.InnerBounds.Should().Be(innerBounds);

        mouseService.GetWindowPosition().Returns(secondMousePosition);
        mouseService.LeftButtonClicked().Returns(false);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.InnerBounds.Should().Be(innerBounds.Shift(positionDelta));

        mouseService.GetWindowPosition().Returns(secondMousePosition);
        mouseService.LeftButtonClicked().Returns(false);
        mouseService.LeftButtonReleased().Returns(false);

        stickyCursor.Update();

        stickyCursor.InnerBounds.Should().Be(innerBounds.Shift(positionDelta));
    }

    [Test]
    public void TestToleranceOnMoveInnerBounds()
    {
        var outerBounds = new Rectangle(0, 0, 2000, 2000);
        var innerBounds = new Rectangle(0, 0, 10, 10);
        var mouseService = Substitute.For<IMouseService>();

        var stickyCursor = new StickyCursor(mouseService, outerBounds, innerBounds);

        var mousePosition = new Vector2(0, 0);
        const float delta = 1.05f;
        var positionDelta = new Vector2(delta, delta);

        var updateCount = (int)((outerBounds.Width - innerBounds.Width) / delta);
        for (var i = 0; i < updateCount; i++)
        {
            mouseService.GetWindowPosition().Returns(mousePosition);
            mouseService.LeftButtonClicked().Returns(i == 0);
            mouseService.LeftButtonReleased().Returns(false);

            stickyCursor.Update();

            Console.WriteLine($"mouse position {mousePosition} bounds {stickyCursor.InnerBounds}");
            stickyCursor.InnerBounds.Should().Be(innerBounds.Shift(mousePosition));
            mousePosition += positionDelta;
        }
    }
}