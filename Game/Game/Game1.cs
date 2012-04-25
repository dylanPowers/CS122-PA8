using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
        List<Enemy> enemies;
        Player player1;
        Player player2;

        int NUMBLOCKS;
        int NUMSPIKES;
        int NUMENEMIES;

        int CURRENTLEVEL = 1;

        Vector2 playerStart;

        Block door;

        int playerMoveSpeed;

        const int UP = 1;
        const int DOWN = 2;
        const int LEFT = 3;
        const int RIGHT = 4;

        SoundEffect jumpSound;
        Song music;

        Texture2D player1TextureLeft;
        Texture2D player1TextureRight;
        Texture2D player2TextureLeft;
        Texture2D player2TextureRight;

        Texture2D blockTexture;

        Texture2D spikeUpTexture;
        Texture2D spikeDownTexture;

        Texture2D enemyTextureLeft;
        Texture2D enemyTextureRight;

        Menu gameMenu;
        //End Declaration Code

        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            gameMenu = new Menu();
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
            music = Content.Load<Song>("music");
            //MediaPlayer.Play(music);

            blocks = new List<Block>();
            spikes = new List<Block>();
            enemies = new List<Enemy>();
            player1 = new Player();
            player2 = new Player();
            playerMoveSpeed = 8;

            door = new Block();
            //End Initialization Code

            base.Initialize();
            loadLevel(1);

            gameMenu.Initialize();
            
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
            blockTexture = Content.Load<Texture2D>("brick");

            spikeUpTexture = Content.Load<Texture2D>("spike");
            spikeDownTexture = Content.Load<Texture2D>("spike_upsidedown");

            player1TextureLeft = Content.Load<Texture2D>("player_purple_left");
            player1TextureRight = Content.Load<Texture2D>("player_purple_right");
            player2TextureLeft = Content.Load<Texture2D>("player_yellow_left");
            player2TextureRight = Content.Load<Texture2D>("player_yellow_right");

            player1.Initialize(player1TextureLeft, new Vector2(0, 0));
            player2.Initialize(player2TextureLeft, new Vector2(0, 0));

            door.Initialize(Content.Load<Texture2D>("door"), new Vector2(0, 0));

            enemyTextureLeft = Content.Load<Texture2D>("enemy_left");
            enemyTextureRight = Content.Load<Texture2D>("enemy_right");

            jumpSound = Content.Load<SoundEffect>("beep");


            gameMenu.LoadContent(Content);

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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    this.Exit();

            // Resets the character when the escape key is pressed
            //if (currentKeyboardState.IsKeyDown(Keys.Escape))
            //    

            //Begin Update Code
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (gameMenu.is_active)
            {
                this.IsMouseVisible = true;
            }
            gameMenu.Update(currentKeyboardState.IsKeyDown(Keys.Escape), previousKeyboardState.IsKeyDown(Keys.Escape));
            if (!gameMenu.is_active)
            {
                this.IsMouseVisible = false;
            }

            if (gameMenu.isExit())
            {

                this.Exit();
            }

            


            

//PLAYER UPDATE CODE


            if (!gameMenu.isPause())
            {
                if (gameMenu.isRestart())//This function will only return true once before resetting itself.
                {
                    player1.position = playerStart;
                    player2.position = playerStart;
                }

                if (player1.position.Y + player1.height < GraphicsDevice.Viewport.Height && !player1.onTopOfBlock) //check if player is in mid air
                {
                    player1.airbourne = true;
                }
                else
                {
                    player1.airbourne = false;
                    player1.velocity = 0;
                }

                if (player2.position.Y + player2.height < GraphicsDevice.Viewport.Height && !player2.onTopOfBlock) //check if player is in mid air
                {
                    player2.airbourne = true;
                }
                else
                {
                    player2.airbourne = false;
                    player2.velocity = 0;
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

                if (currentKeyboardState.IsKeyDown(Keys.W)) //jumping
                {
                    if (!player2.airbourne)
                    {
                        player2.airbourne = true;
                        player2.onTopOfBlock = false;
                        //beep.Play();
                        player2.jump();
                    }
                }

                if (currentKeyboardState.IsKeyDown(Keys.Left)) //moving player left
                {
                    player1.texture = player1TextureLeft;
                    bool isColliding = false;

                    if (player1.position.X - playerMoveSpeed > 0)
                    {
                        for (int i = 0; i < blocks.Count; i++)
                        {
                            if (player1.willCollide(blocks[i], LEFT, playerMoveSpeed)) //if collides with block, player's position is on edge of block
                            {
                                player1.position.X = blocks[i].position.X + blocks[i].width;
                                isColliding = true;
                                break;
                            }
                        }

                        for (int i = 0; i < spikes.Count; i++)
                        {
                            int upOrDown = 0;
                            if (spikes[i].texture == spikeUpTexture)
                                upOrDown = 0;
                            else
                                upOrDown = 1;
                            if (player1.willCollideTriangle(spikes[i], LEFT, playerMoveSpeed, upOrDown)) //if collides with spike, send player to start
                            {
                                player1.position = playerStart;
                                isColliding = true;
                                break;
                            }
                        }

                        if (player1.willCollide(door, LEFT, playerMoveSpeed))
                        {
                            CURRENTLEVEL++;
                            loadLevel(CURRENTLEVEL);
                            return;
                        }

                        if (!isColliding)//normal movement
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

                if (currentKeyboardState.IsKeyDown(Keys.A)) //moving player left
                {
                    player2.texture = player2TextureLeft;
                    bool isColliding = false;

                    if (player2.position.X - playerMoveSpeed > 0)
                    {
                        for (int i = 0; i < blocks.Count; i++)
                        {
                            if (player2.willCollide(blocks[i], LEFT, playerMoveSpeed)) //if collides with block, player's position is on edge of block
                            {
                                player2.position.X = blocks[i].position.X + blocks[i].width;
                                isColliding = true;
                                break;
                            }
                        }

                        for (int i = 0; i < spikes.Count; i++)
                        {
                            int upOrDown = 0;
                            if (spikes[i].texture == spikeUpTexture)
                                upOrDown = 0;
                            else
                                upOrDown = 1;
                            if (player2.willCollideTriangle(spikes[i], LEFT, playerMoveSpeed, upOrDown)) //if collides with spike, send player to start
                            {
                                player2.position = playerStart;
                                isColliding = true;
                                break;
                            }
                        }

                        if (player2.willCollide(door, LEFT, playerMoveSpeed))
                        {
                            CURRENTLEVEL++;
                            loadLevel(CURRENTLEVEL);
                            return;
                        }

                        if (!isColliding)//normal movement
                        {
                            player2.position.X -= 10;
                            if (player2.onTopOfBlock)
                            {
                                if (player2.position.X + player2.width <= blocks[player2.whichBlock].position.X)
                                    player2.onTopOfBlock = false;
                            }
                        }

                    }
                    else
                    {
                        player2.position.X = 0; //player's position is on left edge of screen
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

                        for (int i = 0; i < spikes.Count; i++)
                        {
                            int upOrDown = 0;
                            if (spikes[i].texture == spikeUpTexture)
                                upOrDown = 0;
                            else
                                upOrDown = 1;
                            if (player1.willCollideTriangle(spikes[i], RIGHT, playerMoveSpeed, upOrDown)) //if collides with spike, send player to start
                            {
                                player1.position = playerStart;
                                isColliding = true;
                                break;
                            }
                        }

                        if (player1.willCollide(door, RIGHT, playerMoveSpeed))
                        {
                            CURRENTLEVEL++;
                            loadLevel(CURRENTLEVEL);
                            return;
                        }

                        if (!isColliding) //normal movement
                        {
                            player1.position.X += 10;
                            if (player1.onTopOfBlock)
                            {
                                if (player1.position.X >= blocks[player1.whichBlock].position.X + blocks[player1.whichBlock].width)
                                    player1.onTopOfBlock = false;
                            }
                        }

                    }
                    else
                    {
                        player1.position.X = GraphicsDevice.Viewport.Width - player1.width; //player's position is on left edge of screen
                    }

                }

                if (currentKeyboardState.IsKeyDown(Keys.D)) //moving player right
                {
                    player2.texture = player2TextureRight;
                    bool isColliding = false;

                    if (player2.position.X + player2.width + playerMoveSpeed < GraphicsDevice.Viewport.Width)
                    {
                        for (int i = 0; i < blocks.Count; i++)
                        {
                            if (player2.willCollide(blocks[i], RIGHT, playerMoveSpeed)) //if collides with block, player's position is on edge of block
                            {
                                player2.position.X = blocks[i].position.X - player2.width;
                                isColliding = true;
                                break;
                            }
                        }

                        for (int i = 0; i < spikes.Count; i++)
                        {
                            int upOrDown = 0;
                            if (spikes[i].texture == spikeUpTexture)
                                upOrDown = 0;
                            else
                                upOrDown = 1;
                            if (player2.willCollideTriangle(spikes[i], RIGHT, playerMoveSpeed, upOrDown)) //if collides with spike, send player to start
                            {
                                player2.position = playerStart;
                                isColliding = true;
                                break;
                            }
                        }

                        if (player2.willCollide(door, RIGHT, playerMoveSpeed))
                        {
                            CURRENTLEVEL++;
                            loadLevel(CURRENTLEVEL);
                            return;
                        }

                        if (!isColliding) //normal movement
                        {
                            player2.position.X += 10;
                            if (player2.onTopOfBlock)
                            {
                                if (player2.position.X >= blocks[player2.whichBlock].position.X + blocks[player2.whichBlock].width)
                                    player2.onTopOfBlock = false;
                            }
                        }

                    }
                    else
                    {
                        player2.position.X = GraphicsDevice.Viewport.Width - player2.width; //player's position is on left edge of screen
                    }

                }

                if (player1.airbourne)
                {
                    player1.onTopOfBlock = false;
                    player1.velocity += player1.acceleration; //acceleration due to gravity
                    for (int i = 0; i < blocks.Count; i++)
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
                        int upOrDown = 0;
                        if (spikes[i].texture == spikeUpTexture)
                            upOrDown = 0;
                        else
                            upOrDown = 1;
                        if (player1.willCollideTriangle(spikes[i], DOWN, player1.velocity, upOrDown))
                        {
                            player1.position = playerStart;
                            player1.velocity = 0;
                        }
                    }

                    if (player1.willCollide(door, DOWN, player1.velocity))
                    {
                        player1.velocity = 0;
                        CURRENTLEVEL++;
                        loadLevel(CURRENTLEVEL);
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

                if (player2.airbourne)
                {
                    player2.onTopOfBlock = false;
                    player2.velocity += player2.acceleration; //acceleration due to gravity
                    for (int i = 0; i < blocks.Count; i++)
                    {
                        if (player2.willCollide(blocks[i], DOWN, player2.velocity) && player2.velocity > 0) //if player hits block with downward trajectory
                        {
                            player2.position.Y = blocks[i].position.Y - player2.height;
                            player2.velocity = 0;
                            player2.onTopOfBlock = true;
                            player2.whichBlock = i;
                            break;
                        }

                        if (player2.willCollide(blocks[i], DOWN, player2.velocity) && player2.velocity < 0) //if player hits block with upward trajectory
                        {
                            player2.position.Y = blocks[i].position.Y + player2.height;
                            player2.velocity = 0;
                            break;
                        }
                    }

                    for (int i = 0; i < spikes.Count; i++) //if player lands on spikes send player back to start
                    {
                        int upOrDown = 0;
                        if (spikes[i].texture == spikeUpTexture)
                            upOrDown = 0;
                        else
                            upOrDown = 1;
                        if (player2.willCollideTriangle(spikes[i], DOWN, player2.velocity, upOrDown))
                        {
                            player2.position = playerStart;
                            player2.velocity = 0;
                        }
                    }

                    if (player2.willCollide(door, DOWN, player2.velocity))
                    {
                        player2.velocity = 0;
                        CURRENTLEVEL++;
                        loadLevel(CURRENTLEVEL);
                    }

                    if (player2.position.Y + player2.velocity + player2.height > GraphicsDevice.Viewport.Height && !player2.onTopOfBlock) //if player lands on bottom of screen
                    {
                        player2.onTopOfBlock = false;
                        player2.position.Y = GraphicsDevice.Viewport.Height - player2.height;
                    }
                    else if (!player2.onTopOfBlock) //basic falling
                    {
                        player2.position.Y += player2.velocity;
                    }
                }

                //ENEMY UPDATE CODE
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i].goingLeft)
                    {
                        bool isColliding = false;
                        for (int i2 = 0; i2 < blocks.Count; i2++)
                        {
                            if (enemies[i].willCollide(blocks[i2], LEFT, enemies[i].speed)) //if enemy collides with a block, turns around
                            {
                                enemies[i].position.X = blocks[i2].position.X + blocks[i2].width;
                                enemies[i].goingLeft = false;
                                enemies[i].texture = enemyTextureRight;
                                isColliding = true;
                            }
                        }
                        if (!isColliding)
                        {
                            if (enemies[i].willCollide(player1, LEFT, enemies[i].speed)) //if enemy touches player send player back to start
                            {
                                player1.position = playerStart;
                            }
                            if (enemies[i].willCollide(player2, LEFT, enemies[i].speed)) //if enemy touches player send player back to start
                            {
                                player2.position = playerStart;
                            }
                            enemies[i].position.X -= enemies[i].speed; //moves enemy forward
                        }
                    }
                    else
                    {
                        bool isColliding = false;
                        for (int i2 = 0; i2 < blocks.Count; i2++)
                        {
                            if (enemies[i].willCollide(blocks[i2], RIGHT, enemies[i].speed)) //if enemy collides with a block, turns around
                            {
                                enemies[i].position.X = blocks[i2].position.X - enemies[i].width;
                                enemies[i].goingLeft = true;
                                enemies[i].texture = enemyTextureLeft;
                                isColliding = true;
                            }
                        }
                        if (!isColliding)
                        {
                            if (enemies[i].willCollide(player1, RIGHT, enemies[i].speed)) //if enemy touches player send player back to start
                            {
                                player1.position = playerStart;
                            }
                            if (enemies[i].willCollide(player2, RIGHT, enemies[i].speed)) //if enemy touches player send player back to start
                            {
                                player2.position = playerStart;
                            }

                            enemies[i].position.X += enemies[i].speed; //moves enemy forward
                        }
                    }
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

            door.Draw(spriteBatch);

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(spriteBatch);
            }

            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);

            gameMenu.draw(ref spriteBatch);
            // End Drawing Code

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void loadLevel(int level)
        {
            blocks.Clear();
            spikes.Clear();
            enemies.Clear();
            NUMBLOCKS = NUMSPIKES = NUMENEMIES = 0;

            string levelName = "Content/level";
            levelName += level;
            levelName += ".txt";
            string[] lines;

            try
            {
                lines = System.IO.File.ReadAllLines(levelName);
                int i = 1;
                string[] coordinates;

                //load blocks
                while (lines[i] != "")
                {
                    NUMBLOCKS++;
                    blocks.Add(new Block());
                    coordinates = lines[i].Split(' ');
                    blocks[NUMBLOCKS - 1].Initialize(blockTexture, new Vector2(Convert.ToInt32(coordinates[0]), Convert.ToInt32(coordinates[1])));
                    i++;
                }

                i += 2; //skips over title and blank line

                //load upspikes
                while (lines[i] != "")
                {
                    NUMSPIKES++;
                    spikes.Add(new Block());
                    coordinates = lines[i].Split(' ');
                    spikes[NUMSPIKES - 1].Initialize(spikeUpTexture, new Vector2(Convert.ToInt32(coordinates[0]), Convert.ToInt32(coordinates[1])));
                    i++;
                }

                i += 2; //skips over title and blank line

                //load downspikes
                while (lines[i] != "")
                {
                    NUMSPIKES++;
                    spikes.Add(new Block());
                    coordinates = lines[i].Split(' ');
                    spikes[NUMSPIKES - 1].Initialize(spikeDownTexture, new Vector2(Convert.ToInt32(coordinates[0]), Convert.ToInt32(coordinates[1])));
                    i++;
                }

                i += 2; //skips over title and blank line

                //load enemies
                while (lines[i] != "")
                {
                    NUMENEMIES++;
                    enemies.Add(new Enemy());
                    coordinates = lines[i].Split(' ');
                    enemies[NUMENEMIES - 1].Initialize(enemyTextureLeft, new Vector2(Convert.ToInt32(coordinates[0]), Convert.ToInt32(coordinates[1])));
                    i++;
                }

                i += 2; //skips over title and blank line

                coordinates = lines[i].Split(' ');
                playerStart = new Vector2(float.Parse(coordinates[0]), float.Parse(coordinates[1]));
                player1.Initialize(player1TextureLeft, playerStart);
                player2.Initialize(player2TextureLeft, playerStart);

                i += 3; //skips over title and blank line

                coordinates = lines[i].Split(' ');
                door.position.X = float.Parse(coordinates[0]);
                door.position.Y = float.Parse(coordinates[1]);
            }

            catch (FileNotFoundException e)
            {
                loadLevel(99);
                //Code of the end of the game... You Win! or something...
            }
        }
    }
}

