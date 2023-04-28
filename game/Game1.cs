using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace game
{
    internal class Game1 : Game
    {
        private Dictionary<Keys, Action<float>> actions;

        private GameModel model;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Texture2D playerTexture;

        public int WindowWidth => graphics.PreferredBackBufferWidth;
        public int WindowHeight => graphics.PreferredBackBufferHeight;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            actions = new();
            model = new(new Vector2(WindowWidth / 2, WindowHeight / 2));
            // Убрать, после добавления генерации локации
            #region tempInitLocation
            model.Location.Width = WindowWidth;
            model.Location.Height = WindowHeight;
            #endregion

            RegisterAllKeys();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            playerTexture = Content.Load<Texture2D>("Player");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var keyboardState = Keyboard.GetState();
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var pressedKeys = keyboardState.GetPressedKeys();
            HandleKeys(pressedKeys, deltaTime);

            model.CheckPlayerOnOutBounds(playerTexture.Width, playerTexture.Height);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(
                playerTexture,
                model.Player.Position,
                null,
                Color.White,
                0f,
                new Vector2(playerTexture.Width / 2, playerTexture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f);  
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
            actions[Keys.W] = deltaTime => model.Player.Move(Directions.Up, deltaTime);
            actions[Keys.S] = deltaTime => model.Player.Move(Directions.Down, deltaTime);
            actions[Keys.A] = deltaTime => model.Player.Move(Directions.Left, deltaTime);
            actions[Keys.D] = deltaTime => model.Player.Move(Directions.Right, deltaTime);
        }
    }
}