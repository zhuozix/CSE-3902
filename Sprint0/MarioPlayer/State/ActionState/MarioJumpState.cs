using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer.State.PowerupState;
using System;

namespace Sprint0.MarioPlayer.State.ActionState
{
    public class MarioJumpState : MarioActionState
    {
        private static float VerticalVelocity = -120;
        private static float superVerticalVelocity = -180;
        private float timeSpent = 0f;

        public MarioJumpState(Mario marioEntity, MarioFactory marioFactory) : base(marioEntity, marioFactory)
        { }

        public override void Enter(IMarioActionState previousState)
        {
            base.Enter(previousState);
            MarioPowerupStateType powerupStateType = marioEntity.CurrentPowerupState.GetEnumValue();
            if(powerupStateType != MarioPowerupStateType.Normal)
            {
                VerticalVelocity = superVerticalVelocity;
            }
            marioEntity.velocity += new Vector2(marioEntity.velocity.X, VerticalVelocity);
        }

        public override MarioActionStateType GetEnumValue()
        {
            return MarioActionStateType.Jumping;
        }

        public override void FallingTransition()
        {
            Exit();
            CurrentState = new MarioFallState(marioEntity, marioFactory);
            CurrentState.Enter(this);
        }

		public override void PoleSlidingTransition()
		{
			Exit();
			CurrentState = new MarioPoleslideState(marioEntity, marioFactory);
			CurrentState.Enter(this);
		}

		public override void IdleTransition()
        {
            Exit();
            CurrentState = new MarioFallState(marioEntity, marioFactory);
            CurrentState.Enter(this);
        }
        public override void CrouchingTransition()
        {
            IdleTransition();
        }
        public override void TurnLeft()
        {
            
            if (marioEntity.IsFacingRight)
            {
                marioEntity.IsFacingRight = false;
            }
            if (Math.Abs(marioEntity.velocity.X - 50) <= 100)
            {
                marioEntity.velocity = new Vector2(marioEntity.velocity.X - 50, marioEntity.velocity.Y);
            }
            else
            {
                marioEntity.velocity = new Vector2(-50, marioEntity.velocity.Y);
            }

        }
        public override void TurnRight()
        {
           
            if (!marioEntity.IsFacingRight)
            {
                marioEntity.IsFacingRight = true;
            }
            if (marioEntity.velocity.X + 50 <= 100)
            {
                marioEntity.velocity = new Vector2(marioEntity.velocity.X + 50, marioEntity.velocity.Y);
            }
            else
            {
                marioEntity.velocity = new Vector2(50, marioEntity.velocity.Y);
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


        public override void Update(GameTime gameTime)
        {
            if (marioEntity.velocity.Y < 0)
            {
                float accelaretion = 0.98f;
                timeSpent += (float)gameTime.ElapsedGameTime.TotalSeconds;
                marioEntity.velocity = new Vector2(marioEntity.velocity.X, marioEntity.velocity.Y + (timeSpent * accelaretion));
            }
            else
            {
                timeSpent = 0f;
                FallingTransition();
            }
           
           
        }

    }
}
