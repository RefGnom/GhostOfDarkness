using System.Collections.Generic;

namespace game;

internal class FightState : CreatureState
{
    private readonly float activeTime = 3;
    private float leftTime;

    public FightState(IStateSwitcher stateSwitcher, Animator animator, Dictionary<string, int> animations)
        : base(stateSwitcher, animator, animations)
    {
        CanMove = true;
        CanAttack = true;
    }

    public override void Attack()
    {
        switcher.SwitchState<AttackState>();
    }

    public override void Run()
    {
        switcher.SwitchState<RunState>();
    }

    public override void TakeDamage()
    {
        switcher.SwitchState<TakeDamageState>();
    }

    public override void Start(IState previousState)
    {
        Animator.SetAnimation(animations["idle"]);
        leftTime = activeTime;
    }

    public override void Stop()
    {
    }

    public override void Update(float deltaTime)
    {
        leftTime -= deltaTime;
        if (leftTime <= 0)
            switcher.SwitchState<IdleState>();
    }

    public override void Kill()
    {
        switcher.SwitchState<DeadState>();
    }

    public override void Idle()
    {
    }
}