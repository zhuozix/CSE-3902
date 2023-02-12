using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Enemy;

namespace Sprint0.Enemy
{

    public class GommbaSprite : MovingEnemy
    {
        public GommbaSprite(Texture2D texture, Vector2 position, int rows, int cols, GraphicsDeviceManager graphics)
            : base(texture, position, rows, cols, graphics)
        { }

    }
}