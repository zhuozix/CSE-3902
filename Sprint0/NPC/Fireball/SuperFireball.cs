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
    public class SuperFireball : Fireball
    {
        public float maxHeight;
        public SuperFireball(Texture2D texture, Vector2 position, int rowsIn, int colsIn, int moveDirection) : base(texture, position, rowsIn, colsIn)
        {
            this.Position = position;
            if (moveDirection == 0)
            {
                //right
                this.velocity = new Vector2(300, 0);
            }
            else if (moveDirection == -1)
            {
                this.velocity = new Vector2(0, 0);
            }
            else
            {
                //left
                this.velocity = new Vector2(-300, 0);
            }

            this.crash = false;
            this.maxHeight = 0f;
            //this.mode = false;
        }


        public override void Update(GameTime gameTime)
        {
            if (crash)
            {
                velocity = new Vector2(velocity.X * -1, velocity.Y);
                crash = false;
            }

            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += (float)gameTime.ElapsedGameTime.TotalSeconds * velocity;
            base.Update(gameTime);

        }
    }
}
