using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.UI.Text
{
    internal class DeathTextSprite : TextSprite
    {
        private string lifetext;
        private Game1 gameInstance;
        public DeathTextSprite(SpriteFont fontIn, string textIn, Vector2 location, Color fontColor, Game1 gameInstanceIn) : base(fontIn, textIn, location, fontColor)
        {
            this.text = "Dead";
            this.lifetext = "Mario X ";
            this.gameInstance = gameInstanceIn;
        }

        public override void Update(GameTime gameTime)
        {
            this.lifetext = "Mario X " + this.gameInstance.life.ToString();
        }

        public override void Draw(SpriteBatch spriteBatch, bool isFlipped)
        {
            spriteBatch.DrawString(this.font, this.text, this.Position, this.color);
            spriteBatch.DrawString(this.font, this.lifetext, this.Position + new Vector2(0,50), this.color);
        }
    }
}

