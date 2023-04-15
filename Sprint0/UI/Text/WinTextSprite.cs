using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Sprites;
using Microsoft.Xna.Framework.Content;

namespace Sprint0.UI.Text
{
    internal class WinTextSprite: TextSprite
    {
        string instruction;
        Vector2 nextLine;
        Game1 gameInstance;
        public WinTextSprite(Game1 gameInstance,SpriteFont fontIn, string textIn, Vector2 location, Color fontColor) : base(fontIn, textIn, location, fontColor)
        {
            this.text = "You win the game!";
            this.instruction = "Press { R } to ReStart, Press any other key to exit.";
            this.nextLine = new Vector2(location.X, location.Y + 100);
            this.gameInstance = gameInstance;
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch, bool isFlipped)
        {
            ContentManager content = gameInstance.Content;
            Texture2D winpage = content.Load<Texture2D>("mariowinpage");
            spriteBatch.Draw(winpage, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(this.font, this.text, this.Position, this.color);
            spriteBatch.DrawString(this.font, this.instruction, this.nextLine, this.color);
        }
    }
}
