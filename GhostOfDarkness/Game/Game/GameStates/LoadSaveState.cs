using System;
using System.Collections.Generic;
using Core.Saves;
using game;
using Game.Controllers.Buttons;
using Game.Interfaces;
using Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Game.GameStates;

internal class LoadSaveState : GameState
{
    private SaveInfo selectedInfo;
    private readonly Button loadButton;

    private readonly List<IComponent> saveComponents;
    private readonly ISaveHandler saveHandler;
    private readonly IButtonFactory buttonFactory;

    public LoadSaveState(
        IGameStateSwitcher stateSwitcher,
        ISaveHandler saveHandler,
        IButtonFactory buttonFactory
    ) : base(stateSwitcher)
    {
        this.saveHandler = saveHandler;
        this.buttonFactory = buttonFactory;

        Drawables.Add(new Sprite(Textures.SavesWindow, new Vector2(40, 70), Layers.UIBackground));
        Drawables.Add(new Sprite(Textures.Background, Vector2.Zero, Layers.Background));

        loadButton = buttonFactory.CreateButtonWithText(Textures.ButtonBackground, new Vector2(40, 960), "Load");
        loadButton.OnClicked += Play;
        loadButton.Inactive = true;
        Components.Add(loadButton);

        var createNew = buttonFactory.CreateButtonWithText(Textures.ButtonBackground, new Vector2(538, 960), "Create New Game");
        createNew.OnClicked += NewGame;
        Components.Add(createNew);

        var back = buttonFactory.CreateButtonWithText(Textures.ButtonBackground, new Vector2(1432, 960), "Back");
        back.OnClicked += Back;
        Components.Add(back);

        saveComponents = new List<IComponent>();
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
        Console.WriteLine("Start save '{0}'", selectedInfo.Name);
        Switcher.SwitchState<PlayState>();
    }

    public override void Start(GameState previousState)
    {
        // saveComponents.Clear();
        // var saveInfos = saveHandler.Select();
        // var deltaY = 0;
        // foreach (var saveInfo in saveInfos)
        // {
        //     var button = new SaveButton(Textures.Save,
        //         new Vector2(63, 100 + deltaY),
        //         saveInfo.Name,
        //         saveInfo.Difficulty.ToString(),
        //         saveInfo.PlayTime.ToString()
        //     );
        //     button.OnClicked += () => ChoiceSave(saveInfo);
        //     saveComponents.Add(button);
        //     deltaY += 120;
        // }
    }

    public override void Stop()
    {
    }

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        base.Draw(spriteBatch, scale);

        foreach (var saveComponent in saveComponents)
        {
            saveComponent.Draw(spriteBatch, scale);
        }
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        foreach (var saveComponent in saveComponents)
        {
            saveComponent.Update(deltaTime);
        }
    }

    private void ChoiceSave(SaveInfo saveInfo)
    {
        selectedInfo = saveInfo;
        loadButton.Inactive = false;
    }
}