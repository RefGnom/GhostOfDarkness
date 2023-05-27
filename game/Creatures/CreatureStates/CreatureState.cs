using System;

namespace game;

internal abstract class CreatureState : IState
{
    protected float timeLeft;
    protected readonly IStateSwitcher switcher;

    public bool CanAttack { get; protected set; }
    public bool CanMove { get; protected set; }
    public bool Killed { get; protected set; }
    public bool CanDelete { get; protected set; }

    public Func<float> OnStarted;

    public CreatureState(IStateSwitcher stateSwitcher)
    {
        switcher = stateSwitcher;
    }

    public abstract void Start(IState previousState);

    public abstract void Stop();

    public abstract void Run();

    public abstract void Idle();

    public abstract void Attack();

    public abstract void TakeDamage();

    public abstract void Kill();

    public abstract void Update(float deltaTime);
}