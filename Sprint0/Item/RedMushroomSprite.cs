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
    internal class RedMushroomSprite : MovingNoneAnimatedSprite
    {
        private GraphicsDeviceManager graphics;
        private Vector2 originalPosition;
        //1 means right and down, -1 means move left and up
        private int moveDirection;
        static float moveSpeed = 100f;
        private int xDirection = 1;
        public RedMushroomSprite(Texture2D texture, Vector2 position, int rows, int cols, GraphicsDeviceManager graphics, int moveDirection)
            : base(texture, position, rows, cols, graphics, moveDirection)
        {
            this.graphics = graphics;
            this.originalPosition = position;
            this.moveDirection = moveDirection;
        }

        public override void Update(GameTime gameTime)
        {
            float currHeight = position.Y;
            float currWidth = position.X;
            float maxHeight = graphics.PreferredBackBufferHeight - (Texture.Height + 1);
            float maxWidth = graphics.PreferredBackBufferWidth - (Texture.Width + 1);
            float minHeight = 0;
            float minWidth = 0;
            if (currHeight >= maxHeight)
            {
                position.Y = maxHeight;
            }

            if (Position.X >= maxWidth && xDirection > 0)
            {
                position.X = maxWidth - 1;
                xDirection = -xDirection;
            }
            else if (Position.X <= minWidth && xDirection < 0)
            {
                xDirection = -xDirection;
            }


            position += new Vector2(moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * xDirection, moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * moveDirection);

        }
    }
}
