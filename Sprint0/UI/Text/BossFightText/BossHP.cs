using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.UI.Text
{
    internal class BossHP :TextSprite
    {
        private string title;
        private string LifeRemin;
        private Game1 gameInstance;
        private int hp;
        private string noDmg;
        public BossHP(SpriteFont fontIn, string textIn, Vector2 location, Color fontColor, Game1 gameInstanceIn) : base(fontIn, textIn, location, fontColor)
        {
            this.text = "";
            this.title = this.text;
            this.LifeRemin = "";
            this.gameInstance = gameInstanceIn;
            noDmg = "invincible";
            hp = 100;
        }

        public override void Update(GameTime gameTime)
        {
            if (!gameInstance.bowser._ai.angryMode)
            {
                this.title = "King Koopa: ";
            }
            else
            {
                this.title = "!!!ANGRY!!!: ";
            }
            if (!gameInstance.bowser._ai.noDmgLock) { 
                this.LifeRemin = "";
                this.hp = gameInstance.bossHP;
                ReminCalculator();
                this.text = this.title + this.LifeRemin;
            }
            else {this.text = this.title + noDmg;}
        }

        private void ReminCalculator()
        {       
            while (hp > 0)
            {
                this.LifeRemin += '|';
                hp = hp - 10;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, bool isFlipped)
        {
            spriteBatch.DrawString(this.font, this.text, this.Position, this.color);
        }
    }
}
