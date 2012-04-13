using System;
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

        public bool willCollide(Block obj2, int direction, int speed)
        {
            Rectangle obj1Rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
            switch (direction)
            {
                case 1: obj1Rectangle.Y -= speed; break;    //up
                case 2: obj1Rectangle.Y += speed; break;    //down
                case 3: obj1Rectangle.X -= speed; break;    //left
                case 4: obj1Rectangle.X += speed; break;    //right
            }
            Rectangle obj2Rectangle = new Rectangle((int)obj2.position.X, (int)obj2.position.Y, obj2.width, obj2.height);

            return obj1Rectangle.Intersects(obj2Rectangle);
        }
    }
}
