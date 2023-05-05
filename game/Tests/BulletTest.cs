using game.Creatures;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace game.Tests
{
    [TestFixture]
    public class BulletTest
    {
        [Test]
        public void TestCountBullets()
        {
            var player = new Player(Vector2.Zero, 0);
            var count = 10;
            var countCallUpdate = 2;
            for (int i = 0; i < count; i++)
            {
                player.Shoot(Vector2.Zero);
            }
            player.Update(1.2f / countCallUpdate, 0, 0);
            Assert.AreEqual(count, player.Bullets.Count);
            player.Update(1.2f / countCallUpdate, 0, 0);
            Assert.AreEqual(0, player.Bullets.Count);
        }

        [Test]
        public void TestUpdateBullets()
        {
            var player = new Player(Vector2.Zero, 0);
            var count = 10;
            var bulletSpeed = 600;
            var deltaTime = 0.2f;
            for (int i = 0; i < count; i++)
            {
                player.Shoot(Vector2.One);
                player.Update(deltaTime, 0, 0);
                for (int j = player.Bullets.Count; j > 0; j--)
                {
                    var coordinate = bulletSpeed * deltaTime * j;
                    var expectedPosition = new Vector2(coordinate, coordinate);
                    Assert.AreEqual(expectedPosition, player.Bullets[^j].Position);
                }
            }
        }
    }
}