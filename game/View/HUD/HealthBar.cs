using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class HealthBar : IDrawable
{
    private Vector2 origin;
    private readonly float localScale = 0.7f;
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

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        var position = new Vector2(1920 - 15, 15);
        var percent = health / maxHealth;
        var width = TexturesManager.HealthBarBackground.Width * percent;
        var height = TexturesManager.HealthBarBackground.Height;
        var healthRectangle = new Rectangle(0, 0, (int)width, height);
        spriteBatch.Draw(TexturesManager.HealthBarBackground, position * scale, null, Color.White, 0, origin, localScale * scale, SpriteEffects.None, Layers.HUDBackground);
        spriteBatch.Draw(TexturesManager.HealthBarForeground, position * scale, healthRectangle, Color.White, 0, origin, localScale * scale, SpriteEffects.None, Layers.HUDForeground);
    }
}