﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.NPC.Item
{
    internal class StarSprite : NoneMovingAnimatedSprite
    {
        float maxHeight = 0f;
        public StarSprite(Texture2D texture, Vector2 position, int rowsIn, int colsIn) : base(texture, position, rowsIn, colsIn)
        {
            this.Position = position;
            this.velocity = new Vector2(80, 100);
            this.crash = false;

        }

        private void bounce()
        {
            if (this.velocity.Y == 0)
            {
                this.maxHeight = this.position.Y - 20;
                this.velocity = new Vector2(this.velocity.X, -50);
            }
        }

        private void fall()
        {
            if (this.maxHeight != 0)
            {
                if (this.position.Y <= maxHeight)
                {
                    this.velocity = new Vector2(this.velocity.X, 50);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (crash)
            {
                velocity = new Vector2(velocity.X * -1, velocity.Y);
                crash = false;
            }

            bounce();
            fall();
            Position += (float)gameTime.ElapsedGameTime.TotalSeconds * velocity;
            base.Update(gameTime);

        }

    }
}