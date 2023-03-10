using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.NPC.Enemy
{

        internal class EnemySprite : NoneMovingAnimatedSprite
        {

            public EnemySprite(Texture2D texture, Vector2 position, int rowsIn, int colsIn) : base(texture, position, rowsIn, colsIn)
            {
                this.Position = position;
                this.velocity = new Vector2(-100, 100);
                this.crash = false;

            }

            public override void Update(GameTime gameTime)
            {
            if (this.state == "Dead" || this.state == "idle" || this.state == "out")
            {
                velocity = new Vector2(0, velocity.Y);
            }
            else if (this.state == "Rolling")
            {
                if (velocity.X < 0)
                {
                    velocity = new Vector2(-300, velocity.Y);
                }
                else
                {
                    velocity = new Vector2(300, velocity.Y);
                }
            }
            else
            {
                if (velocity.X < 0)
                {
                    velocity = new Vector2(-100, velocity.Y);
                }
                else
                {
                    velocity = new Vector2(100, velocity.Y);
                }
            }
            
            

            if (crash)
                {
                    velocity = new Vector2(velocity.X * -1, velocity.Y);
                    crash = false;
                }

                if(this.position.X <= 0)
                {
                velocity = new Vector2(velocity.X * -1, velocity.Y);
                }

                Position += (float)gameTime.ElapsedGameTime.TotalSeconds * velocity;
                base.Update(gameTime);

            }
        }

}
