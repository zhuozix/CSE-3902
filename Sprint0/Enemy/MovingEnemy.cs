using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;

namespace Sprint0.Enemy
{
    public class MovingEnemy : SpriteE
    {
        private static float moveSpeed = 100f;

        private GraphicsDeviceManager graphics;
        private Vector2 originalPosition;
        public Vector2 Velocity;
        private int moveDirection;

        public MovingEnemy(Texture2D texture, Vector2 position, int rows, int cols, GraphicsDeviceManager graphics, int moveDirection)
            : base(texture, position, rows, cols)
        {
            this.graphics = graphics;
            originalPosition = position;
            this.moveDirection = moveDirection;
        }
        public override void Update(GameTime gameTime)
        {
            float currentx = position.X;
            timeSinceLastFrameTransition += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastFrameTransition > animateFrequency)
            {
                timeSinceLastFrameTransition = 0;
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }
            }
            Velocity = new Vector2(moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * moveDirection, 0);

            position += Velocity;
            if (currentx >= 780)
            {
                moveDirection = -1;
            }
            else if (currentx <= 0)
            {
                moveDirection = 1;
            }
        }

    }
}

