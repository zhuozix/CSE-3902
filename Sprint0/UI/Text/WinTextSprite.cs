using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.UI.Text
{
    internal class WinTextSprite: TextSprite
    {
        string instruction;
        Vector2 nextLine;
        public WinTextSprite(SpriteFont fontIn, string textIn, Vector2 location, Color fontColor) : base(fontIn, textIn, location, fontColor)
        {
            this.text = "You win the game!";
            this.instruction = "Press { R } to ReStart, Press any other key to exit.";
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
