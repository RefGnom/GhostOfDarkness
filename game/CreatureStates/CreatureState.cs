using game.Interfaces;
using game.View;
using System.Collections.Generic;

namespace game.CreatureStates;

internal abstract class CreatureState
{
    protected readonly IStateSwitcher stateSwitcher;
    public Animator Animator { get; private set; }
    protected readonly Dictionary<string, int> animations;

    public CreatureState(IStateSwitcher stateSwitcher)
    {
        this.stateSwitcher = stateSwitcher;
    }

    public abstract void Start();

    public abstract void Stop();

    public abstract void Run();

    public abstract void StartFight();

    public abstract void Attack();

    public abstract void SetHealth(float health);

    public abstract void Dead();

    public abstract void Update(float deltaTime);
}