using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game;

internal abstract class GameState : IState, IDrawable, IUpdateable
{
    protected readonly IGameStateSwitcher switcher;
    protected GameState previousState;
    protected List<IComponent> components;
    protected List<IDrawable> drawables;
    protected List<IUpdateable> updateables;

    public bool IsConfirmed { get; protected set; }
    public bool GameIsExit { get; protected set; }
    public bool Saved { get; protected set; }

    public GameState(IGameStateSwitcher stateSwitcher)
    {
        switcher = stateSwitcher;
        components = new();
        drawables = new();
        updateables = new();
    }

    public void Start(IState previousState)
    {
        var state = (GameState)previousState;
        this.previousState = state;
        Start(state);
    }

    public virtual void Update(float deltaTime)
    {
        for (int i = 0; i < components.Count; i++)
        {
            components[i].Update(deltaTime);
        }
        for (int i = 0; i < updateables.Count; i++)
        {
            updateables[i].Update(deltaTime);
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch, float scale)
    {
        for (int i = 0; i < components.Count; i++)
        {
            components[i].Draw(spriteBatch, scale);
        }
        for (int i = 0; i < drawables.Count; i++)
        {
            drawables[i].Draw(spriteBatch, scale);
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