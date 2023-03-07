using Microsoft.Xna.Framework;
using Sprint0.Factory;
using Sprint0.NPC.Blocks;
using Sprint0.ObjectManager;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.NPC.StateChange
{
    class EnemyChangeManager : IStateChangeManager
    {
        public ISprite currentEnemy;
        SpritesFactory factory;
        GameObjectManager objManager;
        bool touchedByFireball = false;
        
        public EnemyChangeManager(ISprite enemyIn, Game1 gameInstance) 
        {
            currentEnemy= enemyIn;
            factory = gameInstance.spritesFactory;
            objManager = gameInstance.gameObjectManager;
        }
        public void attackedByFireball()
        {
            touchedByFireball = true;
        }
        public void changeState()
        {
            enemyTransition();
        }

        private void enemyTransition()
        {
            switch (currentEnemy.Name)
            {
                case "Koopa":
                    koopaTransition();break;
                case "Gommba":
                    gommbaTransition();break;
                default: break;
            }
        }

        private void gommbaTransition()
        {
                foreach (ISprite target in objManager.enemies)
                {
                    if (currentEnemy.Equals(target))
                    {
                        if (!touchedByFireball)
                        {
                        ISprite futureSprite = factory.getDeadGommbaSprite();
                        futureSprite.Position = currentEnemy.Position;
                        futureSprite.velocity = Vector2.Zero;
                        futureSprite.state = "Dead";
                        objManager.addObject(futureSprite, "enemy");

                        }
                        objManager.enemies.Remove(target);
                        break;
                    }
                }
            
        }

        private void koopaTransition()
        {
            ISprite futureSprite = factory.getKoopaShellSprite();
            futureSprite.Position = currentEnemy.Position;
            if (touchedByFireball)
            {
                foreach (ISprite target in objManager.enemies)
                {
                    if (currentEnemy.Equals(target))
                    {
                        objManager.enemies.Remove(target);
                        break;
                    }
                }
                
            }

            if (currentEnemy.state == "Normal")
            {
                currentEnemy.state = "idle";
                objManager.addObject(futureSprite, "enemies");
            } 
            else if(currentEnemy.state == "idle")
            {
               
            }
            else if(currentEnemy.state == "rolling")
            {

            }
        }
    }
}
