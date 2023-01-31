using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint_0
{
    internal interface ISprite
    {
        void Update(Game1 game, GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
