using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;


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
        }

        void draw()
        {

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
                    MouseState mouse = Mouse.GetState();
                }
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
