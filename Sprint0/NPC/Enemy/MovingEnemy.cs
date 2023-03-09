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
using System.Security.Cryptography.X509Certificates;

namespace Sprint0.NPC.Enemy
{
    public class MovingEnemy : SpriteE
    {
        public float moveSpeed = 100f;

        private GraphicsDeviceManager graphics;
        private Vector2 originalPosition;
        private Vector2 velocity;
        private int moveDirectionX;
        public int moveDirectionY = 0;
        public bool crash { get; set; }


        public MovingEnemy(Texture2D texture, Vector2 position, int rowsIn, int colsIn, GraphicsDeviceManager graphics, int moveDirection)
            : base(texture, position, rowsIn, colsIn)
        {
            this.graphics = graphics;
            originalPosition = position;
            moveDirectionX = moveDirection * -1;
            crash = false;
        }
        public override void Update(GameTime gameTime)
        {
            if (this.state == "Dead" || this.state == "idle" || this.state == "out")
            {
                moveSpeed = 0;
            }
            else if (this.state == "Rolling")
            {
                moveSpeed = 300;
            }
            else
            {
                moveSpeed = 100;
            }

            
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
            velocity = new Vector2(moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * moveDirectionX, moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * moveDirectionY);

            position += velocity;
            if (crash)
            {

                moveDirectionX = moveDirectionX * -1;
                position += new Vector2(5 * moveDirectionX, 0);
                crash = false;
            }
            
            if (moveDirectionX != 0)
            {
                 if (currentx <= 0)
                {
                    moveDirectionX = 1;
                }
            } 

        }

    }
}

