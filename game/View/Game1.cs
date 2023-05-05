using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using game.Managers;
using game.Interfaces;
using game.Enums;
using game.Model;

namespace game.View
{
    internal class Game1 : Game, IPauseHandler
    {
        private Dictionary<Keys, Action<float>> actions;

        private GameModel model;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Sprite playerSprite;
        private Animator meleeEnemyAnimator;
        private Texture2D bulletTexture;
        private Texture2D pauseMenu;

        public int WindowWidth => graphics.PreferredBackBufferWidth;
        public int WindowHeight => graphics.PreferredBackBufferHeight;

        private PauseManager PauseManager => GameManager.Instance.PauseManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            actions = new();
            model = new(new Vector2(WindowWidth / 2, WindowHeight / 2), WindowWidth, WindowHeight);

            GameManager.Instance.PauseManager.RegisterHandler(this);
            RegisterAllKeys();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bulletTexture = Content.Load<Texture2D>("Bullet");
            pauseMenu = Content.Load<Texture2D>("Pause Menu");
            playerSprite = new(Content.Load<Texture2D>("Player"));
            meleeEnemyAnimator = AnimatorsCreator.GetAnimator("Melee Enemy", Content.Load<Texture2D>("Melee Enemy"));
            meleeEnemyAnimator.SetAnimation(1);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            if (KeyboardManager.IsSingleDown(Keys.Escape))
                PauseManager.SetPaused(!PauseManager.IsPaused);

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            var mousePosition = mouseState.Position.ToVector2();
            var direction = mousePosition - model.Player.Position;
            direction.Normalize();

            var pressedKeys = keyboardState.GetPressedKeys();
            HandleKeys(pressedKeys, deltaTime);

            if (!PauseManager.IsPaused)
            {
                if (MouseManager.LeftButtomClicked())
                    model.Player.Shoot(direction);
                playerSprite.UpdateDirection(direction);
            }
            model.Update(deltaTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (PauseManager.IsPaused)
                return;

            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            playerSprite.Draw(spriteBatch, model.Player.Position);
            foreach (var bullet in model.Player.Bullets)
            {
                spriteBatch.Draw(bulletTexture, bullet.Position, Color.White);
            }
            foreach (var enemy in model.Location.Enemies)
            {
                var flip = SpriteEffects.None;
                if (enemy.Direction.X < 0)
                    flip = SpriteEffects.FlipHorizontally;
                meleeEnemyAnimator.Draw(enemy.Position, spriteBatch, flip);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void HandleKeys(Keys[] keys, float deltaTime)
        {
            foreach (var key in keys)
            {
                if (actions.ContainsKey(key))
                    actions[key](deltaTime);
            }
        }

        public void RegisterAllKeys()
        {
            actions[Keys.W] = deltaTime => model.Player.EnableDirections[Directions.Up] = true;
            actions[Keys.S] = deltaTime => model.Player.EnableDirections[Directions.Down] = true;
            actions[Keys.A] = deltaTime => model.Player.EnableDirections[Directions.Left] = true;
            actions[Keys.D] = deltaTime => model.Player.EnableDirections[Directions.Right] = true;
        }

        public void SetPaused(bool isPaused)
        {
            if (isPaused)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(pauseMenu, Vector2.Zero, null, Color.White, 0, Vector2.One, 1, SpriteEffects.None, 0);
                spriteBatch.End();
            }
        }
    }
}