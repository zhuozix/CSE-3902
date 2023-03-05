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
        Vector2 Position { get; set; }

        Texture2D Texture { get; }
        int Width { get; }
        int Height { get; }

        public bool collide { get; set; }
        public bool collideA { get; set; }

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, bool isFlipped);
    }
}


