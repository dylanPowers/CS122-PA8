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
        private bool is_active;
        private bool exit_flag;
        private bool restart_flag;
        private Texture2D restart_button;
        private Texture2D exit_button;
        private Vector2 restart_but_pos;
        private Vector2 exit_but_pos;
        private bool mouse_viewable;

        void Initialize(ref bool IsMouseVisible)
        {
            mouse_viewable = IsMouseVisible;
            is_active = false;
            exit_flag = false;
            restart_flag = false;
            restart_but_pos.X = 600;
            restart_but_pos.Y = 300;
            exit_but_pos.X = 800;
            exit_but_pos.Y = 300;
        }

        void LoadContent(ref ContentManager Content)
        {
            restart_button = Content.Load<Texture2D>("Content/MenuContent/RestartMenuButton");
            exit_button = Content.Load<Texture2D>("Content/MenuContent/ExitMenuButton");
        }

        void draw(ref SpriteBatch sprites)
        {
            sprites.Draw(restart_button, restart_but_pos, Color.White);
            sprites.Draw(exit_button, exit_but_pos, Color.White);
        }

        void setActive()
        {
            is_active = true;
        }

        void Update(bool isEscapeKeyPressed)
        {
            if (is_active)
            {
                if (isEscapeKeyPressed)
                {
                    is_active = false;
                }
                else
                {
                    mouse_viewable = true;
                    MouseState mouse = Mouse.GetState();
                }

                mouse_viewable = false;
            }
        }

        void isPause()
        {

        }

        void isRestart()
        {

        }

        void isExit()
        {


        }
    }
}
