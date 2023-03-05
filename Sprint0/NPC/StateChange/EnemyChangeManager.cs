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
        
        public EnemyChangeManager(ISprite enemyIn, SpritesFactory factoryIn, GameObjectManager objIn) 
        {
            currentEnemy= enemyIn;
            factory = factoryIn;
            objManager = objIn;
        }
        public void attackedByFireball()
        {
            touchedByFireball = true;
        }
        public void changeState()
        {

        }

        private void findNextState()
        {
            switch (currentEnemy.Name)
            {
                case "Koopa":
                    koopaStateChange();break;
                case "Gommba":
                    gommbaStateChange();break;
                default: break;
            }
        }

        private void gommbaStateChange()
        {
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

            else if (currentEnemy.state == "Normal")
            {
                currentEnemy.state = "Dead";
                currentEnemy.velocity = new Vector2(0, 0);
            }
        }

        private void koopaStateChange()
        {
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
