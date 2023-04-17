using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.UI.Text.BossFightText
{
    internal class MachineGunText: TextSprite
    {
        private string haveGun;
        private string EmptyStr;
        private Game1 gameInstance;
        public MachineGunText(SpriteFont fontIn, string textIn, Vector2 location, Color fontColor, Game1 gameInstanceIn) : base(fontIn, textIn, location, fontColor)
        {
            this.haveGun = "MGun";
            this.EmptyStr = "";
            this.gameInstance = gameInstanceIn;
            this.text = this.EmptyStr;
        }

        public override void Update(GameTime gameTime)
        {
            bool isGun = this.gameInstance.mario.mode;
            if (!isGun)
            {
                this.text = haveGun;
            }
            else
            {
                this.text = EmptyStr;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, bool isFlipped)
        {
            spriteBatch.DrawString(this.font, this.text, this.Position, this.color);
        }
    }
}
