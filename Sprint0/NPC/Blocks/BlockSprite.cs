using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading;
using Sprint0.Sprites;

namespace Sprint0.NPC.Blocks
{
    internal class BlockSprite : ISprite
    {
        public string Name { get; set; }
        public string state { get; set; }
        public Texture2D Texture { get; set; }
        public int rows { get; set; }
        public int cols { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 originalPosition;
        public Vector2 velocity { get; set; }
        public bool crash { get; set; }

        public int Width => throw new NotImplementedException();

        public int Height => throw new NotImplementedException();

        public bool collide { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool collideA { get; set; }

        private int currentFrame;
        private int totalFrames;
        private Vector2 location;
        internal double timeSinceLastFrameTransition = 0.0;
        public static double animateFrequency = 0.9 / 5.0;

        public BlockSprite(Texture2D texture, int rowsIn, int columnsIn, Vector2 l)
        {
            Texture = texture;
            this.rows = rowsIn;
            this.cols = columnsIn;
            currentFrame = 0;
            totalFrames = this.rows * this.cols;
            location = l;
        }
        public void Update(GameTime gameTime) {
            if(velocity == Vector2.Zero){
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
            else
            {
                Position += velocity;
                if (Position.Y != originalPosition.Y)
                {
                    
                        //Position += velocity;
                        if(originalPosition.Y >= Position.Y + 30)
                        {
                            velocity = new Vector2(0, 3);
                        }
                    
                }
                    if(Position.Y == originalPosition.Y)
                    {
                        velocity = Vector2.Zero;
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