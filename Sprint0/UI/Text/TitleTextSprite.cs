using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.UI.Text
{
    internal class TitleTextSprite : TextSprite
    {
        string instruction;
        Vector2 nextLine;
        public TitleTextSprite(SpriteFont fontIn, string textIn, Vector2 location, Color fontColor) : base(fontIn, textIn, location, fontColor)
        {
            this.text = "Super Mario Bros.";
            this.instruction = "Press { Enter } to play.";
            this.nextLine = new Vector2(location.X, location.Y + 100);
        }

        public override void Update(GameTime gameTime)
        {
           
        }

        public override void Draw(SpriteBatch spriteBatch, bool isFlipped)
        {
            spriteBatch.DrawString(this.font, this.text, this.Position, this.color);
            spriteBatch.DrawString(this.font, this.instruction, this.nextLine, this.color);
        }
    }
}
