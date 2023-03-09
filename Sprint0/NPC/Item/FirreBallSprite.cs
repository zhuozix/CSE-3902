﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.NPC.Item
{
    internal class FireBallSprite : MovingAnimatedSprite
    {

        private GraphicsDeviceManager graphics;
        private Vector2 originalPosition;
        //1 means right and down, -1 means move left and up
        private int moveDirection;
        //private float moveSpeed = 100f;
        private int xDirection = 1;
        public bool isFliped = true;
        public float time = 0f;
        public FireBallSprite(Texture2D texture, Vector2 position, int rowsIn, int colsIn, GraphicsDeviceManager graphics, int moveDirection, bool isFliped) : base(texture, position, rowsIn, colsIn, graphics, moveDirection)
        {
            this.graphics = graphics;
            originalPosition = position;
            this.moveDirection = moveDirection;
            this.isFliped = isFliped;
            xDirection = moveDirection;
            velocity = new Vector2(100f, 0);
        }

        public override void Update(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            float currHeight = position.Y;
            float currWidth = position.X;
            float maxHeight = graphics.PreferredBackBufferHeight - (Texture.Height + 1);
            float maxWidth = graphics.PreferredBackBufferWidth - (Texture.Width / totalFrames + 1);
            float minHeight = graphics.PreferredBackBufferHeight - 50;
            float minWidth = 0;

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

            if (currHeight >= maxHeight)
            {
                moveDirection = -moveDirection;
            }
            else if (currHeight <= minHeight && moveDirection < 0)
            {
                moveDirection = -moveDirection;
            }

            if (Position.X >= maxWidth && xDirection > 0)
            {
                xDirection = -xDirection;
            }
            else if (Position.X <= minWidth && xDirection < 0)
            {
                xDirection = -xDirection;
            }


            position += new Vector2(velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds * xDirection, velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds * moveDirection);

        }

    }
}
