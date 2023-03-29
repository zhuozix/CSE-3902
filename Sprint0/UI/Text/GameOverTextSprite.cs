using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Sprites;

namespace Sprint0.UI.Text
{
    internal class GameOverTextSprite:TextSprite
    {
        string instruction;
        Vector2 nextLine;
        public GameOverTextSprite(SpriteFont fontIn, string textIn, Vector2 location, Color fontColor) : base(fontIn, textIn, location, fontColor)
        {
            this.text = "GameOver!";
            this.instruction = "Press { R } to Replay, Press any other key to exit.";
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
