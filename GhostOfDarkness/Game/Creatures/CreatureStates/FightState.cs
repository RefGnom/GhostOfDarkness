using Game.Interfaces;

namespace Game.Creatures.CreatureStates;

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
        Switcher.SwitchState<AttackState>();
    }

    public override void Run()
    {
        Switcher.SwitchState<RunState>();
    }

    public override void TakeDamage()
    {
        Switcher.SwitchState<TakeDamageState>();
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
        {
            Switcher.SwitchState<IdleState>();
        }

        OnUpdate?.Invoke();
    }

    public override void Kill()
    {
        Switcher.SwitchState<DeadState>();
    }

    public override void Idle()
    {
    }
}