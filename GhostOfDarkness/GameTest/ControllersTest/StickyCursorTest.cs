using Game.Controllers;
using Game.Controllers.InputServices;
using Microsoft.Xna.Framework;
using NSubstitute;

namespace GameTest.ControllersTest;


public class StickyCursorTest : TestBase
{
    [Test]
    public void TestHoldInnerBoundsOnDownRightAndBoundsMustBeNotChange()
    {
        var outerBounds = new Rectangle(0, 0, 100, 100);
        var innerBounds = new Rectangle(0, 0, 10, 10);
        var mouseService = Substitute.For<IMouseService>();

        var stickyCursor = new StickyCursor(mouseService, outerBounds, innerBounds);
    }
}