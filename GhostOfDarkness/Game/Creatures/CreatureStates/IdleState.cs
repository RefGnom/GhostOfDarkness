using game;
using Game.Interfaces;

namespace Game.Creatures.CreatureStates;

internal class IdleState : CreatureState
{
    public IdleState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
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
        TimeLeft = OnStarted.Invoke();
    }

    public override void Stop()
    {
    }

    public override void Update(float deltaTime)
    {
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