using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game
{
    class Player : Block
    {
        public int health;
        public int acceleration;
        public int velocity;

        public override void Initialize(Texture2D initTexture, Vector2 initPosition)
        {
            base.Initialize(initTexture, initPosition);
            health = 5;
            acceleration = 10;
            velocity = 0;
        }
    }
}
