using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    internal class GreenMushroomSprite : NoneMovingAnimatedSprite
    {
        public GreenMushroomSprite(Texture2D texture, Vector2 position, int rows, int cols)
            : base(texture, position, rows, cols)
        { }
    }
}
