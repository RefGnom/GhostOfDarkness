using Core.Saves;
using game;
using Game.Interfaces;
using Game.View;
using Microsoft.Xna.Framework;

namespace Game.Game.GameStates;

internal class LoadSaveState : GameState
{
    private readonly ISaveHandler saveHandler;

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
            var text = new Text(
                new Rectangle(new Point(40, 70 + deltaY), new Point(100, 16)),
                $"{saveInfo.Name} time: {saveInfo.PlayTime}",
                Align.Left,
                0,
                Fonts.Common16
            );
            Drawables.Add(text);
            deltaY += 20;
        }
    }

    public override void Stop()
    {
    }
}