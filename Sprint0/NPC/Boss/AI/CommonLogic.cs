using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer;
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
        public CommonLogic(Mario player, Boss boss, BossAI aiIn, Game1 gameIn) 
        {
            this.player = player;
            this.boss = boss;
            this.ai = aiIn;
            game = gameIn;
        }


        public bool marioOnGround()
        {
            if(player.Position.Y > 280)
            {
                return true;
            }
            return false;
        }
        public int findPlayerCurrentLevel() {
            int level = 0;
            if (player.Position.Y < 100) level = 3;
            else if (player.Position.Y < 200) level = 2;
            else if (player.Position.Y < 280) level = 1;
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
            if (boss.Position.Y < 355)
            {
                ai.stateChange.fall();
            }
            else
            {
                
                ai.stateChange.stopMoving();
                ai.restTimerLock = true;
            }
        }

        public void jumpAttack()
        {
            if (boss.currentActionType == BossActionType.Falling)
                return;

            if (boss.Position.Y <= 280f)
            {
                boss.velocity = Vector2.Zero;
                ai.restTimer = 0.5f;
                falling();
                return;
            }

            ai.stateChange.jump();
            SetBossVelocity();

        }

        private void SetBossVelocity()
        {
            if (boss.Position.X != player.Position.X)
            {
                boss.isFacingRight = playerOnRight();
                float velocityX = boss.isFacingRight ? 150f : -150f;
                boss.velocity = new Vector2(velocityX, boss.velocity.Y);
            }
            else
            {
                boss.velocity = new Vector2(0, boss.velocity.Y);
            }
        }

        private bool playerOnRight()
        {
            return player.Position.X > boss.Position.X;
        }

        public bool canPerformAction()
        {
            return !ai.hitAndCannotMove;
        }

        public void hitByMario()
        {
            ai.hitAndCannotMove = true;
            ai.angry += 10;
            ai.stateChange.stopMoving();
        }

        public void hitByFireball()
        {
            ai.angry += 2;
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
            game.gameObjectManager.addObject(game.spritesFactory.getFireballSprite(boss.Position, false, false), "superFireball");
        }

    }
}
