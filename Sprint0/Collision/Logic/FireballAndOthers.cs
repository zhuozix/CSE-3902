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
    public class FireballAndOthers : ICollisionLogic
    {
        private Collide collide;
        private CollisionDetection collisionDetection;
        private List<ISprite> fireballList;
        private List<ISprite> marioList;
        private List<ISprite> enemyList;
        private List<ISprite> blockList;
        public FireballAndOthers(List<ISprite> fireballIn, List<ISprite> marioListIn, List<ISprite> enemyListIn, List<ISprite> blockListIn, Collide collideInstance)
        {
            fireballList = fireballIn;
            marioList = marioListIn;
            enemyList = enemyListIn;
            blockList = blockListIn;

            collide = collideInstance;
            collisionDetection = new CollisionDetection();
        }
        public void update(GameTime gameTime)
        {
            //Fireball and mario
            foreach (ISprite a in fireballList)
            {
                bool find = false;
                foreach (ISprite b in marioList)
                {
                    Rectangle RectangleA = collisionDetection.getRectangle(a);
                    Rectangle RectangleB = collisionDetection.getRectangle(b);
                    if (RectangleA.Intersects(RectangleB))
                    {
                        find = true;
                        FireballChangeManager changeFireBall = new FireballChangeManager(b, collide.game);
                        changeFireBall.changeState();
                        break;
                    }
                    if (find) { break; }
                }
            }

            //Fireball and enemy
            foreach (ISprite a in fireballList)
            {
                bool find = false;
                foreach (ISprite b in enemyList)
                {
                    Rectangle RectangleA = collisionDetection.getRectangle(a);
                    Rectangle RectangleB = collisionDetection.getRectangle(b);
                    if (RectangleA.Intersects(RectangleB))
                    {
                        find = true;
                        EnemyChangeManager changeState = new EnemyChangeManager(b, collide.game);
                        changeState.attackedByFireball();
                        changeState.changeState();
                        FireballChangeManager changeFireBall = new FireballChangeManager(b, collide.game);
                        changeFireBall.changeState();
                        break;
                    }
                    if (find) { break; }
                }
            }

            //Fireball and block
            foreach (ISprite a in fireballList)
            {

                foreach (ISprite b in blockList)
                {

                    Rectangle RectangleBlock = collisionDetection.getRectangle(b);
                    Rectangle RectangleItem = collisionDetection.getRectangle(a);
                    if (RectangleBlock.Intersects(RectangleItem))
                    {

                        if (collisionDetection.touchBottom(RectangleItem, RectangleBlock))
                        {
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
            }
        }


    }
}
