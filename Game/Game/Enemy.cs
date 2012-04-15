using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game
{
    class Enemy : MovableBlock
    {
        public int speed;
        public bool goingLeft;

        public override void Initialize(Texture2D initTexture, Vector2 initPosition)
        {
            base.Initialize(initTexture, initPosition);
            speed = 5;
            goingLeft = true;
        }
    }
}
