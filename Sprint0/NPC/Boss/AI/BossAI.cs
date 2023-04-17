using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer;
using Sprint0.NPC.Boss.AI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.NPC.Boss
{
    public class BossAI
    {
        //basic
        public Game1 game;
        public Mario player;
        public Boss boss;
        public BossStateChange stateChange;

        //logic
        public CommonLogic commonLogic;

        //Timer
        private float hitByMarioTimer = 2f;

        //Lock
        private bool hitAndCannotMove = false;

        public BossAI(Boss bossIn, Mario playerIn, Game1 gameIn)
        {
            game = gameIn;
            player = playerIn;
            boss = bossIn;
            commonLogic = new CommonLogic(player,boss);
            stateChange = new BossStateChange(boss, game);
        }

        public void Update(GameTime gameTime)
        {
            //timer
            timerManager(gameTime);
            //common logic
            hitByMario(gameTime);
            hitByFireball(gameTime);

            //ai
            if (canPerformAction())
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
                if(hitByMarioTimer <= 0)
                {
                    hitByMarioTimer = 2f;
                    hitAndCannotMove = false;
                }
            }
        }

        public void ai(GameTime gameTime)
        {
            //state 1, boss and mario in the same layer on left side of level --> spawn enemies to go towards mario
            if (commonLogic.marioOnGround())
            {
                commonLogic.findPlayerDirection();
                

            }
            //state 2, boss and mario is in different layer --> jump to marios level and launch a fireball

            //state 3, Mario is on the smae level on right side of level (by bowser) --> try to jump on mario
            
        }

        public bool canPerformAction()
        {
            if (hitAndCannotMove)
            {
                return false;
            }
            return true;
        }

        public void hitByMario(GameTime gameTime)
        {
            hitAndCannotMove = true;
            stateChange.hitByMario();
            stateChange.toIdleState();
        }

        public void hitByFireball(GameTime gameTime)
        {
            stateChange.hitByFireball();
        }
    }
}
