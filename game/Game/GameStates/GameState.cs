using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game;

internal abstract class GameState : IState, IDrawable, IComponent
{
    protected readonly IGameStateSwitcher switcher;
    protected GameState previousState;
    protected List<Button> buttons;
    public bool IsConfirmed { get; protected set; }
    public bool GameIsExit { get; protected set; }
    public bool Saved { get; protected set; }

    public GameState(IGameStateSwitcher stateSwitcher)
    {
        switcher = stateSwitcher;
    }

    public void Start(IState previousState)
    {
        var state = (GameState)previousState;
        this.previousState = state;
        Start(state);
    }

    public virtual void Update(float deltaTime) { }

    public virtual void Draw(SpriteBatch spriteBatch, float scale)
    {
        if (buttons is not null)
        {
            for (int i = 0; i < buttons.Count; i++)
                buttons[i].Draw(spriteBatch, scale);
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

    protected void ClickedButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Clicked();
        }
    }
}