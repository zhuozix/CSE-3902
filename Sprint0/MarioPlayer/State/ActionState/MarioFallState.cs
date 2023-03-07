using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer.State.PowerupState;
using System;

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

            marioEntity.velocity = new Vector2(0, 100);
            
            
        }

        public override void JumpingTransition()
        {

        }

        public override void IdleTransition()
        {
            

        }

        public override void RunningTransition()
        {

        }

        public override void FallingTransition()
        {
            
        }

        public override void TurnLeft()
        {
            if (!marioEntity.crash && marioEntity.velocity.X == 0 && !marioEntity.IsFacingRight)
            {
                marioEntity.velocity = new Vector2(-50, marioEntity.velocity.Y);

            }
            else if (marioEntity.crash)
            {
                IdleTransition();
            }
        }
        public override void TurnRight()
        {
            if (!marioEntity.crash && marioEntity.velocity.X == 0 && marioEntity.IsFacingRight)
            {
                marioEntity.velocity = new Vector2(50, marioEntity.velocity.Y);
            } else if (marioEntity.crash)
            {
                IdleTransition();
            }
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
            if (marioEntity.crash || marioEntity.velocity.Y == 0)
            {
                Exit();
                CurrentState = new MarioIdleState(marioEntity, marioFactory);
                CurrentState.Enter(this);
            }

            else if (marioEntity.velocity.Y < 150)
            {
                float accelaretion = 9.8f;
                timeSpent += (float)gameTime.ElapsedGameTime.TotalSeconds;
                marioEntity.velocity = new Vector2(marioEntity.velocity.X, marioEntity.velocity.Y + (timeSpent * accelaretion));
            }
            

        }
    }
}
