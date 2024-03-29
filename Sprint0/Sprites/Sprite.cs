﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Sprites;

namespace Sprint0.Content
{
    public abstract class Sprite: ISprite
    {
        public static double animateFrequency = 0.9 / 5.0;

        private Texture2D texture;
        private int numRows;
        private int numCols;
        internal int currentFrame;
        internal int totalFrames;

        public Vector2 position;
        public Vector2 velocity;
        public float speed;

        internal double timeSinceLastFrameTransition = 0;

        public Vector2 Position { get { return position; } set { position = value; } }
        public Texture2D Texture { get { return texture; } }
        public int Width { get { return texture.Width; } }
        public int Height { get { return texture.Height; } }

        public Sprite(Texture2D texture, Vector2 position, int rows, int cols)
        {
            this.texture = texture;
            this.numRows = rows;
            this.numCols = cols;
            this.position = position;
            this.currentFrame = 0;
            this.totalFrames = numRows * numCols;
        }
        public abstract void Update(GameTime gameTime);
        public void Draw(SpriteBatch _spriteBatch, bool isFlipped)
        {
            
                int width = texture.Width / numCols;
                int height = texture.Height / numRows;
                int row = currentFrame / numCols;
                int column = currentFrame % numCols;
                SpriteEffects spriteEffect;
                Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
                 
                if (isFlipped)
                {
                    spriteEffect = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    spriteEffect = SpriteEffects.None;
                }
               
                _spriteBatch.Begin();
                _spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, spriteEffect, 0);
                _spriteBatch.End();
            
        }
       
    }
}
