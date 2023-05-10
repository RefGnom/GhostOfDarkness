using System;
using System.Collections.Generic;
using System.Linq;
using game.Interfaces;
using game.View;

namespace game.CreatureStates;

internal class CreatureStatesController : IStateSwitcher
{
    private CreatureState currentState;
    private readonly List<CreatureState> states;

    protected Animator animator;

    public bool Killed => currentState.Killed;
    public bool CanAttack => currentState.CanAttack;
    public bool CanMove => currentState.CanMove;

    public CreatureStatesController(Animator animator, Dictionary<string, int> animations)
    {
        this.animator = animator;

        states = new List<CreatureState>()
        {
            new IdleState(this, animator, animations),
            new RunState(this, animator, animations),
            new FightState(this, animator, animations),
            new AttackState(this, animator, animations),
            new TakeDamageState(this, animator, animations),
            new DeadState(this, animator, animations),
            new KilledState(this, animator, animations)
        };
        currentState = states[0];
    }

    public void SwitchState<T>() where T : CreatureState
    {
        var state = states.FirstOrDefault(s => s is T);
        currentState.Stop();
        state.Start();
        currentState = state;
    }

    public void Update(float deltaTime) => currentState.Update(deltaTime);

    public Type GetStateType() => currentState.GetType();

    protected void SetStateIdle() => SwitchState<IdleState>();

    protected void SetStateRun() => currentState.Run();

    protected void SetStateAttack() => currentState.Attack();

    protected void SetStateTakeDamage() => currentState.TakeDamage();

    protected void SetStateDead() => currentState.Kill();
}