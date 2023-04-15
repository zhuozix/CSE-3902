using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer.State.PowerupState;
using System;

namespace Sprint0.MarioPlayer.State.ActionState
{
    public class MoonJumpState : MarioActionState
    {
        private static float VerticalVelocity = -300;

        public MoonJumpState(Mario marioEntity, MarioFactory marioFactory) : base(marioEntity, marioFactory)
        { }

        public override void Enter(IMarioActionState previousState)
        {
            base.Enter(previousState);
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
                marioEntity.velocity = new Vector2(marioEntity.velocity.X, marioEntity.velocity.Y);
            }
            else
            {
                FallingTransition();
            }
        }

    }
}
