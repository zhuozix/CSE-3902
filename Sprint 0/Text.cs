using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint_0
{
    public class Text : ISprite

    {
        String text;
        Vector2 position;
        SpriteFont font;
        public Text(String text, Vector2 position, SpriteFont font)
        { 
            this.text = text;                                             //text print on screen
            this.position = position;                                    //the position of text on screen
            this.font = font;                                           //the font of the text
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, Color.Black);
        }

        public void Update(Game1 game, GameTime gameTime)
        {
            // No Upadate Needed
        }
    }
}
