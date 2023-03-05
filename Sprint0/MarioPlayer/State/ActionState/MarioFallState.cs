﻿using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer.State.PowerupState;

namespace Sprint0.MarioPlayer.State.ActionState
{
    public class MarioFallState : MarioActionState
    {
        private float timeSpent = 0f;
        public MarioFallState(Mario marioEntity, MarioFactory marioFactory) :
            base(marioEntity, marioFactory)
        { }

        public override void Enter(IMarioActionState previousState)
        {
            base.Enter(previousState);
            marioEntity.velocity = new Vector2(marioEntity.velocity.X, 50);
        }

        public override void JumpingTransition()
        {

        }

        public override void IdleTransition()
        {
            if (!marioEntity.yPositionChecker())
            {
                Exit();
                CurrentState = new MarioIdleState(marioEntity, marioFactory);
                CurrentState.Enter(this);
            }
           
        }

        public override void RunningTransition()
        {

        }

        public override void FallingTransition()
        {

        }

        public override void Attack()
        {
            MarioPowerupStateType powerupStateType = marioEntity.CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Fire)
            {
                marioEntity.generateFireball();

            }
        }

        public override MarioActionStateType GetEnumValue()
        {
            return MarioActionStateType.Falling;
        }

        public override void Update(GameTime gameTime)
        {
            if (marioEntity.velocity.Y < 100)
            {
                float accelaretion = 9.8f;
                timeSpent += (float)gameTime.ElapsedGameTime.TotalSeconds;
                marioEntity.velocity = new Vector2(marioEntity.velocity.X, marioEntity.velocity.Y + (timeSpent * accelaretion));
            }
            else
            {
                timeSpent = 0f;
                if (!marioEntity.yPositionChecker())
                {
                    IdleTransition();
                }
            }
        }
    }
}
