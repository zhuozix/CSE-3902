using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.UI.Text
{
    internal class LifeTextSprite : TextSprite
    {
        private string lifetext;
        private Game1 gameInstance;
        public LifeTextSprite(SpriteFont fontIn, string textIn, Vector2 location, Color fontColor, Game1 gameInstanceIn) : base(fontIn, textIn, location, fontColor)
        {
            this.text = "LifeText";
            this.lifetext = "Life: ";
            this.gameInstance = gameInstanceIn;
        }

        public override void Update(GameTime gameTime)
        {
            this.text = this.lifetext + this.gameInstance.life.ToString();
        }

        public override void Draw(SpriteBatch spriteBatch, bool isFlipped)
        {
            spriteBatch.DrawString(this.font, this.text, this.Position, this.color);
        }
    }
}
