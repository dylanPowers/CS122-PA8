﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game
{
    class Block
    {
        public Vector2 position;
        public Texture2D texture;
        public bool active;
        public int height
        {
            get { return texture.Height; }
        }
        public int width
        {
            get { return texture.Width; }
        }

        public virtual void Initialize(Texture2D initTexture, Vector2 initPosition)
        {
            texture = initTexture;
            position = initPosition;
            active = true;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }
    }
}
