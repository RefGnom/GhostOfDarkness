using game.Interfaces;
using game.View;
using System.Collections.Generic;

namespace game.CreatureStates;

internal class KilledState : CreatureState
{
    public KilledState(IStateSwitcher stateSwitcher, Animator animator, Dictionary<string, int> animations) : base(stateSwitcher, animator, animations)
    {
        Killed = true;
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

    public override void Start()
    {
    }

    public override void Stop()
    {
    }

    public override void Update(float deltaTime)
    {
    }

    public override void Kill()
    {
    }
}