using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.UI.Text
{
    internal class CoinTextSprite :TextSprite
    {
        private string Cointext;
        private Game1 gameInstance;
        public CoinTextSprite(SpriteFont fontIn, string textIn, Vector2 location, Color fontColor, Game1 gameInstanceIn) : base(fontIn, textIn, location, fontColor)
        {
            this.text = "CoinsText";
            this.Cointext = "Coins: ";
            this.gameInstance = gameInstanceIn;
        }

         public override void Update(GameTime gameTime)
        {
            this.text = this.Cointext + this.gameInstance.coins.ToString();
        }

        public override void Draw(SpriteBatch spriteBatch, bool isFlipped)
        {
            spriteBatch.DrawString(this.font, this.text, this.Position, this.color);
        }
    }
}
