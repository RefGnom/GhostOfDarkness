using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace game;

internal class GameStatesController : IStateSwitcher, IDrawable
{
    private GameState currentState;
    private readonly List<GameState> states;

    public GameStatesController()
    {
        states = new List<GameState>()
        {
            new MainMenuState(this),
            new LoadSaveState(this),
            new ConfirmationState(this),
            new CreateGameState(this),
            new PauseState(this),
            new PlayerDeadState(this),
            new PlayState(this),
            new SettingsState(this)
        };
        currentState = states[0];
    }

    public void Back() => currentState.Back();

    public void NewGame()=> currentState.NewGame();

    public void LoadSave()=> currentState.LoadSave();

    public void OpenSettings()=> currentState.OpenSettings();

    public void Exit()=> currentState.Exit();

    public void Play()=> currentState.Play();

    public void Restart()=> currentState.Restart();

    public void Confirm()=> currentState.Confirm();

    public void Save()=> currentState.Save();

    public void SwitchState<T>() where T : IState
    {
        var state = states.FirstOrDefault(s => s is T);
        currentState.Stop();
        state.Start(currentState);
        currentState = state;
    }

    void IDrawable.Draw(SpriteBatch spriteBatch)
    {
        currentState.Draw(spriteBatch);
    }
}