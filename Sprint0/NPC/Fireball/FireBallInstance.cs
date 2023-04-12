using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Sprint0.NPC.Fireball
{
    public class FireBallInstance : NoneMovingAnimatedSprite
    {
        public float time;
        public float maxHeight;
        public FireBallInstance(Texture2D texture, Vector2 position, int rowsIn, int colsIn, int moveDirection) : base(texture, position, rowsIn, colsIn)
        {
            this.Position = position;
            if(moveDirection == 0)
            {
                //right
                this.velocity = new Vector2(300, 100);
            }
            else if (moveDirection == -1)
            {
                this.velocity = new Vector2(0, -100);
            }
            else
            {
                //left
                this.velocity = new Vector2(-300, 100);
            }

            this.crash = false;
            this.maxHeight = 0f;
        }

        private void bounce()
        {
            if(this.velocity.Y == 0)
            {
                this.maxHeight = this.position.Y - 20;
                this.velocity = new Vector2(this.velocity.X, -50);
            }
        }

        private void fall()
        {
            if(this.maxHeight != 0)
            {
                if(this.position.Y <= maxHeight)
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
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += (float)gameTime.ElapsedGameTime.TotalSeconds * velocity;
            base.Update(gameTime);

        }
    }
}
