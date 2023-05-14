using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class SettingsState : GameState
{
    public SettingsState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Back()
    {
        switcher.SwitchState(previousState);
    }

    public override void Confirm()
    {
    }

    public override void LoadSave()
    {
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
    }

    public override void NewGame()
    {
    }

    public override void OpenSettings()
    {
    }

    public override void Play()
    {
    }

    public override void Restart()
    {
    }

    public override void Save()
    {
        Saved = true;
        switcher.SwitchState(previousState);
    }

    public override void Start(IState previousState)
    {
        this.previousState = (GameState)previousState;
    }

    public override void Stop()
    {
    }
}