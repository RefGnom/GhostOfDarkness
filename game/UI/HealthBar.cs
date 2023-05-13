using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class HealthBar : IDrawable
{
    private readonly Player player;
    private Vector2 origin;
    private readonly float scale = 0.7f;

    public HealthBar(Player player)
    {
        this.player = player;
        origin = new Vector2(TexturesManager.HealthBarBackground.Width, 0);
        GameManager.Instance.Drawer.RegisterUI(this);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var position = new Vector2(GameManager.Instance.Game.WindowWidth - 15, 15);
        var localScale = GameManager.Instance.Game.WindowWidth / 1920f;
        var percent = player.Health / player.MaxHealth;
        var width = TexturesManager.HealthBarBackground.Width * percent;
        var height = TexturesManager.HealthBarBackground.Height;
        var healtRectangle = new Rectangle(0, 0, (int)width, height);
        spriteBatch.Draw(TexturesManager.HealthBarBackground, position, null, Color.White, 0, origin, scale * localScale, SpriteEffects.None, Layers.UI);
        spriteBatch.Draw(TexturesManager.HealthBarForeground, position, healtRectangle, Color.White, 0, origin, scale * localScale, SpriteEffects.None, Layers.UI);
    }
}