using Microsoft.Xna.Framework;
using Sprint0.NPC.StateChange;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Collision.Logic
{
    public class EnemyAndEnemy : ICollisionLogic
    {
        private Collide collide;
        private CollisionDetection collisionDetection;
        private List<ISprite> aList;
        private List<ISprite> bList;
        public EnemyAndEnemy(List<ISprite> aIn, List<ISprite> bIn, Collide collideInstance)
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
                bool killed = false;

                foreach (ISprite b in bList)
                {
                    if (a.Equals(b))
                    {
                        break;
                    }
                    Rectangle RectangleEnemyA = collisionDetection.getRectangle(b);
                    Rectangle RectangleEnemyB = collisionDetection.getRectangle(a);
                    if (RectangleEnemyA.Intersects(RectangleEnemyB))
                    {
                        if (collisionDetection.touchBottom(RectangleEnemyA, RectangleEnemyB))
                        {
                            b.Position = new Vector2(b.Position.X, b.Position.Y - 10);
                        }
                        if (a.state == "Rolling")
                        {
                            EnemyChangeManager changeState = new EnemyChangeManager(b, collide.game);
                            changeState.attackedByFireball();
                            changeState.changeState();
                            killed = true;
                            break;
                        }
                        else
                        {
                            a.crash = true;
                            b.crash = true;
                        }
                    }
                }
                if (killed)
                {
                    break;
                }
            }
        }
    }
    
    
}
