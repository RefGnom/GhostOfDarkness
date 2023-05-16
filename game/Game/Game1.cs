using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace game;

internal class Game1 : Game
{
    private readonly float maxWindowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

    private GameModel model;
    private GameView view;
    private GameController controller;

    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    public int WindowWidth => graphics.PreferredBackBufferWidth;
    public int WindowHeight => graphics.PreferredBackBufferHeight;
    public float Scale => WindowWidth / maxWindowWidth;

    private static Camera Camera => GameManager.Instance.Camera;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        LoadContent();
        model = new(new Vector2(WindowWidth / 2, WindowHeight / 2), 1920, 1080);
        view = new(model);
        controller = new(graphics, model);
        controller.SetSizeScreen(1280, 720);
        Debug.Initialize(WindowHeight);
        KeyboardController.GameWindow = Window;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        TexturesManager.Load(Content);
        FontsManager.Load(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (view.GameIsExit)
            Exit();

        KeyboardController.Update();
        MouseController.Update();
        Debug.Update(WindowHeight);

        if (model.Player.IsDead)
            view.Dead();

        if (KeyboardController.IsSingleKeyDown(Keys.Escape))
            view.Back();

        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        model.Update(deltaTime);
        controller.Update(deltaTime);

        Camera.Follow(model.Player.Position, WindowWidth, WindowHeight, deltaTime);
        Camera.ChangeScale(MouseController.ScrollValue());

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        view.Draw(spriteBatch, Scale);
        base.Draw(gameTime);
    }
}