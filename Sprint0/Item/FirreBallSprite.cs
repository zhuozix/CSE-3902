using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Item
{
    internal class FireBallSprite : MovingAnimatedSprite
    {

        private GraphicsDeviceManager graphics;
        private Vector2 originalPosition;
        //1 means right and down, -1 means move left and up
        private int moveDirection;
        static float moveSpeed = 100f;
        private int xDirection = 1;
        public bool isFilped = true;
        public FireBallSprite(Texture2D texture, Vector2 position, int rows, int cols, GraphicsDeviceManager graphics, int moveDirection, bool isFilepd)
            : base(texture, position, rows, cols, graphics, moveDirection)
        {
            this.graphics = graphics;
            this.originalPosition = position;
            this.moveDirection = moveDirection;
            this.isFilped = isFilepd;
        }

    }
}
