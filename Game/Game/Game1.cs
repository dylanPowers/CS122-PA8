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

        //Begin Declaration Code
        Block block1;
        Player player1;
        Player player2;

        int playerMoveSpeed;

        const int UP = 1;
        const int DOWN = 2;
        const int LEFT = 3;
        const int RIGHT = 4;
        //End Declaration Code

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
            playerMoveSpeed = 10;
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
            Vector2 vectorBlock = new Vector2(100, GraphicsDevice.Viewport.Height - textureBlock.Height);
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

            //Begin Update Code

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (player1.position.Y + player1.height < GraphicsDevice.Viewport.Height && !player1.onTopOfBlock) //check if player is in mid air
            {
                player1.airbourne = true;
            }
            else
            {
                player1.airbourne = false;
                player1.velocity = 0;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                if (!player1.airbourne)
                {
                    player1.airbourne = true;
                    player1.onTopOfBlock = false;
                    player1.jump();
                }
            }

            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                if (player1.position.X - playerMoveSpeed > 0)
                {
                    if (player1.willCollide(block1, LEFT, playerMoveSpeed)) //if collides with block, players position is on edge of block
                    {
                        player1.position.X = block1.position.X + block1.width;
                    }
                    else //normal movement
                    {
                        player1.position.X -= 10;
                        if (player1.onTopOfBlock)
                        {
                            if (player1.position.X + player1.width < block1.position.X)
                                player1.onTopOfBlock = false;
                        }
                    }

                }
                else
                {
                    player1.position.X = 0; //player's pos is on left edge of screen
                }
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                if (player1.position.X + player1.width + playerMoveSpeed < GraphicsDevice.Viewport.Width)
                {
                    if (player1.willCollide(block1, RIGHT, playerMoveSpeed)) //if collides with block, players position is on edge of block
                    {
                        player1.position.X = block1.position.X - player1.width;
                    }
                    else //normal movement
                    {
                        player1.position.X += 10;
                        if (player1.onTopOfBlock)
                        {
                            if (player1.position.X > block1.position.X + block1.width)
                                player1.onTopOfBlock = false;
                        }
                    }

                }
                else
                {
                    player1.position.X = GraphicsDevice.Viewport.Width - player1.width; //player's pos is on left edge of screen
                }
                
            }

            if (player1.airbourne)
            {
                player1.velocity += player1.acceleration;
                if (player1.willCollide(block1, DOWN, player1.velocity))
                {
                    player1.position.Y = block1.position.Y - player1.height;
                    player1.velocity = 0;
                    player1.onTopOfBlock = true;
                }
                else if (player1.position.Y + player1.velocity + player1.height > GraphicsDevice.Viewport.Height)
                {
                    player1.onTopOfBlock = false;
                    player1.position.Y = GraphicsDevice.Viewport.Height - player1.height;
                }
                else
                {
                    player1.position.Y += player1.velocity;
                }
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
