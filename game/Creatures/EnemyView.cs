using game.CreatureStates;
using game.Interfaces;
using game.View;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game.Creatures;

internal class EnemyView : CreatureStatesController, IDrawable
{
    private Enemy model;
    public int Radius => animator.Radius;

    public EnemyView(Enemy model, Animator animator, Dictionary<string, int> animations) : base(animator, animations)
    {
        this.model = model;
    }

    public void Attack() => SetStateAttack();

    public void TakeDamage() => SetStateTakeDamage();

    public void Kill() => SetStateDead();

    public void Idle() => SetStateIdle();

    public void Run() => SetStateRun();

    public void Draw(SpriteBatch spriteBatch)
    {
        var flip = model.Direction.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        animator.Draw(model.Position, spriteBatch, flip);
    }
}