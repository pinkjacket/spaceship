using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace spaceship
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D ship_Sprite;
        Texture2D asteroid_Sprite;
        Texture2D space_Sprite;

        SpriteFont gameFont;
        SpriteFont timerFont;

        Ship player = new Ship();

        Asteroid testAsteroid = new Asteroid(300);

        Controller gameController = new Controller();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 540;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ship_Sprite = Content.Load<Texture2D>("ship");
            asteroid_Sprite = Content.Load<Texture2D>("asteroid");
            space_Sprite = Content.Load<Texture2D>("space");

            gameFont = Content.Load<SpriteFont>("spaceFont");
            timerFont = Content.Load<SpriteFont>("timerFont");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.shipUpdate(gameTime, gameController);
            gameController.conUpdate(gameTime);

            for (int i = 0; i < gameController.asteroids.Count; i++)
            {
                gameController.asteroids[i].asteroidUpdate(gameTime);

                if (gameController.asteroids[i].position.X < (0 - gameController.asteroids[i].radius))
                {
                    gameController.asteroids[i].offscreen = true;
                }

                int sum = gameController.asteroids[i].radius + 30;
                if (Vector2.Distance(gameController.asteroids[i].position, player.position) < sum)
                {
                    gameController.inGame = false;
                    player.position = Ship.startingPosition;
                    i = gameController.asteroids.Count;
                    gameController.asteroids.Clear();
                }
            }
            //gets rid of asteroids that pass offscreen
            gameController.asteroids.RemoveAll(a => a.offscreen == true);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(space_Sprite, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(ship_Sprite, new Vector2(player.position.X - 34, player.position.Y - 50), Color.White);

            if (gameController.inGame == false)
            {
                string menuMessage = "Press Enter to start!";
                Vector2 textSize = gameFont.MeasureString(menuMessage);
                spriteBatch.DrawString(gameFont, menuMessage, new Vector2(480 - textSize.X / 2, 120), Color.White);
            }

            for (int i = 0; i < gameController.asteroids.Count; i++)
            {
                Vector2 tempPos = gameController.asteroids[i].position;
                int tempRadius = gameController.asteroids[i].radius;
                spriteBatch.Draw(asteroid_Sprite, new Vector2(tempPos.X - tempRadius, tempPos.Y - tempRadius), Color.White);
            }

            spriteBatch.DrawString(timerFont, "time: " + Math.Floor(gameController.totalTime).ToString(), new Vector2(3, 3), Color.White);

                spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
