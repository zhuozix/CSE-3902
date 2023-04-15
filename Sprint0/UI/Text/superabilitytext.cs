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
    internal class superabilitytext: TextSprite
    {
        private string timetext;
        private Game1 gameInstance;
        public superabilitytext(SpriteFont fontIn, string textIn, Vector2 location, Color fontColor, Game1 gameInstanceIn) : base(fontIn, textIn, location, fontColor)
        {
            this.text = "";
            this.timetext = "";
            this.gameInstance = gameInstanceIn;
        }

        public override void Update(GameTime gameTime)
        {
            int timeInt = (int)this.gameInstance.time;
            this.text = this.timetext + timeInt.ToString();
        }

        public override void Draw(SpriteBatch spriteBatch, bool isFlipped)
        {
           
            spriteBatch.DrawString(this.font, this.text, this.Position, this.color);
        }

    }
}
