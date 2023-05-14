using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game;

internal abstract class GameState : IState, IDrawable
{
    protected readonly IStateSwitcher switcher;
    protected GameState previousState;
    protected List<Button> buttons;
    public bool IsConfirmed { get; protected set; }
    public bool GameIsExit { get; protected set; }
    public bool Saved { get; protected set; }

    public GameState(IStateSwitcher stateSwitcher)
    {
        switcher = stateSwitcher;
    }

    public abstract void Start(IState previousState);

    public abstract void Stop();

    public abstract void Back();

    public abstract void NewGame();

    public abstract void LoadSave();

    public abstract void OpenSettings();

    public abstract void Exit();

    public abstract void Play();

    public abstract void Restart();

    public abstract void Confirm();

    public abstract void Save();

    public abstract void Update(float deltaTime);

    public abstract void Draw(SpriteBatch spriteBatch);

    protected void RegisterButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            GameManager.Instance.Drawer.RegisterUI(buttons[i]);
        }
    }

    protected void UnregisterButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            GameManager.Instance.Drawer.UnregisterUI(buttons[i]);
        }
    }

    protected void ClickedButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Clicked();
        }
    }
}