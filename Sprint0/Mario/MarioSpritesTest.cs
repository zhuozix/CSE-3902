using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Sprites;
using Sprint0.Content;

namespace Sprint0.Mario
{
    internal class MarioSpritesTest : NoneAnimatedNonMovingSprite
    {
        public MarioSpritesTest(Texture2D texture, Vector2 position, int rows, int cols)
            : base(texture, position, rows, cols)
        { }
    }
}
