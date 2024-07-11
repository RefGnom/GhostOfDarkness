using System.Collections.Generic;
using System.Linq;
using Game.Configuration;
using Game.Graphics;
using Game.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Game.GameStates;

internal class GameStatesController : IGameStateSwitcher, IDrawable, IUpdateable
{
    private GameState currentState;
    private readonly List<GameState> states;

    public bool GameIsExit => currentState.GameIsExit;
    public bool IsPlay => currentState is PlayState;

    public GameStatesController()
    {
        states = DiConfiguration.ServiceProvider.GetServices<GameState>().ToList();
        foreach (var state in states)
        {
            state.Switcher = this;
        }

        currentState = states.Single(x => x is MainMenuState);
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

    public virtual void StartGame()
    {
    }

    public virtual void Draw(ISpriteBatch spriteBatch, float scale)
    {
        currentState.Draw(spriteBatch, scale);
    }

    public virtual void Update(float deltaTime)
    {
        currentState.Update(deltaTime);
    }
}