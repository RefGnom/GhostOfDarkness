using Game.Interfaces;

namespace game;

internal class FightState : CreatureState
{
    private float leftTime;

    public FightState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
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
        leftTime =  OnStarted.Invoke();
    }

    public override void Stop()
    {
    }

    public override void Update(float deltaTime)
    {
        leftTime -= deltaTime;
        if (leftTime <= 0)
            switcher.SwitchState<IdleState>();
        OnUpdate?.Invoke();
    }

    public override void Kill()
    {
        switcher.SwitchState<DeadState>();
    }

    public override void Idle()
    {
    }
}