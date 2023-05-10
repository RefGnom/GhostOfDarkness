using game.Interfaces;
using game.View;
using System.Collections.Generic;

namespace game.CreatureStates;

internal class DeadState : CreatureState
{
    private float leftTime;

    public DeadState(IStateSwitcher stateSwitcher, Animator animator, Dictionary<string, int> animations)
        : base(stateSwitcher, animator, animations)
    {
        CanAttack = false;
        CanMove = false;
    }

    public override void Attack()
    {
    }

    public override void Run()
    {
    }

    public override void TakeDamage()
    {
    }

    public override void Start()
    {
        Killed = true;
        Animator.SetAnimation(animations["dead"], false);
        leftTime = Animator.GetAnimationTime(animations["dead"]) * 10;
    }

    public override void Stop()
    {
    }

    public override void Update(float deltaTime)
    {
        leftTime -= deltaTime;
        if (leftTime <= 0)
            CanDelete = true;
    }

    public override void Kill()
    {
    }
}