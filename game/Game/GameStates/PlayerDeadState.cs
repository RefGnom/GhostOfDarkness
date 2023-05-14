using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class PlayerDeadState : GameState
{
    public PlayerDeadState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Back()
    {
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
        switcher.SwitchState<MainMenuState>();
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
        switcher.SwitchState<PlayState>();
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