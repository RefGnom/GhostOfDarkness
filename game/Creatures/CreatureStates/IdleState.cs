using game.Interfaces;
using game.View;
using System.Collections.Generic;

namespace game.Creatures.CreatureStates;

internal class IdleState : CreatureState
{
    public IdleState(IStateSwitcher stateSwitcher, Animator animator, Dictionary<string, int> animations) : base(stateSwitcher, animator, animations)
    {
        CanMove = true;
        CanAttack = true;
    }

    public override void Attack()
    {
        stateSwitcher.SwitchState<AttackState>();
    }

    public override void Run()
    {
        stateSwitcher.SwitchState<RunState>();
    }

    public override void TakeDamage()
    {
        stateSwitcher.SwitchState<TakeDamageState>();
    }

    public override void Start()
    {
        Animator.SetAnimation(animations["idle"]);
    }

    public override void Stop()
    {
    }

    public override void Update(float deltaTime)
    {
    }

    public override void Kill()
    {
        stateSwitcher.SwitchState<DeadState>();
    }

    public override void Idle()
    {
    }
}