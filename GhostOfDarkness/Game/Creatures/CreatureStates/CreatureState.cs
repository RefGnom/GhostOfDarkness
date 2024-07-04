using System;
using game;
using Game.Interfaces;

namespace Game.Creatures.CreatureStates;

public abstract class CreatureState : IState
{
    protected float TimeLeft;
    protected readonly IStateSwitcher Switcher;

    public bool CanAttack { get; protected set; }
    public bool CanMove { get; protected set; }
    public bool Killed { get; protected set; }
    public bool CanDelete { get; protected set; }

    public Func<float> OnStarted;
    public Func<float> OnUpdate;

    protected CreatureState(IStateSwitcher stateSwitcher)
    {
        Switcher = stateSwitcher;
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