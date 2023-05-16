using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace game;

internal class EnemyView : CreatureView
{
    private Vector2 position;
    private Vector2 direction;

    public int Radius => animator.Radius;

    public event Func<Vector2> PositionChanged;
    public event Func<Vector2> DirectionChanged;

    public EnemyView(Animator animator, Dictionary<string, int> animations) : base(animator, animations)
    {
    }

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        position = PositionChanged.Invoke();
        direction = DirectionChanged.Invoke();
        var flip = direction.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        animator.Draw(position, spriteBatch, flip, Layers.Creatures);
        if (Settings.ShowHitboxes)
            HitboxManager.DrawHitbox(spriteBatch, position, HitboxManager.MeleeEnemy, new Vector2(11, 11));
    }
}