using Microsoft.Xna.Framework;
using Sprint0.Factory;
using Sprint0.MarioPlayer;
using Sprint0.NPC.Blocks;
using Sprint0.ObjectManager;
using Sprint0.Sounds;
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
        Game1 game;
        public EnemyChangeManager(ISprite enemyIn, Game1 gameInstance) 
        {
            currentEnemy= enemyIn;
            factory = gameInstance.spritesFactory;
            objManager = gameInstance.gameObjectManager;
            game = gameInstance;

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
                case "KoopaShell":
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
                        futureSprite.state = "Dead";
                        objManager.addObject(futureSprite, "enemy");
                        SoundPlayer.playStomp();
                        }
                        objManager.enemies.Remove(target);
                        break;
                    }
                }
            
        }

        private void koopaTransition()
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

            else if (currentEnemy.state != "idle")
            {
                foreach (ISprite target in objManager.enemies)
                {
                    if (currentEnemy.Equals(target))
                    {
                        objManager.enemies.Remove(target);
                        ISprite futureSprite = factory.getKoopaShellSprite();
                        futureSprite.Position = currentEnemy.Position;
                        futureSprite.state = "idle";
                        objManager.addObject(futureSprite, "enemy");
                        SoundPlayer.playStomp();
                        break;
                    }
                }
            }
            else
            {

                foreach (ISprite target in objManager.enemies)
                {
                    if (currentEnemy.Equals(target))
                    {
                        objManager.enemies.Remove(target);
                        ISprite futureSprite = factory.getKoopaShellSprite();
                        futureSprite.Position = currentEnemy.Position;
                        futureSprite.state = "Rolling";
                        if(game.mario.IsFacingRight)
                        {
                            futureSprite.crash = true;
                        }
                        objManager.addObject(futureSprite, "enemy");
                        SoundPlayer.playStomp();
                        break;
                    }
                }

            }

        }

    }
}

