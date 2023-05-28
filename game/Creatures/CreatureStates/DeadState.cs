namespace game;

internal class DeadState : CreatureState
{
    public DeadState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
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

    public override void Start(IState previousState)
    {
        Killed = true;
        timeLeft = OnStarted.Invoke();
    }

    public override void Stop()
    {
    }

    public override void Update(float deltaTime)
    {
        timeLeft -= deltaTime;
        if (timeLeft <= 0)
            CanDelete = true;
        OnUpdate?.Invoke();
    }

    public override void Kill()
    {
    }

    public override void Idle()
    {
    }
}