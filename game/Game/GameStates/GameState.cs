using Microsoft.Xna.Framework.Graphics;

namespace game;

internal abstract class GameState : IState, IDrawable
{
    protected readonly IStateSwitcher switcher;
    protected GameState previousState;
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

    public abstract void Draw(SpriteBatch spriteBatch);
}