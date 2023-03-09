using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Sprint0.Content
{
    public class NoneMovingAnimatedSprite : Sprite
    {
        public NoneMovingAnimatedSprite(Texture2D texture, Vector2 position, int rowsIn, int colsIn)
            : base(texture, position, rowsIn, colsIn)
        { }

        public override void Update(GameTime gameTime)
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
    }
}
