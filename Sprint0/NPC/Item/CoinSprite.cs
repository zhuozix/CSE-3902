using Sprint0.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.NPC.Item
{
    internal class CoinSprite : NoneMovingAnimatedSprite
    {
        public CoinSprite(Texture2D texture, Vector2 position, int rowsIn, int colsIn) : base(texture, position, rowsIn, colsIn)
        {
            this.velocity = Vector2.Zero;  
        
        }
    }
}
