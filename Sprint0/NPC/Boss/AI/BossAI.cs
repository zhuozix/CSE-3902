using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer;
using Sprint0.NPC.Boss.AI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sprint0.NPC.Boss
{
    enum ActionLockType
    {
        unlocked,
        jumpAttack,
        fireBall,
    }
    public class BossAI
    {
        //basic
        public Game1 game;
        public Mario player;
        public Boss boss;
        public BossStateChange stateChange;

        //logic
        public CommonLogic commonLogic;
        public int angry;
        public bool angryMode;

        //Timer
        public float hitByMarioTimer = 2f;
        public float summonTimer = 0f;
        public float restTimer = 1f;
       

        //Lock
        public bool hitAndCannotMove = false;
        public bool restTimerLock = false;

        //ActionLock
        private ActionLockType actionLockType;

        public BossAI(Boss bossIn, Mario playerIn, Game1 gameIn)
        {
            game = gameIn;
            player = playerIn;
            boss = bossIn;
            commonLogic = new CommonLogic(player, boss, this, gameIn);
            stateChange = new BossStateChange(boss, game);
            angry = 0;
            actionLockType = ActionLockType.unlocked;
        }

        public void Update(GameTime gameTime)
        {
            //timer
            timerManager(gameTime);

            //ai
            if (commonLogic.canPerformAction())
            {
                ai(gameTime);
            }

        }

        public void timerManager(GameTime gameTime)
        {
            // hit by mario
            if (hitAndCannotMove)
            {
                hitByMarioTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (hitByMarioTimer <= 0)
                {
                    hitByMarioTimer = 2f;
                    hitAndCannotMove = false;
                }
            }
            //summon 
            if(summonTimer < 0f)
            {
                summonTimer = 0f;
            }else if(summonTimer > 0f)
            {
                summonTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            //jump -> fall -> rest 
            if (restTimerLock)
            {
                restTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(restTimer <= 0)
                {
                    restTimer = 1f;
                    restTimerLock = false;
                }
            }
        }

        public void ai(GameTime gameTime)
        {

            if (commonLogic.marioOnGround())
            {
                
                 if (commonLogic.findPlayerCurrentLevel() == 0 && commonLogic.marioNearBowser())
                {
                    //state 3, Mario is on the smae level on right side of level (by bowser) --> try to jump on mario
                    if (!restTimerLock)
                    {
                        if(actionLockType == ActionLockType.unlocked || actionLockType == ActionLockType.jumpAttack)
                        {
                            actionLockType = ActionLockType.jumpAttack;
                            commonLogic.jumpAttack();
                            if(boss.currentActionType == BossActionType.Idle)
                            {
                                actionLockType = ActionLockType.unlocked;
                            }
                        }
                        else
                        {
                            commonLogic.falling();
                            if (boss.currentActionType == BossActionType.Idle)
                            {
                                actionLockType = ActionLockType.unlocked;
                            }
                        }
                        
                    }
                    
                }
                else
                {
                    if(actionLockType == ActionLockType.unlocked)
                    {

                    
                        //state 1, Approach the player and summon gommba
                        if(summonTimer == 0f)
                        {
                            summonTimer = 2f;
                            commonLogic.summonGommba();
                            stateChange.stopMoving();
                        }
                        else if(summonTimer <= 1.65f)
                        {
                            commonLogic.approchToPlayer();
                        }

                    }
                    else
                    {
                        commonLogic.falling();
                        if (boss.currentActionType == BossActionType.Idle)
                        {
                            actionLockType = ActionLockType.unlocked;
                        }
                    }

                }
            }
            else
            {
                if(actionLockType != ActionLockType.jumpAttack) { 

                    //state 2, boss and mario is in different layer --> jump to marios level and launch a fireball
                    //shoot fireball to commonLogic.findPlayerCurrentLevel();
                    int level = commonLogic.findPlayerCurrentLevel();
                    int jumpTolerence = 80;
                    int[] yFirePosition = { 0,350, 254, 158 };
                    if (boss.Position.Y == player.Position.Y)
                    {
                        commonLogic.fireballAttack();
                    }
                    if(boss.currentActionType != BossActionType.Falling && !restTimerLock)
                    {
                        actionLockType |= ActionLockType.fireBall;
                        commonLogic.jumpTo(yFirePosition[level] - jumpTolerence);
                    }
                
                    if(boss.currentActionType == BossActionType.Falling)
                    {
                        commonLogic.falling();
                        if (boss.currentActionType == BossActionType.Idle)
                        {
                            actionLockType = ActionLockType.unlocked;
                        }
                    }

                }
                else
                {
                    commonLogic.falling();
                    if (boss.currentActionType == BossActionType.Idle)
                    {
                        actionLockType = ActionLockType.unlocked;
                    }
                }
            }

        }
    }
}
