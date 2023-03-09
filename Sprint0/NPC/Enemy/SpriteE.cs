﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Sprites;

namespace Sprint0.NPC.Enemy
{
    public abstract class SpriteE : ISprite
    {
        public string Name { get; set; }
        public string state { get; set; }
        public static double animateFrequency = 0.9 / 5.0;
        public bool crash { get; set; }
        private Texture2D texture;

        public int rows { get; set; }
        public int cols { get; set; }
        internal int currentFrame;
        internal int totalFrames;
        SpriteEffects spriteEffect = SpriteEffects.None;

        public Vector2 position;
        public Vector2 velocity { get; set; }
        public float speed;
        public bool collide { get; set; }

        internal double timeSinceLastFrameTransition = 0;

        public Vector2 Position { get { return position; } set { position = value; } }
        public Texture2D Texture { get { return texture; } }
        public int Width { get { return texture.Width; } }
        public int Height { get { return texture.Height; } }

        public bool collideA { get; set; }

        public SpriteE(Texture2D texture, Vector2 position, int rowsIn, int colsIn)
        {
            this.texture = texture;
            this.rows = rowsIn;
            this.cols = colsIn;
            this.position = position;
            currentFrame = 0;
            totalFrames = rows * cols;
        }
        public abstract void Update(GameTime gameTime);
        public void Draw(SpriteBatch _spriteBatch, bool isFlipped)
        {

            int width = texture.Width / cols;
            int height = texture.Height / rows;
            int row = currentFrame / cols;
            int column = currentFrame % cols;
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            if (collideA)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
            }
            else
            {
                spriteEffect = SpriteEffects.None;
            }

            _spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, spriteEffect, 0);


        }

    }
}
