using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class MainMenuState : GameState
{
    public MainMenuState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Back()
    {
    }

    public override void Confirm()
    {
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        GameIsExit = true;
    }

    public override void NewGame()
    {
        switcher.SwitchState<CreateGameState>();
    }

    public override void LoadSave()
    {
        switcher.SwitchState<LoadSaveState>();
    }

    public override void OpenSettings()
    {
        switcher.SwitchState<SettingsState>();
    }

    public override void Play()
    {
    }

    public override void Restart()
    {
    }

    public override void Save()
    {
    }

    public override void Start(IState previousState)
    {
    }

    public override void Stop()
    {
    }
}