using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;
using Sprint0.ObjectManager;
using Sprint0.Collision;
using System.Diagnostics;

namespace Sprint0.Enemy
{
    public class MovingEnemy : SpriteE
    {
        private static float moveSpeed = 100f;

        private GraphicsDeviceManager graphics;
        private Vector2 originalPosition;
        public Vector2 Velocity;
        private int moveDirectionX;
        private int moveDirectionY = 0;
        

        public MovingEnemy(Texture2D texture, Vector2 position, int rows, int cols, GraphicsDeviceManager graphics, int moveDirection)
            : base(texture, position, rows, cols)
        {
            this.graphics = graphics;
            originalPosition = position;
            this.moveDirectionX = moveDirection;
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
            Velocity = new Vector2(moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * moveDirectionX, moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * moveDirectionY);

            position += Velocity;
            if (collide)
            {
                moveDirectionX = 0;
            }
           /* else if(collide && moveDirectionX == -1)
            {
                moveDirectionX = 1;
                collide = false;
            }*/

            if (collideA)
            {
                moveDirectionX = -1;
            }
            else
            {
                moveDirectionX = 1;
            }
       /*     if (currentx >= 780)
            {
                moveDirection = -1;
            }
            else if (currentx <= 0)
            {
                moveDirection = 1;
            }*/
        }

    }
}

