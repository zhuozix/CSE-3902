﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Sprint0.Sprites;

namespace Sprint0.Blocks
{
    internal class AnimatedBlockSprite : ISprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public Vector2 Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Width => throw new NotImplementedException();

        public int Height => throw new NotImplementedException();

        private int currentFrame;
        private int totalFrames;
        private Vector2 location;
		internal double timeSinceLastFrameTransition = 0.0;
		public static double animateFrequency = 0.9 / 5.0;

		public AnimatedBlockSprite(Texture2D texture, int rows, int columns, Vector2 l)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            location = l;
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
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X - width, (int)location.Y - height, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}

