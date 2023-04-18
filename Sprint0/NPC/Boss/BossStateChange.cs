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
                   
        }

        public void runnungRight()
        {
            if (boss.currentActionType != BossActionType.Running)
            {
                toRunningState();
            }
            boss.isFacingRight = true;
            boss.velocity = new Vector2(50, 0);
        }

        public void jump()
        {
            if (boss.currentActionType != BossActionType.Jumping)
            {
                toJumpingState();
            }
            boss.velocity = new Vector2(boss.velocity.X, -180);
        }

        public void fall()
        {
            if (boss.currentActionType != BossActionType.Falling)
            {
                toFallingState();
            }
            boss.velocity = new Vector2(boss.velocity.X, 180);
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
            game.bossHP -= 10;
            boss._ai.commonLogic.hitByMario();
        }

        public void hitByFireball() 
        {
            game.bossHP--;
            boss._ai.commonLogic.hitByFireball();
        } 
    }
}
