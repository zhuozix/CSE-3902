using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Collision.Logic
{
    public class ActivateEnemy : ICollisionLogic
    {
        private Collide collide;
        private List<ISprite> marioList;
        private List<ISprite> enemyList;
        private CollisionDetection collisionDetection;
        public ActivateEnemy(List<ISprite> marioListIn, List<ISprite> enemyListIn, Collide collideIn )
        {
            collide = collideIn;
            marioList = marioListIn;
            enemyList = enemyListIn;
            collisionDetection = new CollisionDetection();
        }

        public void update(GameTime gameTime)
        {
            foreach(Mario mario in marioList)
            {
                foreach(ISprite enemy in enemyList)
                {
                    if(Math.Abs(mario.Position.X - enemy.Position.X) <= 800)
                    {
                        activate(enemy);
                    }
                }
            }
        }

        public void activate(ISprite enemy)
        {
            if (enemy.state == "out")
            {
                enemy.state = "Normal";
            }
        }
    }
}
