using System.Collections.Generic;

namespace game;

internal abstract class GameState : IState
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

    public abstract void Start(GameState previousState);

    public abstract void Draw();

    public abstract void Erase();

    public abstract void Stop();

    public abstract void Back();

    public abstract void NewGame();

    public abstract void LoadSave();

    public abstract void OpenSettings();

    public abstract void Exit();

    public abstract void Play();

    public abstract void Dead();

    public abstract void Restart();

    public abstract void Confirm();

    public abstract void Save();

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