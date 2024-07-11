using System;
using System.Collections.Generic;
using Core.DependencyInjection;
using Game.Graphics;
using Game.Interfaces;

namespace Game.Game.GameStates;

[DiUsage]
internal abstract class GameState : IState, IDrawable, IUpdateable
{
    private IGameStateSwitcher switcher;

    public IGameStateSwitcher Switcher
    {
        get
        {
            if (switcher == null)
            {
                throw new InvalidOperationException("Switcher must be initialized");
            }

            return switcher;
        }
        set
        {
            if (switcher != null)
            {
                throw new InvalidOperationException("Switcher already initialized");
            }

            switcher = value;
        }
    }

    protected GameState PreviousState;
    protected readonly List<IComponent> Components = [];
    protected readonly List<IDrawable> Drawables = [];
    private readonly List<IUpdateable> updatables = [];

    public bool IsConfirmed { get; protected set; }
    public bool GameIsExit { get; protected set; }
    public bool Saved { get; protected set; }

    public void Start(IState previousState)
    {
        var state = (GameState)previousState;
        PreviousState = state;
        Start(state);
    }

    public virtual void Update(float deltaTime)
    {
        foreach (var component in Components)
        {
            component.Update(deltaTime);
        }

        foreach (var updatable in updatables)
        {
            updatable.Update(deltaTime);
        }
    }

    public virtual void Draw(ISpriteBatch spriteBatch, float scale)
    {
        foreach (var component in Components)
        {
            component.Draw(spriteBatch, scale);
        }

        foreach (var drawable in Drawables)
        {
            drawable.Draw(spriteBatch, scale);
        }
    }

    public virtual void Start(GameState previousState)
    {
    }

    public virtual void Stop()
    {
    }

    public virtual void Back()
    {
    }

    public virtual void NewGame()
    {
    }

    public virtual void LoadSave()
    {
    }

    public virtual void OpenSettings()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void Play()
    {
    }

    public virtual void Dead()
    {
    }

    public virtual void Restart()
    {
    }

    public virtual void Confirm()
    {
    }

    public virtual void Save()
    {
    }
}