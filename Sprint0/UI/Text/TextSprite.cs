using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Content;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.UI.Text
{
    public abstract class TextSprite: ISprite
    {
        public string Name { get; set; }
        public string state { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 velocity { get; set; }
        public Texture2D Texture { get; }
        public int Width { get; }
        public int Height { get; }
        public int rows { get; set; }
        public int cols { get; set; }
        public bool crash { get; set; }

        public SpriteFont font { get; set; }
        public string text { get; set; }
        public Color color { get; set; }
        
        public TextSprite(SpriteFont fontIn, string textIn, Vector2 location, Color fontColor)
        {
            this.font = fontIn;
            this.text = textIn;
            this.Position = location;
            this.color = fontColor;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch, bool isFlipped);

    }
}
