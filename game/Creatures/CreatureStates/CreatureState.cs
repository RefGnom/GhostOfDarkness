using System.Collections.Generic;

namespace game;

internal abstract class CreatureState
{
    protected readonly IStateSwitcher stateSwitcher;
    public readonly Animator Animator;
    protected readonly Dictionary<string, int> animations;

    public bool CanAttack { get; protected set; }
    public bool CanMove { get; protected set; }
    public bool Killed { get; protected set; }
    public bool CanDelete { get; protected set; }

    public CreatureState(IStateSwitcher stateSwitcher, Animator animator, Dictionary<string, int> animations)
    {
        this.stateSwitcher = stateSwitcher;
        Animator = animator;
        this.animations = animations;
    }

    public abstract void Start();

    public abstract void Stop();

    public abstract void Run();

    public abstract void Idle();

    public abstract void Attack();

    public abstract void TakeDamage();

    public abstract void Kill();

    public abstract void Update(float deltaTime);
}