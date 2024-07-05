using System.Collections.Generic;
using Game.Graphics;
using Game.Interfaces;

namespace Game.Game.GameStates;

internal abstract class GameState : IState, IDrawable, IUpdateable
{
    protected readonly IGameStateSwitcher Switcher;
    protected GameState PreviousState;
    protected readonly List<IComponent> Components;
    protected readonly List<IDrawable> Drawables;
    private readonly List<IUpdateable> updatables;

    public bool IsConfirmed { get; protected set; }
    public bool GameIsExit { get; protected set; }
    public bool Saved { get; protected set; }

    protected GameState(IGameStateSwitcher stateSwitcher)
    {
        Switcher = stateSwitcher;
        Components = new List<IComponent>();
        Drawables = new List<IDrawable>();
        updatables = new List<IUpdateable>();
    }

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

    public virtual void Start(GameState previousState) { }

    public virtual void Stop() { }

    public virtual void Back() { }

    public virtual void NewGame() { }

    public virtual void LoadSave() { }

    public virtual void OpenSettings() { }

    public virtual void Exit() { }

    public virtual void Play() { }

    public virtual void Dead() { }

    public virtual void Restart() { }

    public virtual void Confirm() { }

    public virtual void Save() { }
}