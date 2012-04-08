using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        Block block1;
        Player player1;
        Player player2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Begin Initialization Code
            block1 = new Block();
            player1 = new Player();
            player2 = new Player();
            //End Initialization Code

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

            //Begin Loading Code
            Texture2D textureBlock = Content.Load<Texture2D>("brick");
            Vector2 vectorBlock = new Vector2(50, 50);
            block1.Initialize(textureBlock, vectorBlock);

            Texture2D texturePlayer = Content.Load<Texture2D>("mario");
            Vector2 playerVector = new Vector2(100, 100);
            player1.Initialize(texturePlayer, playerVector);
            player2.Initialize(texturePlayer, playerVector);
            //End Loading Code
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            //Begin Update Code
            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                player1.position.X -= 10;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                player1.position.X += 10;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                player1.position.Y -= 10;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                player1.position.Y += 10;
            }
            if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                player2.position.X -= 10;
            }
            if (currentKeyboardState.IsKeyDown(Keys.D))
            {
                player2.position.X += 10;
            }
            if (currentKeyboardState.IsKeyDown(Keys.W))
            {
                player2.position.Y -= 10;
            }
            if (currentKeyboardState.IsKeyDown(Keys.S))
            {
                player2.position.Y += 10;
            }
            //End Update Code

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            // Begin Drawing Code
            block1.Draw(spriteBatch);
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            // End Drawing Code

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
