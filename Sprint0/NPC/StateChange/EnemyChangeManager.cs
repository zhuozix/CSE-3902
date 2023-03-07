﻿using Microsoft.Xna.Framework;
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

            else if (currentEnemy.state == "Normal")
            {
                currentEnemy.state = "Dead";

                currentEnemy.velocity = new Vector2(0, 0);
                objManager.addObject(futureSprite, "enemies");
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
