using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;


namespace Game
{
    class Menu
    {
        public bool is_active;
        private bool exit_flag;
        private bool restart_flag;

        private Texture2D restart_but;
        private Texture2D restart_but_clicked;
        private Texture2D restart_but_hover;
        private Texture2D restart_but_current;
        private Texture2D exit_but;
        private Texture2D exit_but_clicked;
        private Texture2D exit_but_hover;
        private Texture2D exit_but_current;
        private Texture2D bg;
        private Texture2D x_but;
        private Texture2D x_but_clicked;
        private Texture2D x_but_current;
        private Vector2 restart_but_pos;
        private Vector2 exit_but_pos;
        private Vector2 bg_pos;
        private Vector2 x_but_pos;
        private MouseState prev_mouse;

        public void Initialize()
        {
            is_active = false;
            exit_flag = false;
            restart_flag = false;
            restart_but_pos.X = 400;
            restart_but_pos.Y = 266;
            exit_but_pos.X = 650;
            exit_but_pos.Y = 266;
            x_but_pos.X = 800;
            x_but_pos.Y = 125;
            bg_pos.X = 313;
            bg_pos.Y = 88;
        }

        public void LoadContent(ContentManager Content)
        {
            restart_but = Content.Load<Texture2D>("MenuContent/RestartButton");
            restart_but_clicked = Content.Load<Texture2D>("MenuContent/RestartButtonClicked");
            restart_but_hover = Content.Load<Texture2D>("MenuContent/RestartButtonHover");
            exit_but = Content.Load<Texture2D>("MenuContent/ExitButton");
            exit_but_clicked = Content.Load<Texture2D>("MenuContent/ExitButtonClicked");
            exit_but_hover = Content.Load<Texture2D>("MenuContent/ExitButtonHover");
            x_but = Content.Load<Texture2D>("MenuContent/xButton");
            x_but_clicked = Content.Load<Texture2D>("MenuContent/xButtonClicked");

            bg = Content.Load<Texture2D>("MenuContent/MenuBackground");

            restart_but_current = restart_but;
            exit_but_current = exit_but;
            x_but_current = x_but;
           

        }

        public void draw(ref SpriteBatch sprites)
        {
            if (is_active)
            {
                sprites.Draw(bg, bg_pos, Color.White);
                sprites.Draw(restart_but_current, restart_but_pos, Color.White);
                sprites.Draw(exit_but_current, exit_but_pos, Color.White);
                sprites.Draw(x_but_current, x_but_pos, Color.White);
            }
        }

        public void setActive()
        {
            is_active = true;
        }

        public void Update(bool isEscapeKeyPressed, bool previousKeyState)
        {
            if (isEscapeKeyPressed && !previousKeyState)
            {
                if (is_active)
                {
                    is_active = false;
                }
                else
                {
                    is_active = true;
                }
            }

            if (is_active)
            {
                MouseState mouse = Mouse.GetState();

                //Check for the restart button.
                if ((mouse.X <= restart_but_pos.X + 140 && restart_but_pos.X <= mouse.X)
                                        &&
                    (mouse.Y <= restart_but_pos.Y + 68 && restart_but_pos.Y <= mouse.Y))
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        restart_but_current = restart_but_clicked;
                    }
                    else if (mouse.LeftButton == ButtonState.Released && prev_mouse.LeftButton == ButtonState.Pressed)
                    {
                        restart_flag = true;
                        is_active = false;
                    }
                    else
                    {
                        restart_but_current = restart_but_hover;
                    }
                }

                //Let's check for the exit button. 
                else if ((mouse.X <= exit_but_pos.X + 140 && exit_but_pos.X <= mouse.X)
                                                    &&
                         (mouse.Y <= exit_but_pos.Y + 68 && exit_but_pos.Y <= mouse.Y))
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        exit_but_current = exit_but_clicked;

                    }
                    else if (mouse.LeftButton == ButtonState.Released && prev_mouse.LeftButton == ButtonState.Pressed)
                    {
                        exit_flag = true;
                        is_active = false;
                    }
                    else
                    {
                        exit_but_current = exit_but_hover;
                    }
                }

                else if ((mouse.X <= x_but_pos.X + 43 && x_but_pos.X + 7 <= mouse.X)
                                                    &&
                         (mouse.Y <= x_but_pos.Y + 43 && x_but_pos.Y + 7 <= mouse.Y))
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        x_but_current = x_but_clicked;

                    }
                    else if (mouse.LeftButton == ButtonState.Released && prev_mouse.LeftButton == ButtonState.Pressed)
                    {
                        is_active = false;
                    }
                    else
                    {
                        x_but_current = x_but_clicked;
                    }

                }
                else
                {
                    exit_but_current = exit_but;
                    restart_but_current = restart_but;
                    x_but_current = x_but;
                }

                prev_mouse = mouse;
            }

        }

        public bool isPause()
        {
            if (is_active)
            {
                return true;
            }
            return false;
        }

        //This function only returns true once for every occurence.
        public bool isRestart()
        {
            bool flag = restart_flag;
            restart_flag = false;
            return flag;
        }

        public bool isExit()
        {
            return exit_flag;
        }
    }
}
