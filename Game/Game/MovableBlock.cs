using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game
{
    class MovableBlock : Block
    {
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

        public bool willCollideTriangle(Block obj2, int direction, int speed, int upOrDown)
        {
            bool status = false;
            if(this.willCollide(obj2, direction, speed))
            {
                switch (direction)
                {
                    case 1: position.Y -= speed; break;    //up
                    case 2: position.Y += speed; break;    //down
                    case 3: position.X -= speed; break;    //left
                    case 4: position.X += speed; break;    //right
                }

                Vector2 topLeftCorner = position;
                Vector2 topRightCorner = new Vector2(position.X, position.Y + width);
                Vector2 bottomLeftCorner = new Vector2(position.X + height, position.Y);
                Vector2 bottomRightCorner = new Vector2(position.X + height, position.Y + width);
                Vector2 triangleTop = new Vector2(obj2.position.X + (1 / 2) * obj2.width, obj2.position.Y);
                Vector2 triangleBottomLeft = new Vector2(obj2.position.X, obj2.position.Y + obj2.height);
                Vector2 triangleBottomRight = new Vector2(obj2.position.X + obj2.width, obj2.position.Y + obj2.height);
                Vector2 triangleBottom = new Vector2(obj2.position.X + (1 / 2) * obj2.width, obj2.position.Y + obj2.height);
                Vector2 triangleTopLeft = obj2.position;
                Vector2 triangleTopRight = new Vector2(obj2.position.X + obj2.width, obj2.position.Y);

                if (upOrDown == 0) // upward pointing triangle
                {
                    if (bottomLeftCorner.X > triangleTop.X) // on right side of triangle
                    {
                        if (pointInTriangle(bottomLeftCorner, triangleTop, triangleBottom, triangleBottomRight))
                            status = true;
                    }
                    else // on left side of triangle
                    {
                        if (pointInTriangle(bottomRightCorner, triangleTop, triangleBottom, triangleBottomLeft))
                            status = true;
                    }
                }
                else // downward pointing triangle
                {
                    if (topLeftCorner.X > triangleBottom.X) // on right side of triangle
                    {
                        if (pointInTriangle(topLeftCorner, triangleTop, triangleBottom, triangleTopRight))
                            status = true;
                    }
                    else // on left side of triangle
                    {
                        if (pointInTriangle(topRightCorner, triangleTop, triangleBottom, triangleTopLeft))
                            status = true;
                    }
                }

                switch (direction)
                {
                    case 1: position.Y += speed; break;    //up
                    case 2: position.Y -= speed; break;    //down
                    case 3: position.X += speed; break;    //left
                    case 4: position.X -= speed; break;    //right
                }
            }

            return status;
        }

        private bool sameSide(Vector2 p1, Vector2 p2, Vector2 a, Vector2 b)
        {
            Vector2 cp1 = Cross(b - a, p1 - a);
            Vector2 cp2 = Cross(b - a, p2 - a);
            return Vector2.Dot(cp1, cp2) >= 0;
        }
        private bool pointInTriangle(Vector2 p, Vector2 a, Vector2 b, Vector2 c)
        { 
            if(sameSide(p,a,b,c) && sameSide(p,b,a,c) && sameSide(p,c,a,b))
                return true;
            return false;
        }

        private Vector2 Cross(Vector2 p1, Vector2 p2)
        {
            Vector3 result3D = Vector3.Cross(new Vector3(p1, 0), new Vector3(p2, 0));
            Vector2 result = new Vector2(result3D.X, result3D.Y);
            return result;
        }
    }
}
