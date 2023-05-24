using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game;

internal class EnemyView : CreatureView
{
    protected float scaleFactor = 1;
    private Enemy model;

    public int Radius => animator.Radius;

    public EnemyView(Animator animator, Dictionary<string, int> animations) : base(animator, animations)
    {
    }

    public void SetModel(Enemy model)
    {
        this.model = model;
    }

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        var flip = model.Direction.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        animator.Draw(model.Position, spriteBatch, flip, Layers.Creatures, scaleFactor);
        var origin = new Vector2(model.Hitbox.Width / 2, model.Hitbox.Height / 2);
        if (Settings.ShowHitboxes)
            HitboxManager.DrawHitbox(spriteBatch, model.Position, model.Hitbox, origin);
    }
}