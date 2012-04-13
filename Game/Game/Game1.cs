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
        List<Block> spikes;
        Player player1;
        Player player2;

        Enemy enemy1;

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

        Texture2D spikeTexture;

        Texture2D enemyTextureLeft;
        Texture2D enemyTextureRight;
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
            spikes = new List<Block>();
            for (int i = 0; i < 50; i++ ) //add needed number of blocks to block list
                blocks.Add(new Block());
            for (int i = 0; i < 11; i++)
                spikes.Add(new Block()); //add needed number of spikes to spike list
            player1 = new Player();
            player2 = new Player();
            playerMoveSpeed = 10;

            enemy1 = new Enemy();
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
            blocks[0].Initialize(textureBlock, new Vector2(0, 550));
            blocks[1].Initialize(textureBlock, new Vector2(150, 550));
            blocks[2].Initialize(textureBlock, new Vector2(150, 500));
            blocks[3].Initialize(textureBlock, new Vector2(150, 450));
            blocks[4].Initialize(textureBlock, new Vector2(150, 400));
            blocks[5].Initialize(textureBlock, new Vector2(150, 350));
            blocks[6].Initialize(textureBlock, new Vector2(150, 300));
            blocks[7].Initialize(textureBlock, new Vector2(150, 250));
            blocks[8].Initialize(textureBlock, new Vector2(150, 200));
            blocks[9].Initialize(textureBlock, new Vector2(150, 150));
            blocks[10].Initialize(textureBlock, new Vector2(150, 100));
            blocks[11].Initialize(textureBlock, new Vector2(100, 400));
            blocks[12].Initialize(textureBlock, new Vector2(0, 250));
            blocks[13].Initialize(textureBlock, new Vector2(100, 100));
            blocks[14].Initialize(textureBlock, new Vector2(200, 200));
            blocks[15].Initialize(textureBlock, new Vector2(250, 200));
            blocks[16].Initialize(textureBlock, new Vector2(300, 200));
            blocks[17].Initialize(textureBlock, new Vector2(350, 200));
            blocks[18].Initialize(textureBlock, new Vector2(400, 200));
            blocks[19].Initialize(textureBlock, new Vector2(450, 200));
            blocks[20].Initialize(textureBlock, new Vector2(500, 200));
            blocks[21].Initialize(textureBlock, new Vector2(550, 200));
            blocks[22].Initialize(textureBlock, new Vector2(600, 200));
            blocks[23].Initialize(textureBlock, new Vector2(600, 150));
            blocks[24].Initialize(textureBlock, new Vector2(600, 100));
            blocks[25].Initialize(textureBlock, new Vector2(650, 100));
            blocks[26].Initialize(textureBlock, new Vector2(700, 100));
            blocks[27].Initialize(textureBlock, new Vector2(750, 100));
            blocks[28].Initialize(textureBlock, new Vector2(800, 100));
            blocks[29].Initialize(textureBlock, new Vector2(850, 100));
            blocks[30].Initialize(textureBlock, new Vector2(900, 100));
            blocks[31].Initialize(textureBlock, new Vector2(950, 100));
            blocks[32].Initialize(textureBlock, new Vector2(750, 300));
            blocks[33].Initialize(textureBlock, new Vector2(800, 300));
            blocks[34].Initialize(textureBlock, new Vector2(950, 300));
            blocks[35].Initialize(textureBlock, new Vector2(1100, 350));
            blocks[36].Initialize(textureBlock, new Vector2(1150, 350));
            blocks[37].Initialize(textureBlock, new Vector2(1150, 450));
            blocks[38].Initialize(textureBlock, new Vector2(1100, 450));
            blocks[39].Initialize(textureBlock, new Vector2(1050, 450));
            blocks[40].Initialize(textureBlock, new Vector2(1000, 450));
            blocks[41].Initialize(textureBlock, new Vector2(950, 450));
            blocks[42].Initialize(textureBlock, new Vector2(900, 450));
            blocks[43].Initialize(textureBlock, new Vector2(850, 450));
            blocks[44].Initialize(textureBlock, new Vector2(800, 450));
            blocks[45].Initialize(textureBlock, new Vector2(750, 450));
            blocks[46].Initialize(textureBlock, new Vector2(700, 450));
            blocks[47].Initialize(textureBlock, new Vector2(650, 450));
            blocks[48].Initialize(textureBlock, new Vector2(600, 450));
            blocks[49].Initialize(textureBlock, new Vector2(600, 400));

            spikeTexture = Content.Load<Texture2D>("spike");
            spikes[0].Initialize(spikeTexture, new Vector2(750, 425));
            spikes[1].Initialize(spikeTexture, new Vector2(800, 425));
            spikes[2].Initialize(spikeTexture, new Vector2(850, 425));
            spikes[3].Initialize(spikeTexture, new Vector2(900, 425));
            spikes[4].Initialize(spikeTexture, new Vector2(950, 425));
            spikes[5].Initialize(spikeTexture, new Vector2(1000, 425));
            spikes[6].Initialize(spikeTexture, new Vector2(1050, 425));
            spikes[7].Initialize(spikeTexture, new Vector2(1100, 425));
            spikes[8].Initialize(spikeTexture, new Vector2(1150, 425));
            spikes[9].Initialize(spikeTexture, new Vector2(650, 425));
            spikes[10].Initialize(spikeTexture, new Vector2(700, 425));

            player1TextureLeft = Content.Load<Texture2D>("player_purple_left");
            player1TextureRight = Content.Load<Texture2D>("player_purple_right");
            player2TextureLeft = Content.Load<Texture2D>("player_yellow_left");
            player2TextureRight = Content.Load<Texture2D>("player_yellow_right");
            Vector2 playerVector = new Vector2(100, GraphicsDevice.Viewport.Height - player1TextureLeft.Height);
            player1.Initialize(player1TextureLeft, playerVector);
            //player2.Initialize(playerTexture2, playerVector);

            Vector2 enemyVector = new Vector2(550, 150);
            enemyTextureLeft = Content.Load<Texture2D>("enemy_left");
            enemyTextureRight = Content.Load<Texture2D>("enemy_right");
            enemy1.Initialize(enemyTextureLeft, enemyVector);

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

            //Begin Update Code

//PLAYER UPDATE CODE
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

            if (currentKeyboardState.IsKeyDown(Keys.Up)) //jumping
            {
                if (!player1.airbourne)
                {
                    player1.airbourne = true;
                    player1.onTopOfBlock = false;
                    //beep.Play();
                    player1.jump();
                }
            }

            if (currentKeyboardState.IsKeyDown(Keys.Left)) //moving player left
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
                            isColliding = true; break;
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
                    player1.position.X = 0; //player's position is on left edge of screen
                }
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right)) //moving player right
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
                            break;
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
                    player1.position.X = GraphicsDevice.Viewport.Width - player1.width; //player's position is on left edge of screen
                }
                
            }

            if (player1.airbourne)
            {
                player1.onTopOfBlock = false;
                player1.velocity += player1.acceleration; //acceleration due to gravity
                for (int i = 0; i < blocks.Count; i++ )
                {
                    if (player1.willCollide(blocks[i], DOWN, player1.velocity) && player1.velocity > 0) //if player hits block with downward trajectory
                    {
                        player1.position.Y = blocks[i].position.Y - player1.height;
                        player1.velocity = 0;
                        player1.onTopOfBlock = true;
                        player1.whichBlock = i;
                        break;
                    }
                    if (player1.willCollide(blocks[i], DOWN, player1.velocity) && player1.velocity < 0) //if player hits block with upward trajectory
                    {
                        player1.position.Y = blocks[i].position.Y + player1.height;
                        player1.velocity = 0;
                        break;
                    }
                }
                for (int i = 0; i < spikes.Count; i++) //if player lands on spikes send player back to start
                {
                    if (player1.willCollide(spikes[i], DOWN, player1.velocity))
                    {
                        player1.position.X = 100;
                        player1.position.Y = GraphicsDevice.Viewport.Height - player1TextureLeft.Height;
                    }
                }
                if (player1.position.Y + player1.velocity + player1.height > GraphicsDevice.Viewport.Height && !player1.onTopOfBlock) //if player lands on bottom of screen
                {
                    player1.onTopOfBlock = false;
                    player1.position.Y = GraphicsDevice.Viewport.Height - player1.height;
                }
                else if (!player1.onTopOfBlock) //basic falling
                {
                    player1.position.Y += player1.velocity;
                }
            }

//ENEMY UPDATE CODE
            if (enemy1.goingLeft)
            {
                if (enemy1.willCollide(blocks[9], LEFT, enemy1.speed)) //if enemy collides with leftmost block, turns around
                {
                    enemy1.position.X = blocks[9].position.X + blocks[9].width;
                    enemy1.goingLeft = false;
                    enemy1.texture = enemyTextureRight;
                }
                else
                {
                    if (enemy1.willCollide(player1, LEFT, enemy1.speed)) //if enemy touches player send player back to start
                    {
                        player1.position.X = 100;
                        player1.position.Y = GraphicsDevice.Viewport.Height - player1TextureLeft.Height;
                    }
                    enemy1.position.X -= enemy1.speed; //moves enemy forward
                }
            }
            else
            {
                if (enemy1.willCollide(blocks[23], RIGHT, enemy1.speed)) //if enemy collides with rightmost block, turns around
                {
                    enemy1.position.X = blocks[23].position.X - enemy1.width;
                    enemy1.goingLeft = true;
                    enemy1.texture = enemyTextureLeft;
                }
                else
                {
                    if (enemy1.willCollide(player1, RIGHT, enemy1.speed)) //if enemy touches player send player back to start
                    {
                        player1.position.X = 100;
                        player1.position.Y = GraphicsDevice.Viewport.Height - player1TextureLeft.Height;
                    }

                    enemy1.position.X += enemy1.speed; //moves enemy forward
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

            for (int i = 0; i < spikes.Count; i++)
            {
                spikes[i].Draw(spriteBatch);
            }

            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);

            enemy1.Draw(spriteBatch);
            // End Drawing Code

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
