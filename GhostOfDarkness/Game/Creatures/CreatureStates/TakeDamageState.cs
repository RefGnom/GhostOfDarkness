using game;
using Game.Interfaces;

namespace Game.Creatures.CreatureStates;

internal class TakeDamageState : CreatureState
{
    public TakeDamageState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
        CanAttack = false;
        CanMove = false;
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
        TimeLeft -= deltaTime;
        if (TimeLeft <= 0)
        {
            Switcher.SwitchState<FightState>();
        }
        OnUpdate?.Invoke();
    }

    public override void Kill()
    {
        Switcher.SwitchState<DeadState>();
    }

    public override void Run()
    {
    }

    public override void Idle()
    {
    }

    public override void Attack()
    {
    }
}