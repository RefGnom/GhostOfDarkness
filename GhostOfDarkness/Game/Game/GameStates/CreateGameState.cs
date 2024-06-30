using Core.Extensions;
using Core.Saves;
using game;
using Game.Controllers;
using Game.Controllers.Buttons;
using Game.Interfaces;
using Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Game.GameStates;

internal class CreateGameState : GameState
{
    private readonly ISaveHandler saveHandler;
    private readonly ISaveProvider saveProvider;
    private readonly TextInput textInput;

    public CreateGameState(
        IGameStateSwitcher stateSwitcher,
        ISaveHandler saveHandler,
        ISaveProvider saveProvider,
        IButtonFactory buttonFactory
    ) : base(stateSwitcher)
    {
        this.saveHandler = saveHandler;
        this.saveProvider = saveProvider;

        Drawables.Add(new Sprite(Textures.Background, Vector2.Zero, Layers.Background));

        var back = buttonFactory.CreateButtonWithText(Textures.ButtonBackground, new Vector2(1432, 960), "Back");
        back.OnClicked += Back;
        Components.Add(back);

        var create = buttonFactory.CreateButtonWithText(Textures.ButtonBackground, new Vector2(380, 600), "Create");
        create.OnClicked += CreateGame;
        Components.Add(create);

        textInput = new TextInput(Textures.FieldForText, new Vector2(380, 466));
    }

    public override void Back()
    {
        Switcher.SwitchState(PreviousState);
    }

    public override void Play()
    {
        Switcher.SwitchState<PlayState>();
    }

    private void CreateGame()
    {
        if (textInput.Text.IsNullOrEmpty())
        {
            // todo: Нельзя создавать сохранение с пустым названием
            return;
        }

        var save = saveProvider.CreateDefaultSave(textInput.Text);
        saveHandler.Create(save, textInput.Text);

        Switcher.StartGame();
        Play();
    }

    public override void Start(GameState previousState)
    {
        textInput.Enable();
    }

    public override void Stop()
    {
        textInput.Disable();
        textInput.Clear();
    }

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        textInput.Draw(spriteBatch, scale);
        base.Draw(spriteBatch, scale);
    }
}