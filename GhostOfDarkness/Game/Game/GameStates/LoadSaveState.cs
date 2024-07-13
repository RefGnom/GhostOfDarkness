﻿using System;
using System.Collections.Generic;
using Core.Saves;
using Game.ContentLoaders;
using Game.Controllers;
using Game.Controllers.Buttons;
using Game.Controllers.InputServices;
using Game.Graphics;
using Game.Interfaces;
using Game.Service;
using Game.View;
using Game.View.UI;
using Microsoft.Xna.Framework;

namespace Game.Game.GameStates;

internal class LoadSaveState : GameState
{
    private SaveInfo selectedSaveInfo;
    private readonly Button loadButton;
    private readonly Button deleteButton;

    private readonly List<IComponent> saveComponents;
    private readonly ISaveHandler saveHandler;
    private readonly IButtonFactory buttonFactory;
    private readonly IRadioButtonManager radioButtonManager;

    public LoadSaveState(
        ISaveHandler saveHandler,
        IButtonFactory buttonFactory,
        IRadioButtonManager radioButtonManager
    )
    {
        this.saveHandler = saveHandler;
        this.buttonFactory = buttonFactory;
        this.radioButtonManager = radioButtonManager;

        var savesBackgroundPosition = new Vector2(40, 70);
        var savesBackgroundTexture = Textures.SavesWindow;
        Drawables.Add(new Sprite(savesBackgroundTexture, savesBackgroundPosition, Layers.UiBackground));
        Drawables.Add(new Sprite(Textures.Background, Vector2.Zero, Layers.Background));

        loadButton = buttonFactory.CreateButtonWithText(Textures.ButtonBackground, new Vector2(40, 860), "Load");
        loadButton.OnClicked += Play;
        Components.Add(loadButton);

        deleteButton = buttonFactory.CreateButtonWithText(Textures.ButtonBackground, new Vector2(40, 960), "Delete");
        deleteButton.OnClicked += DeleteSave;
        Components.Add(deleteButton);

        var createNew = buttonFactory.CreateButtonWithText(Textures.ButtonBackground, new Vector2(538, 960), "Create New Game");
        createNew.OnClicked += NewGame;
        Components.Add(createNew);

        var back = buttonFactory.CreateButtonWithText(Textures.ButtonBackground, new Vector2(1432, 960), "Back");
        back.OnClicked += Back;
        Components.Add(back);

        saveComponents = [];

        var scrollBounds = new ScrollBounds(
            Input.MouseService,
            new Rectangle(savesBackgroundPosition.ToPoint(), savesBackgroundTexture.Bounds.Size)
        );

        var savesScrollBar = new ScrollBar(
            new Vector2(971, 130),
            Textures.ScrollBar,
            Textures.ScrollBox,
            new Point(1, 2),
            scrollBounds
        );

        scrollBounds.OnScroll += scrollValue =>
        {
            const int percentsPerShift = 10;
            var shiftValue = scrollValue * percentsPerShift / 100f;
            savesScrollBar.ShiftBox(shiftValue);
        };
        Components.Add(savesScrollBar);
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
        Console.WriteLine("Start save '{0}'", selectedSaveInfo.Name);
        Switcher.SwitchState<PlayState>();
    }

    public override void Start(GameState previousState)
    {
        loadButton.Inactive = true;
        deleteButton.Inactive = true;
        saveComponents.Clear();

        var saveInfos = saveHandler.Select();
        var deltaY = 0;
        var radioButtons = new RadioButton[saveInfos.Length];
        for (var i = 0; i < saveInfos.Length; i++)
        {
            var saveInfo = saveInfos[i];
            var button = buttonFactory.CreateSaveButton(Textures.Save, Textures.Save, new Vector2(63, 100 + deltaY), saveInfo);
            button.OnEnabled += () => ChoiceSave(saveInfo);
            radioButtons[i] = button;
            saveComponents.Add(button);
            deltaY += 120;
        }

        radioButtonManager.LinkRadioButtons(radioButtons);
    }

    public override void Stop()
    {
    }

    public override void Draw(ISpriteBatch spriteBatch, float scale)
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
        selectedSaveInfo = saveInfo;
        loadButton.Inactive = false;
        deleteButton.Inactive = false;
    }

    private void DeleteSave()
    {
        saveHandler.Delete(selectedSaveInfo.Name);
        Start(null);
    }
}