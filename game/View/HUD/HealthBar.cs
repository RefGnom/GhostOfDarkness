using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class HealthBar : IDrawable
{
    private Vector2 origin;
    private readonly float scale = 0.7f;
    private readonly float maxHealth;
    private float health;

    public HealthBar(float maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
        origin = new Vector2(TexturesManager.HealthBarBackground.Width, 0);
        GameManager.Instance.Drawer.RegisterHUD(this);
    }

    public void SetHealth(float health)
    {
        this.health = health;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var position = new Vector2(GameManager.Instance.Game.WindowWidth - 15, 15);
        var localScale = GameManager.Instance.Game.WindowWidth / 1920f;
        var percent = health / maxHealth;
        var width = TexturesManager.HealthBarBackground.Width * percent;
        var height = TexturesManager.HealthBarBackground.Height;
        var healthRectangle = new Rectangle(0, 0, (int)width, height);
        spriteBatch.Draw(TexturesManager.HealthBarBackground, position, null, Color.White, 0, origin, scale * localScale, SpriteEffects.None, Layers.HUD);
        spriteBatch.Draw(TexturesManager.HealthBarForeground, position, healthRectangle, Color.White, 0, origin, scale * localScale, SpriteEffects.None, Layers.HUD);
    }
}