using Game.Interfaces;
using Game.Managers;

namespace Game.Game.GameStates;

internal class PlayState : GameState
{
    public PlayState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Back()
    {
        Switcher.SwitchState<PauseState>();
    }

    public override void Dead()
    {
        Switcher.SwitchState<PlayerDeadState>();
    }

    public override void Start(GameState previousState)
    {
        SongsManager.Resume();
    }

    public override void Stop()
    {
        SongsManager.Pause();
    }
}