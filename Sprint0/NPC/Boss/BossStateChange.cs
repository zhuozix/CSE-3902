using Microsoft.Xna.Framework;
using Sprint0.NPC.Boss.AI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.NPC.Boss
{
    public class BossStateChange
    {
        public Boss boss;
        public Game1 game;

        public BossStateChange(Boss boss, Game1 game)
        {
            this.boss = boss;
            this.game = game;
           
        }

        //state transition
        private void toIdleState()
        {
            boss.currentActionType = BossActionType.Idle;
            boss.velocity = Vector2.Zero;
            boss.changeSprite();
        }

        private void toRunningState()
        {
            boss.currentActionType = BossActionType.Running;
            boss.changeSprite();
        }

        private void toJumpingState() 
        {
            boss.currentActionType = BossActionType.Jumping;
            boss.changeSprite();
        }
        private void toFallingState() 
        {
            boss.currentActionType= BossActionType.Falling;
            boss.changeSprite();
        }
        private void toFlyingState() 
        {
            if(boss.currentActionType != BossActionType.Idle)
            {
                toIdleState();
            }
            boss.isFlying = true;
        }
        //action
        public void runnungLeft()
        {
            if(boss.currentActionType != BossActionType.Running)
            {
                toRunningState();
            }
            boss.isFacingRight = false;
            boss.velocity = new Vector2(-50, 0);
            if (boss._ai.angryMode)
            {
                boss.velocity = new Vector2(-120, 0);
            }
                   
        }

        public void runnungRight()
        {
            if (boss.currentActionType != BossActionType.Running)
            {
                toRunningState();
            }
            boss.isFacingRight = true;
            boss.velocity = new Vector2(50, 0);
            if (boss._ai.angryMode)
            {
                boss.velocity = new Vector2(120, 0);
            }
        }

        public void jump()
        {
            if (boss.currentActionType != BossActionType.Jumping)
            {
                toJumpingState();
            }
            boss.velocity = new Vector2(boss.velocity.X, -180);
            if (boss._ai.angryMode)
            {
                boss.velocity = new Vector2(boss.velocity.X, -220);
            }
        }

        public void fall()
        {
            if (boss.currentActionType != BossActionType.Falling)
            {
                toFallingState();
            }
            boss.velocity = new Vector2(boss.velocity.X, 180);
            if (boss._ai.angryMode)
            {
                boss.velocity = new Vector2(boss.velocity.X, 300);
            }
        }

        public void stopMoving()
        {
            boss.velocity = Vector2.Zero;
            if (boss.currentActionType != BossActionType.Idle)
            {
                toIdleState();
            }
        }

        //event
        public void hitByMario() 
        {
            if (!boss._ai.hitAndCannotMove && !boss._ai.noDmgLock)
            {
                game.bossHP -= 10;
                boss._ai.commonLogic.hitByMario();
            }
            
        }

        public void hitByFireball() 
        {
            if (!boss._ai.hitAndCannotMove && boss._ai.noFireballDmgTimer == 0f && !boss._ai.noDmgLock)
            {
                game.bossHP-- ;
                boss._ai.commonLogic.hitByFireball();
                boss._ai.noFireballDmgTimer = 0.3f;
            }
            
        } 
    }
}
