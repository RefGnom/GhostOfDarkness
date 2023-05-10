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
        Animator.SetAnimation(animations["dead"]);
        leftTime = Animator.GetAnimationTime(animations["dead"]);
    }

    public override void Stop()
    {
    }

    public override void Update(float deltaTime)
    {
        leftTime -= deltaTime;
        if (leftTime <= 0)
            stateSwitcher.SwitchState<KilledState>();
    }

    public override void Kill()
    {
    }
}