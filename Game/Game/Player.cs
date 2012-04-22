using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game
{
    class Player : MovableBlock
    {
        public int acceleration;
        public int velocity;
        public bool onTopOfBlock;
        public int whichBlock;

        public bool airbourne;

        public void jump()
        {
            velocity = -28;
        }

        public override void Initialize(Texture2D initTexture, Vector2 initPosition)
        {
            base.Initialize(initTexture, initPosition);
            acceleration = 2;
            velocity = 0;
            onTopOfBlock = false;
        }
            /*String box = "Hello";
            switch (box)
            {
                case "Hello":
                    box = "too";
                    break;
            }*/
    }
}
