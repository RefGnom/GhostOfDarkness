using Game.Interfaces;

namespace Game.Creatures.CreatureStates;

internal class AttackState : CreatureState
{
    public AttackState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
        CanMove = false;
        CanAttack = true;
    }

    public override void Attack()
    {
    }

    public override void Run()
    {
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
    }

    public override void Idle()
    {
    }
}