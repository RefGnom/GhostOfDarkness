using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game;

internal abstract class CreatureView : CreatureStatesController, IDrawable
{
    public CreatureView(Animator animator, Dictionary<string, int> animations) : base(animator, animations)
    {
    }

    public void Attack() => SetStateAttack();

    public void TakeDamage() => SetStateTakeDamage();

    public void Kill() => SetStateDead();

    public void Idle() => SetStateIdle();

    public void Run() => SetStateRun();

    public abstract void Draw(SpriteBatch spriteBatch, float scale);

    public virtual void Update(float deltaTime)
    {
        UpdateState(deltaTime);
    }
}