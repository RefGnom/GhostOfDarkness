using game.Creatures;
using game.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game.UI;

internal class HealthBar : Interfaces.IDrawable
{
    private readonly Player player;
    private Vector2 origin;
    private readonly float scale = 0.7f;

    public HealthBar(Player player)
    {
        this.player = player;
        origin = new Vector2(TexturesManager.HealthBarBackground.Width, 0);
        GameManager.Instance.Drawer.Register(this);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var position = new Vector2(GameManager.Instance.Game.WindowWidth - 15, 15);
        var localScale = GameManager.Instance.Game.WindowWidth / 1920f;
        var percent = player.Health / player.MaxHealth;
        var width = TexturesManager.HealthBarBackground.Width * percent;
        var height = TexturesManager.HealthBarBackground.Height;
        var healtRectangle = new Rectangle(0, 0, (int)width, height);
        spriteBatch.Draw(TexturesManager.HealthBarBackground, position, null, Color.White, 0, origin, scale * localScale, SpriteEffects.None, 0);
        spriteBatch.Draw(TexturesManager.HealthBarForeground, position, healtRectangle, Color.White, 0, origin, scale * localScale, SpriteEffects.None, 0);
    }
}