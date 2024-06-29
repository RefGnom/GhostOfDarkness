using System.Collections.Generic;
using game;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Game.GameStates;

internal abstract class GameState : IState, IDrawable, IUpdateable
{
    protected readonly IGameStateSwitcher Switcher;
    protected GameState PreviousState;
    protected readonly List<IComponent> Components;
    protected readonly List<IDrawable> Drawables;
    protected List<IUpdateable> updatables;

    public bool IsConfirmed { get; protected set; }
    public bool GameIsExit { get; protected set; }
    public bool Saved { get; protected set; }

    public GameState(IGameStateSwitcher stateSwitcher)
    {
        Switcher = stateSwitcher;
        Components = new();
        Drawables = new();
        updatables = new();
    }

    public void Start(IState previousState)
    {
        var state = (GameState)previousState;
        this.PreviousState = state;
        Start(state);
    }

    public virtual void Update(float deltaTime)
    {
        for (var i = 0; i < Components.Count; i++)
        {
            Components[i].Update(deltaTime);
        }
        for (var i = 0; i < updatables.Count; i++)
        {
            updatables[i].Update(deltaTime);
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch, float scale)
    {
        for (var i = 0; i < Components.Count; i++)
        {
            Components[i].Draw(spriteBatch, scale);
        }
        for (var i = 0; i < Drawables.Count; i++)
        {
            Drawables[i].Draw(spriteBatch, scale);
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