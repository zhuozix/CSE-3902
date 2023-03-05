using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0.Sprites
{
    public interface ISprite
    {
        string Name { get; set; }
        string state { get; set; }
        Vector2 Position { get; set; }
        Vector2 velocity { get; set; }
        Texture2D Texture { get; }
        int Width { get; }
        int Height { get; }

        bool crash { get; set; }

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, bool isFlipped);
    }
}


