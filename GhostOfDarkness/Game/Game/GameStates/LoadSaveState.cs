using System.Collections.Generic;
using Core.Saves;
using game;
using Game.Controllers;
using Game.Interfaces;
using Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Game.GameStates;

internal class LoadSaveState : GameState
{
    private readonly ISaveHandler saveHandler;
    private readonly List<IComponent> SaveComponents;

    public LoadSaveState(
        IGameStateSwitcher stateSwitcher,
        ISaveHandler saveHandler
    ) : base(stateSwitcher)
    {
        this.saveHandler = saveHandler;

        Drawables.Add(new Sprite(Textures.SavesWindow, new Vector2(40, 70), Layers.UIBackground));
        Drawables.Add(new Sprite(Textures.Background, Vector2.Zero, Layers.Background));

        var load = new Button(Textures.ButtonBackground, new Vector2(40, 960), "Load");
        load.OnClicked += Play;
        Components.Add(load);

        var createNew = new Button(Textures.ButtonBackground, new Vector2(538, 960), "Create New Game");
        createNew.OnClicked += NewGame;
        Components.Add(createNew);

        var back = new Button(Textures.ButtonBackground, new Vector2(1432, 960), "Back");
        back.OnClicked += Back;
        Components.Add(back);

        SaveComponents = new List<IComponent>();
    }

    public override void Back()
    {
        Switcher.SwitchState<MainMenuState>();
    }

    public override void NewGame()
    {
        Switcher.SwitchState<CreateGameState>();
    }

    public override void Play()
    {
        Switcher.SwitchState<PlayState>();
    }

    public override void Start(GameState previousState)
    {
        var saveInfos = saveHandler.Select();
        var deltaY = 0;
        foreach (var saveInfo in saveInfos)
        {
            var button = new SaveButton(Textures.Save,
                new Vector2(60, 90 + deltaY),
                saveInfo.Name,
                saveInfo.Difficulty.ToString(),
                saveInfo.PlayTime.ToString()
            );
            SaveComponents.Add(button);
            deltaY += 120;
        }
    }

    public override void Stop()
    {
    }

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        base.Draw(spriteBatch, scale);

        foreach (var saveComponent in SaveComponents)
        {
            saveComponent.Draw(spriteBatch, scale);
        }
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        foreach (var saveComponent in SaveComponents)
        {
            saveComponent.Update(deltaTime);
        }
    }
}