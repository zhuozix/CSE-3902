using Microsoft.Xna.Framework;
using Sprint0.NPC.Enemy;
using Sprint0.NPC.StateChange;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Collision.Logic
{
    public class ItemAndBlock : ICollisionLogic
    {
        private Collide collide;
        private CollisionDetection collisionDetection;
        private List<ISprite> aList;
        private List<ISprite> bList;
        public ItemAndBlock(List<ISprite> aIn, List<ISprite> bIn, Collide collideInstance)
        {
            aList = aIn;
            bList = bIn;
            collide = collideInstance;
            collisionDetection = new CollisionDetection();
        }
        public void update(GameTime gameTime)
        {
            foreach (ISprite a in aList)
            {
                bool touched = false;
                foreach (ISprite b in bList)
                {

                    Rectangle RectangleBlock = collisionDetection.getRectangle(b);
                    Rectangle RectangleItem = collisionDetection.getRectangle(a);
                    if (RectangleBlock.Intersects(RectangleItem))
                    {
                        //block hit item
                        if (b.velocity.X < 0)
                        {
                            a.velocity = new Vector2(b.velocity.X, a.velocity.Y);
                            break;
                        }

                        //item hit block
                        touched = true;
                        if (collisionDetection.touchBottom(RectangleItem, RectangleBlock))
                        {
                            int intersect = RectangleItem.Bottom - RectangleBlock.Top;
                            if (intersect >= 2) a.Position = new Vector2(a.Position.X, a.Position.Y - 2);
                            a.velocity = new Vector2(a.velocity.X, 0);
                        }

                        if (collisionDetection.touchRight(RectangleItem, RectangleBlock))
                        {
                            a.crash = true;
                        }
                        else if (collisionDetection.touchLeft(RectangleItem, RectangleBlock, a))
                        {
                            a.crash = true;
                        }


                    }
                }
                if (!touched)
                {
                    a.velocity = new Vector2(a.velocity.X, 100);
                }
            }
        }


    }
}
