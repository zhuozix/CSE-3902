using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.NPC.Fireball
{
    public class Fireball : NoneMovingAnimatedSprite
    {
        public float time = 0;
        //public bool mode = true;

        public Fireball(Texture2D texture, Vector2 position, int rowsIn, int colsIn, int moveDirection)
            : base(texture, position, rowsIn, colsIn)
        {
        }

        public Fireball(Texture2D texture, Vector2 position, int rowsIn, int colsIn) : base(texture, position, rowsIn, colsIn)
        {
        }

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
