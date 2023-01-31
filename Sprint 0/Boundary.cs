using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint_0
{
    public class Boundary
    {
        private Rectangle _bounds = new Rectangle(350, 200, 105, 73);
        private Rectangle _soruce = new Rectangle(0, 0, 105, 73);
        private bool boundary = true;
        private int x = 350;
        private int y = 200;

        public Boundary()
        {
            //Nothing Needed here
        }
        public Rectangle BoundaryX()                      //check hit the left end and right end
        {
            if (boundary)
            {
                _bounds = new Rectangle(x++, y, 105, 73);          //move to right
                if (x == 700)                                      //hit the right edge
                {
                    boundary = false;
                }
            }
            else
            {
                _bounds = new Rectangle(x--, y, 105, 73);         //move to left
                if (x == 0)                                      //hit the left edge
                {
                    boundary = true;
                }
            }
            return _bounds;
        }
        public Rectangle BoundaryY()                             //check hit the top and bottom
        {
            if (boundary)
            {
                _bounds = new Rectangle(x, y++, 105, 73);           //move down
                if (y == 430)                                       //hit the bottom
                {
                    boundary = false;
                }
            }
            else
            {
                _bounds = new Rectangle(x, y--, 105, 73);          //move up
                if (y == 0)                                        //hit the top
                {
                    boundary = true;
                }
            }
            return _bounds;
        }
    }
}
