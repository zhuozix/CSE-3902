using Sprint0.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0
{
    internal class CoinSprite : NoneMovingAnimatedSprite
    {
        public CoinSprite(Texture2D texture, Vector2 position, int rows, int cols)
            : base(texture, position, rows, cols)
        { }
    }
}
