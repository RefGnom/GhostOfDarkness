using Game.Controllers;
using Microsoft.Xna.Framework;

namespace GameTest.ControllersTest;


public class StickyCursorTest : TestBase
{
    [Test]
    public void Test()
    {
        var outerBounds = new Rectangle(0, 0, 100, 100);
        var innerBounds = new Rectangle(0, 0, 10, 10);

        var cursor = new StickyCursor(outerBounds, innerBounds);
    }
}