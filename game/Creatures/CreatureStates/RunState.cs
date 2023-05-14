using System.Collections.Generic;

namespace game;

internal class RunState : CreatureState
{
    public RunState(IStateSwitcher stateSwitcher, Animator animator, Dictionary<string, int> animations) : base(stateSwitcher, animator, animations)
    {
        CanMove = true;
        CanAttack = true;
    }

    public override void TakeDamage()
    {
        switcher.SwitchState<TakeDamageState>();
    }

    public override void Start(IState previousState)
    {
        Animator.SetAnimation(animations["run"]);
    }

    public override void Stop()
    {
    }

    public override void Update(float deltaTime)
    {
    }

    public override void Run()
    {
    }

    public override void Attack()
    {
        switcher.SwitchState<AttackState>();
    }

    public override void Kill()
    {
        switcher.SwitchState<DeadState>();
    }

    public override void Idle()
    {
        switcher.SwitchState<IdleState>();
    }
}