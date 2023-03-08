using Microsoft.Xna.Framework;
using Sprint0.Factory;
using Sprint0.MarioPlayer;
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

            else if (currentEnemy.state == "Normal")
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
                        pushMario();
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
                        futureSprite.state = "Normal";
                        objManager.addObject(futureSprite, "enemy");
                        pushMario();
                        break;
                    }
                }

            }

        }

        private void pushMario()
        {
            Mario player = game.mario;
            float xVelocity = player.velocity.X;
            float yVelocity = player.velocity.Y;
            if (xVelocity > 0 && yVelocity == 0)
            {
                game.mario.Position = new Vector2(player.Position.X - 50, player.Position.Y);
            }
            else if (xVelocity < 0 && yVelocity == 0)
            {
                game.mario.Position = new Vector2(player.Position.X + 50, player.Position.Y);
            }
            else
            {
                game.mario.Position = new Vector2(player.Position.X, player.Position.Y - 10);
                game.mario.velocity = new Vector2(player.velocity.X, -350);
                player.Jump();
            }
            
        }
    }
}

