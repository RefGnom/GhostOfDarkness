using game.Creatures;
using game.Managers;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace game.Tests;

internal class PlayerTest : Player
{
    public PlayerTest(Vector2 position, float speed, float cooldown) : base(position, speed, 100, cooldown)
    {
        Direction = Vector2.One;
        GameManager.Instance.Drawer.Unregister(view);
    }

    public new void Shoot()
    {
        base.Shoot();
    }

    public void Update(float deltaTime)
    {
        UpdateBullets(deltaTime);
    }
}

[TestFixture]
public class BulletTest
{
    [Test]
    public void TestCountBullets()
    {
        var player = new PlayerTest(Vector2.Zero, 0, -1);
        var count = 10;
        var countCallUpdate = 2;
        for (int i = 0; i < count; i++)
        {
            player.Shoot();
        }
        player.Update(1.2f / countCallUpdate);
        Assert.AreEqual(count, player.Bullets.Count);
        player.Update(1.2f / countCallUpdate);
        Assert.AreEqual(0, player.Bullets.Count);
    }

    [Test]
    public void TestUpdateBullets()
    {
        var player = new PlayerTest(Vector2.Zero, 0, -1);
        var count = 10;
        var bulletSpeed = 600;
        var deltaTime = 0.2f;
        for (int i = 0; i < count; i++)
        {
            player.Shoot();
            player.Update(deltaTime);
            for (int j = player.Bullets.Count; j > 0; j--)
            {
                var coordinate = bulletSpeed * deltaTime * j;
                var expectedPosition = new Vector2(coordinate, coordinate);
                Assert.AreEqual(expectedPosition, player.Bullets[^j].Position);
            }
        }
    }
}