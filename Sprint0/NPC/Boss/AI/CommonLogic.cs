using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer;
using Sprint0.NPC.Blocks;
using Sprint0.Sounds;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.NPC.Boss.AI
{
    public class CommonLogic
    {
        public Mario player;
        public Boss boss;
        public BossAI ai;
        public Game1 game;
        Boolean stomping = false;
        public CommonLogic(Mario player, Boss boss, BossAI aiIn, Game1 gameIn) 
        {
            this.player = player;
            this.boss = boss;
            this.ai = aiIn;
            game = gameIn;
        }

        public bool playerOnRight() 
        {
            return (player.Position.X > boss.Position.X);
            
        }

        public bool marioOnGround()
        {
            if(player.Position.Y > 290)
            {
                return true;
            }
            return false;
        }
        public int findPlayerCurrentLevel() {
            int level = 0;
            if (player.Position.Y < 100) level = 3;
            else if (player.Position.Y < 200) level = 2;
            else if (player.Position.Y < 300) level = 1;
            return level;
        }

        public bool marioNearBowser()
        {
            return Math.Abs(boss.Position.X - player.Position.X) < 150;
        }

        public void approchToPlayer()
        {
            if (playerOnRight())
            {
                ai.stateChange.runnungRight();
            }
            else
            {
                ai.stateChange.runnungLeft();
            }
        }

        public void jumpTo(int yPosition)
        {
            if(boss.Position.Y > yPosition)
            {
                ai.stateChange.jump();
            }
            else
            {
                fireballAttack();
                falling();
            }
        }


        public void falling()
        {
            if (boss.Position.Y < 350)
            {
                ai.stateChange.fall();
            }
            else
            {
                BlockSprite smashed = null;
                int remainder = (int)boss.Position.X % 32;
                int Xpos = (int)boss.Position.X - remainder + 32;
                Vector2 target = new Vector2(Xpos, 416);
                foreach (BlockSprite b in game.gameObjectManager.blocks)
                {
                    if (b.Position == target)
                    {
                        smashed = b;
                    }
                }
                if (smashed != null && stomping == true){
                    game.gameObjectManager.blocks.Remove(smashed);
                    smashed = null;
                }
                stomping = false;
                ai.stateChange.stopMoving();
                ai.restTimerLock = true;
            }
        }

        public void jumpAttack()
        {
            if(boss.currentActionType != BossActionType.Falling)
            {
                if(boss.Position.Y > 250)
                {
                    ai.stateChange.jump();
                    stomping = true;
                    if (boss.Position.X != player.Position.X)
                    {
                        if (playerOnRight())
                        {
                            boss.isFacingRight = true;
                            boss.velocity = new Vector2(200, boss.velocity.Y);
                        }
                        else
                        {
                            boss.isFacingRight = false;
                            boss.velocity = new Vector2(-200, boss.velocity.Y);
                        }
                    }
                    else
                    {
                        boss.velocity = new Vector2(0,boss.velocity.Y);
                    }
                }
                else
                {
                    boss.velocity = Vector2.Zero;
                    ai.restTimer = 0.5f;
                    falling();
                }

            }
            else
            {
                falling();
            }

        }

        public bool canPerformAction()
        {
            if (ai.hitAndCannotMove)
            {
                return false;
            }
            return true;
        }

        public void hitByMario()
        {
            ai.hitAndCannotMove = true;
            ai.angry += 15;
            ai.stateChange.stopMoving();
        }

        public void hitByFireball()
        {
            ai.angry += 12;
        }

        public void resetAngry()
        {
            ai.angry = 0;
        }

        public void summonGommba()
        {
            ISprite newEnemy = game.spritesFactory.getGommbaSprite();
            newEnemy.velocity = new Vector2(newEnemy.velocity.X, 100);
            if (playerOnRight())
            {
                newEnemy.Position = new Vector2(boss.Position.X + 10, boss.Position.Y - 10);
                newEnemy.crash = true;
            }
            else
            {
                newEnemy.Position = new Vector2(boss.Position.X - 10, boss.Position.Y - 10);
            }
            game.gameObjectManager.addObject(newEnemy,"enemy");
        }

        public void fireballAttack()
        {
            ISprite fireball = game.spritesFactory.getFireballSprite(boss.Position, false, false);
            if (boss.isFacingRight)
            {
                fireball.crash = true;
            }
            game.gameObjectManager.addObject(fireball, "superFireball");
            SoundPlayer.playBowserFire();
        }

        public bool haveGommbaNearBy()
        {
            foreach(ISprite gommba in game.gameObjectManager.enemies)
            {
                if(Math.Abs(gommba.Position.X - boss.Position.X) <= 100)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
