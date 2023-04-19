using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Factory;
using Sprint0.MarioPlayer;
using Sprint0.Sounds;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                ReminCalculator(gameTime);
                this.text = this.title + this.LifeRemin;
            }
            else {this.text = this.title + noDmg;}
        }

        private void ReminCalculator(GameTime gameTime)
        {
            Mario mario = gameInstance.mario;
            SpritesFactory spritesFactory = gameInstance.spritesFactory;
            while (hp > 0)
            {
                this.LifeRemin += '|';
                hp = hp - 10;
                /*
                float timer = 0f;
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer >= 5f)
                {
                    ISprite star = spritesFactory.getStarSprite();
                    star.Position = new Vector2(mario.Position.X, mario.Position.Y - 110);
                    gameInstance.gameObjectManager.addObject(star, "item");
                    timer = 0f;
                }
                */
            }
           
        }

        public override void Draw(SpriteBatch spriteBatch, bool isFlipped)
        {
            spriteBatch.DrawString(this.font, this.text, this.Position, this.color);
        }
    }
}
