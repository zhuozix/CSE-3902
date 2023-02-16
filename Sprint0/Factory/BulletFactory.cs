using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Item;
using Sprint0.Sprites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Factory
{
    public class BulletFactory
    {
        private GraphicsDeviceManager _graphics;

        public Texture2D texture_FireBall;

        float timeSpent = 0f;

        public BulletFactory(GraphicsDeviceManager graphicsIn)
        {
            _graphics = graphicsIn;
        }

        public void initalize(ContentManager content)
        {
            texture_FireBall = content.Load<Texture2D>("FireMario/fireball");
        }
        public ISprite getFireballSprite(Vector2 currentLocation, bool isFacingRight)
        {
            if (isFacingRight)
            {
                Vector2 newLocation = new Vector2(currentLocation.X + 5,currentLocation.Y);
                return new FireBallSprite(texture_FireBall, newLocation, 2, 2, _graphics, 1,true);
            }
            else
            {
                Vector2 newLocation = new Vector2(currentLocation.X - 5, currentLocation.Y);
                return new FireBallSprite(texture_FireBall, newLocation, 2, 2, _graphics, -1, false);
            }
            
        }

        public void update(ArrayList bulletList,GameTime gameTimeIn)
        {
            if(bulletList.Count != 0)
            {
                timeSpent += (float)gameTimeIn.ElapsedGameTime.TotalSeconds;
                if(timeSpent >= 2f)
                {
                    timeSpent= 0f;
                    bulletList.RemoveAt(0);
                }
            }
            else 
            {
                timeSpent= 0f;
            }
        }
    }
}
