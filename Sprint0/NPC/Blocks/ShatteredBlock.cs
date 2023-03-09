using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Sprites;

namespace Sprint0.NPC.Blocks
{
    internal class ShatteredBlock : ISprite
    {
        public string Name { get; set; }
        public string state { get; set; }
        public bool crash { get; set; }
        public Texture2D Texture { get; set; }
        public int rows { get; set; }
        public int cols { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 velocity { get; set; }

        public int Width => throw new NotImplementedException();

        public int Height => throw new NotImplementedException();

        //public bool collide { get; set; }
        //public bool collideA { get; set; }

        private int currentFrame;
        private int totalFrames;
        internal double timeSinceLastFrameTransition = 0.0;
        public static double animateFrequency = 0.9 / 5.0;

        public ShatteredBlock(Texture2D texture, int rowsIn, int columns, Vector2 l)
        {
            Texture = texture;
            this.rows = rows;
            this.cols = columns;
            currentFrame = 0;
            totalFrames = this.rows * this.cols;
           
            Position = l;
        }
        public void Update(GameTime gameTime)
        {
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
        }

        public void Draw(SpriteBatch spriteBatch, bool isFlipped)
        {
            int width = Texture.Width / this.cols;
            int height = Texture.Height / this.rows;
            int row = currentFrame / this.cols;
            int column = currentFrame % this.cols;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width * Game1.scale, height * Game1.scale);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);

        }
    }
}

