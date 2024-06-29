using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Core.Saves;
using Game.Game.GameStates;
using Game.Interfaces;

namespace game;

internal class GameStatesController : IGameStateSwitcher, IDrawable, IUpdateable
{
    private GameState currentState;
    private readonly List<GameState> states;

    public bool GameIsExit => currentState.GameIsExit;
    public bool IsPlay => currentState is PlayState;

    public GameStatesController()
    {
        states = new List<GameState>()
        {
            new MainMenuState(this),
            new LoadSaveState(this, new SaveHandler()),
            new ConfirmationState(this),
            new CreateGameState(this, new SaveHandler(), new SaveProvider()),
            new PauseState(this),
            new PlayerDeadState(this),
            new PlayState(this),
            new SettingsState(this)
        };
        currentState = states[0];
        currentState.Start(currentState);
    }

    public void Back() => currentState.Back();

    public void NewGame() => currentState.NewGame();

    public void LoadSave() => currentState.LoadSave();

    public void OpenSettings() => currentState.OpenSettings();

    public void Exit() => currentState.Exit();

    public void Play() => currentState.Play();

    public void Dead() => currentState.Dead();

    public void Confirm() => currentState.Confirm();

    public void Save() => currentState.Save();

    public void SwitchState<T>() where T : IState
    {
        var state = states.First(s => s is T);
        SwitchState(state);
    }

    public void SwitchState(IState state)
    {
        currentState.Stop();
        state.Start(currentState);
        currentState = (GameState)state;
    }

    public virtual void StartGame() { }

    public virtual void Draw(SpriteBatch spriteBatch, float scale)
    {
        currentState.Draw(spriteBatch, scale);
    }

    public virtual void Update(float deltaTime)
    {
        currentState.Update(deltaTime);
    }
}