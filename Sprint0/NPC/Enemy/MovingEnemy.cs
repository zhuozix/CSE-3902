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

namespace Sprint0.NPC.Enemy
{
    public class MovingEnemy : SpriteE
    {
        private static float moveSpeed = 100f;

        private GraphicsDeviceManager graphics;
        private Vector2 originalPosition;
        public Vector2 Velocity;
        private int moveDirectionX;
        private int moveDirectionY = 0;
        public bool crash { get; set; }


        public MovingEnemy(Texture2D texture, Vector2 position, int rows, int cols, GraphicsDeviceManager graphics, int moveDirection)
            : base(texture, position, rows, cols)
        {
            this.graphics = graphics;
            originalPosition = position;
            moveDirectionX = moveDirection;
            crash = false;
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
            if (crash)
            {

                moveDirectionX = moveDirectionX * -1;
                position += new Vector2(5 * moveDirectionX, 0);
                crash = false;
            }
            if (moveDirectionX != 0)
            {
                if (currentx >= 780)
                {
                    moveDirectionX = -1;
                }
                else if (currentx <= 0)
                {
                    moveDirectionX = 1;
                }
            }

        }

    }
}

