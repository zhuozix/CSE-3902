using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Sprites;

namespace Sprint0.Collision
{
    public interface ICollision
    {
        public Rectangle Rectangle { get; set; }
        public Vector2 Velocity { get; set; }
        public bool collideblock { get; set; }
        public bool fall { get; set; }

        public abstract void Update(GameTime gameTime);

        public bool TouchLeft(ISprite collide);

        public bool TouchRight(ISprite collide);

        public bool TouchTop(ISprite collide);

        public bool TouchBottom(ISprite collide);
    }
}
