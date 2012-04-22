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
        List<Block> blocks;
        Player player1;
        Player player2;

        int playerMoveSpeed;

        const int UP = 1;
        const int DOWN = 2;
        const int LEFT = 3;
        const int RIGHT = 4;

        SoundEffect beep;

        Texture2D player1TextureLeft;
        Texture2D player1TextureRight;
        Texture2D player2TextureLeft;
        Texture2D player2TextureRight;
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
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            //Begin Initialization Code
            blocks = new List<Block>();
            for (int i = 0; i < 14; i++ ) //add needed number of blocks to block list
                blocks.Add(new Block());
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
            blocks[0].Initialize(textureBlock, new Vector2(0, GraphicsDevice.Viewport.Height - textureBlock.Height));
            blocks[1].Initialize(textureBlock, new Vector2(150, GraphicsDevice.Viewport.Height - textureBlock.Height));
            blocks[2].Initialize(textureBlock, new Vector2(150, GraphicsDevice.Viewport.Height - (textureBlock.Height * 2)));
            blocks[3].Initialize(textureBlock, new Vector2(150, GraphicsDevice.Viewport.Height - (textureBlock.Height * 3)));
            blocks[4].Initialize(textureBlock, new Vector2(150, GraphicsDevice.Viewport.Height - (textureBlock.Height * 4)));
            blocks[5].Initialize(textureBlock, new Vector2(150, GraphicsDevice.Viewport.Height - (textureBlock.Height * 5)));
            blocks[6].Initialize(textureBlock, new Vector2(150, GraphicsDevice.Viewport.Height - (textureBlock.Height * 6)));
            blocks[7].Initialize(textureBlock, new Vector2(150, GraphicsDevice.Viewport.Height - (textureBlock.Height * 7)));
            blocks[8].Initialize(textureBlock, new Vector2(150, GraphicsDevice.Viewport.Height - (textureBlock.Height * 8)));
            blocks[9].Initialize(textureBlock, new Vector2(150, GraphicsDevice.Viewport.Height - (textureBlock.Height * 9)));
            blocks[10].Initialize(textureBlock, new Vector2(150, GraphicsDevice.Viewport.Height - (textureBlock.Height * 10)));
            blocks[11].Initialize(textureBlock, new Vector2(100, GraphicsDevice.Viewport.Height - (textureBlock.Height * 4)));
            blocks[12].Initialize(textureBlock, new Vector2(0, GraphicsDevice.Viewport.Height - (textureBlock.Height * 7)));
            blocks[13].Initialize(textureBlock, new Vector2(100, GraphicsDevice.Viewport.Height - (textureBlock.Height * 10)));

            player1TextureLeft = Content.Load<Texture2D>("player_purple_left");
            player1TextureRight = Content.Load<Texture2D>("player_purple_right");
            player2TextureLeft = Content.Load<Texture2D>("player_yellow_left");
            player2TextureRight = Content.Load<Texture2D>("player_yellow_right");
            Vector2 playerVector = new Vector2(100, GraphicsDevice.Viewport.Height - player1TextureLeft.Height);
            player1.Initialize(player1TextureLeft, playerVector);
            //player2.Initialize(playerTexture2, playerVector);

            beep = Content.Load<SoundEffect>("beep");
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

            // Resets the character when the escape key is pressed
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
                this.player1.position = new Vector2(100, GraphicsDevice.Viewport.Height - player1TextureLeft.Height);

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
                    beep.Play();
                    player1.jump();
                }
            }

            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                player1.texture = player1TextureLeft;
                bool isColliding = false;

                if (player1.position.X - playerMoveSpeed > 0)
                {
                    for (int i = 0; i < blocks.Count; i++ )
                    {
                        if (player1.willCollide(blocks[i], LEFT, playerMoveSpeed)) //if collides with block, player's position is on edge of block
                        {
                            player1.position.X = blocks[i].position.X + blocks[i].width;
                            isColliding = true;
                        }
                    }
                    if(!isColliding)//normal movement
                    {
                        player1.position.X -= 10;
                        if (player1.onTopOfBlock)
                        {
                            if (player1.position.X + player1.width <= blocks[player1.whichBlock].position.X)
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
                player1.texture = player1TextureRight;
                bool isColliding = false;

                if (player1.position.X + player1.width + playerMoveSpeed < GraphicsDevice.Viewport.Width)
                {
                    for (int i = 0; i < blocks.Count; i++)
                    {
                        if (player1.willCollide(blocks[i], RIGHT, playerMoveSpeed)) //if collides with block, player's position is on edge of block
                        {
                            player1.position.X = blocks[i].position.X - player1.width;
                            isColliding = true;
                        }
                    }
                    if(!isColliding) //normal movement
                    {
                        player1.position.X += 10;
                        if (player1.onTopOfBlock)
                        {
                            if (player1.position.X >= blocks[0].position.X + blocks[0].width)
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
                player1.onTopOfBlock = false;
                player1.velocity += player1.acceleration;
                for (int i = 0; i < blocks.Count; i++ )
                {
                    if (player1.willCollide(blocks[i], DOWN, player1.velocity) && player1.velocity > 0)
                    {
                        player1.position.Y = blocks[i].position.Y - player1.height;
                        player1.velocity = 0;
                        player1.onTopOfBlock = true;
                        player1.whichBlock = i;
                        break;
                    }
                    if (player1.willCollide(blocks[i], DOWN, player1.velocity) && player1.velocity < 0)
                    {
                        player1.position.Y = blocks[i].position.Y + player1.height;
                        player1.velocity = 0;
                        break;
                    }
                }
                if (player1.position.Y + player1.velocity + player1.height > GraphicsDevice.Viewport.Height && !player1.onTopOfBlock)
                {
                    player1.onTopOfBlock = false;
                    player1.position.Y = GraphicsDevice.Viewport.Height - player1.height;
                }
                else if(!player1.onTopOfBlock)
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
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].Draw(spriteBatch);
            }
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            // End Drawing Code

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ShootGun(){


        }
    }
}
