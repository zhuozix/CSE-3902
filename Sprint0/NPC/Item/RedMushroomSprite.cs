using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.MarioPlayer;
using Sprint0.Sprites;

namespace Sprint0.NPC.Item
{
    internal class RedMushroomSprite : NoneMovingAnimatedSprite
    {

        public RedMushroomSprite(Texture2D texture, Vector2 position, int rowsIn, int colsIn, int moveDirection) : base(texture, position, rowsIn, colsIn)
        {
            this.Position = position;
            this.velocity = new Vector2(80, 100);
            this.crash = false;
            
        }
        
        public override void Update(GameTime gameTime)
        {
            if(crash)
            {
                velocity = new Vector2(velocity.X * -1,velocity.Y);
                crash = false;
            }
            Position += (float)gameTime.ElapsedGameTime.TotalSeconds * velocity;
            base.Update(gameTime);

        } 
    }
}
