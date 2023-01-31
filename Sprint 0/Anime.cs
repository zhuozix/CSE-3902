using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint_0
{
    public class Anime
    {
        private int frame = 0;
        private float elapsed;
        private float delay = 200f;
        public Anime() { }

        public int GetFrame(GameTime gameTime)                          //make the luigi walking animation
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                if (frame >= 1)
                {
                    frame = 0;
                }
                else
                {
                    frame++;
                }
                elapsed = 0;
            }
            return frame;
        }
    }
}
