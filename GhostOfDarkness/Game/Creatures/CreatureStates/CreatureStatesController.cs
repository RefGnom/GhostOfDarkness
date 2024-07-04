using System;
using System.Collections.Generic;
using System.Linq;
using Core.DependencyInjection;
using game;
using Game.Graphics;
using Game.Interfaces;

namespace Game.Creatures.CreatureStates;

[DiIgnore]
public abstract class CreatureStatesController : IStateSwitcher, IDrawable
{
    private CreatureState currentState;
    private List<CreatureState> states;

    public bool CanAttack => currentState.CanAttack;
    public bool CanMove => currentState.CanMove;
    public bool Killed => currentState.Killed;
    public bool CanDelete => currentState.CanDelete;

    protected void SetStates(List<CreatureState> statesList)
    {
        this.states = statesList;
        currentState = statesList.FirstOrDefault(s => s is IdleState);
        if (currentState is null)
        {
            throw new ArgumentException("expected idle state but was null");
        }

        currentState.Start(null);
    }

    public void SwitchState<T>() where T : IState
    {
        var state = states.FirstOrDefault(s => s is T);
        SwitchState(state);
    }

    public void SwitchState(IState state)
    {
        currentState.Stop();
        state.Start(currentState);
        currentState = (CreatureState)state;
    }

    public virtual void Update(float deltaTime)
    {
        currentState.Update(deltaTime);
    }

    public Type GetStateType() => currentState.GetType();

    public void SetStateIdle() => currentState.Idle();

    public void SetStateRun() => currentState.Run();

    public void SetStateAttack() => currentState.Attack();

    public void SetStateTakeDamage() => currentState.TakeDamage();

    public void SetStateDead() => currentState.Kill();

    public abstract void Draw(ISpriteBatch spriteBatch, float scale);
}