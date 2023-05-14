using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class PlayState : GameState
{
    public PlayState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Back()
    {
        switcher.SwitchState<PauseState>();
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
    }

    public override void Start(IState previousState)
    {
    }

    public override void Stop()
    {
    }

    public override void Update(float deltaTime)
    {
    }
}