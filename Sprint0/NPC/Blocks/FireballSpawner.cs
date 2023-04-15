using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.NPC.Fireball;
using Microsoft.Xna.Framework;
using Sprint0;
using Sprint0.Sprites;
using Sprint0.Content;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.NPC.Blocks
{
    public class FireballSpawner
    {
        public FireBallInstance fireBall;

        public Vector2 Position { get; set; }

        private Random randomizer;

        private Game1 game;

        public FireballSpawner(int xPos, int yPos, Game1 game1)
        {
            this.Position = new Vector2(xPos, yPos);
            this.fireBall = null;
            this.randomizer = new Random();
            this.game = game1;
        }

        public void Update(GameTime gametime)
        {

            if (fireBall == null && this.randomizer.Next(40) == 1)
            {
                generateFireball();
            }
            else if (this.fireBall != null)
            {
                this.fireBall.Update(gametime);
                if (this.fireBall.Position.Y > 480)
                {
                    this.fireBall = null;
                }

            }

        }

        public void generateFireball()
        {
            this.fireBall = (FireBallInstance)this.game.spritesFactory.getFireballSprite(this.Position, false);
            this.fireBall.velocity = new Vector2(0, -100);
            this.fireBall.maxHeight = 180;
        }

        public void Draw(SpriteBatch spritebatch, Boolean isflipped)
        {
            if (this.fireBall != null)
            {
                this.fireBall.Draw(spritebatch, isflipped);
            }

        }
    }
}
