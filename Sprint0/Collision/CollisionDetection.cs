using Microsoft.Xna.Framework;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Collision
{
    public class CollisionDetection
    {

        public CollisionDetection() 
        { 
        }
        private bool rightXTouchMain(Rectangle a, Rectangle b)
        {
            return Math.Abs(b.Left - a.Right) <= Math.Abs(a.Bottom - b.Top);
        }

        private bool leftXTouchMain(Rectangle a, Rectangle b)
        {
            return Math.Abs(b.Right - a.Left) <= Math.Abs(a.Bottom - b.Top);
        }

        public bool touchLeft(Rectangle mainInstance, Rectangle anotherInstance, ISprite mainSprite)
        {
            if (!leftXTouchMain(mainInstance, anotherInstance))
            {
                return false;
            }
            else if (touchBottom2(mainInstance, anotherInstance)) { return false; }
            return mainInstance.Left < anotherInstance.Right && mainInstance.Right > anotherInstance.Right;
        }
        public bool touchBottom2(Rectangle mainInstance, Rectangle anotherInstance)
        {
            return mainInstance.Bottom - anotherInstance.Top <= 20 && mainInstance.Bottom < anotherInstance.Bottom;
            //return mainInstance.Bottom - anotherInstance.Top <= 5 
        }

        public bool touchBottom(Rectangle mainInstance, Rectangle anotherInstance)
        {
            return mainInstance.Bottom - anotherInstance.Top <= 20 && mainInstance.Bottom < anotherInstance.Bottom && anotherInstance.Left - mainInstance.Left < 28 && mainInstance.Right - anotherInstance.Right < 28;
            //return mainInstance.Bottom - anotherInstance.Top <= 5 
        }
        public bool touchBottomEnemy(Rectangle mainInstance, Rectangle anotherInstance)
        {
            return mainInstance.Bottom > anotherInstance.Top && mainInstance.Bottom < anotherInstance.Bottom - 8;
            //return mainInstance.Bottom > anotherInstance.Top && mainInstance.Bottom < anotherInstance.Bottom;
        }

        public bool touchRight(Rectangle mainInstance, Rectangle anotherInstance)
        {
            if (!rightXTouchMain(mainInstance, anotherInstance))
            {
                return false;
            }
            else if (touchBottom2(mainInstance, anotherInstance)) { return false; }
            return mainInstance.Right > anotherInstance.Left && mainInstance.Left < anotherInstance.Left;
        }
        public bool touchTop(Rectangle mainInstance, Rectangle anotherInstance)
        {

            if (mainInstance.Left < anotherInstance.Left - 24 || mainInstance.Right > anotherInstance.Right + 24)
            {
                return false;
            }
            return anotherInstance.Bottom - mainInstance.Top <= 8;

        }

        public Rectangle getRectangle(ISprite a)
        {
            int scale = Game1.scale;
            int startX = (int)a.Position.X;
            int startY = (int)a.Position.Y;

            int endX = (((int)a.Texture.Width / a.cols) * scale);
            int endy = (((int)a.Texture.Height / a.rows) * scale);
            return new Rectangle(startX, startY, endX, endy);
        }

    }
}
