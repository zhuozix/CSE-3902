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
        public float angryTimer = 12f;

        //Timer
        public float noDmgTimer = 5f;
        public float noFireballDmgTimer = 0f;
        public float hitByMarioTimer = 2f;
        public float summonTimer = 0f;
        public float restTimer = 1f;
        

        //Lock
        public bool noDmgLock = false;
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
                    
                    noDmgTimer = 5f;
                    if (angryMode)
                    {
                        noDmgTimer = 6.5f;
                    }
                    noDmgLock = true;

                    hitAndCannotMove = false;
                }
                commonLogic.falling();
            }
            //summon 
            summonTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            summonTimer = Math.Max(0f, summonTimer);

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

            //fireball
            if(noFireballDmgTimer != 0f)
            {
                if(noFireballDmgTimer < 0f)
                {
                    noFireballDmgTimer = 0f;
                }
                else
                {
                    noFireballDmgTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            //no damage
            if (noDmgLock)
            {
                noDmgTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (noDmgTimer <= 0)
                {    
                    noDmgLock = false;
                }
            }

            //angry
            if(angry >= 50)
            {
                angry = 0;
                angryMode = true;
            }
            if (angryMode)
            {
               angryTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (noDmgTimer <= 0)
                {
                    angryMode = false;
 
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
                        if(summonTimer == 0f && !commonLogic.haveGommbaNearBy())
                        {

                             summonTimer = 2f;
                             stateChange.stopMoving();
                             commonLogic.summonGommba();

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
