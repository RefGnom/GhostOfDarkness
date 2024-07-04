using Core.DependencyInjection;
using Game.Graphics;
using Game.Managers;
using Game.Service;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Game.Interfaces.IDrawable;

namespace game;

[DiIgnore]
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
        origin = new Vector2(Textures.HealthBarBackground.Width, 0);
        GameManager.Instance.Drawer.RegisterHud(this);
    }

    public void SetHealth(float health)
    {
        this.health = health;
    }

    public void Draw(ISpriteBatch spriteBatch, float scale)
    {
        var position = new Vector2(1920 - 15, 15);
        var percent = health / maxHealth;
        var width = Textures.HealthBarBackground.Width * percent;
        var height = Textures.HealthBarBackground.Height;
        var healthRectangle = new Rectangle(0, 0, (int)width, height);
        spriteBatch.Draw(Textures.HealthBarBackground, position * scale, null, Color.White, 0, origin, localScale * scale, SpriteEffects.None, Layers.HudBackground);
        spriteBatch.Draw(Textures.HealthBarForeground, position * scale, healthRectangle, Color.White, 0, origin, localScale * scale, SpriteEffects.None, Layers.HudForeground);
    }
}