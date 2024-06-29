using Game.Interfaces;

namespace game;

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
        switcher.SwitchState<TakeDamageState>();
    }

    public override void Start(IState previousState)
    {
        timeLeft = OnStarted.Invoke();
    }

    public override void Stop()
    {

    }

    public override void Update(float deltaTime)
    {
        timeLeft -= deltaTime;
        if (timeLeft <= 0)
            switcher.SwitchState<FightState>();
        OnUpdate?.Invoke();
    }

    public override void Kill()
    {
    }

    public override void Idle()
    {
    }
}