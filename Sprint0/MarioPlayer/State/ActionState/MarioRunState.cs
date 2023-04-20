using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer.State.PowerupState;

namespace Sprint0.MarioPlayer.State.ActionState
{
    public class MarioRunState : MarioActionState
    {
        public MarioRunState(Mario marioEntity, MarioFactory marioFactory) : base(marioEntity, marioFactory)
        { }

        public override void Enter(IMarioActionState previousState)
        {
            base.Enter(previousState);
            if (marioEntity.IsFacingRight)
            {
                marioEntity.velocity = new Vector2(50, 0);
            }
            else
            {
                marioEntity.velocity = new Vector2(-50, 0);
            }
        }

        public override MarioActionStateType GetEnumValue()
        {
            return MarioActionStateType.Running;
        }

        public override void CrouchingTransition()
        {
            MarioPowerupStateType powerupStateType = marioEntity.CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Super || powerupStateType == MarioPowerupStateType.Fire)
            {
                Exit();
                CurrentState = new MarioCrouchState(marioEntity, marioFactory);
                CurrentState.Enter(this);
            }
        }

        public override void JumpingTransition()
        {
            Exit();
            if (marioEntity.Jumpmode)
            {
                CurrentState = new MarioJumpState(marioEntity, marioFactory);
            }
            else
            {
                CurrentState = new MoonJumpState(marioEntity, marioFactory);
            }
            CurrentState.Enter(this);
        }

        public override void FallingTransition()
        {
            Exit();
            if (marioEntity.Jumpmode)
            {
                CurrentState = new MarioJumpState(marioEntity, marioFactory);
            }
            else
            {
                CurrentState = new MoonJumpState(marioEntity, marioFactory);
            }
            CurrentState.Enter(this);
        }
        
        public override void PoleSlidingTransition()
		{
			/*
			Exit();
            if (marioEntity.Jumpmode)
            {
                CurrentState = new MarioJumpState(marioEntity, marioFactory);
            }
            else
            {
                CurrentState = new MoonJumpState(marioEntity, marioFactory);
            }
            CurrentState.Enter(this);
            */
			Exit();
			CurrentState = new MarioPoleslideState(marioEntity, marioFactory);
			CurrentState.Enter(this);
		}

		public override void IdleTransition()
        {
            Exit();
            if (marioEntity.Jumpmode)
            {
                CurrentState = new MarioJumpState(marioEntity, marioFactory);
            }
            else
            {
                CurrentState = new MoonJumpState(marioEntity, marioFactory);
            }
            CurrentState.Enter(this);
        }

        public override void RunningTransition()
        {

        }

        public override void TurnLeft()
        {
            if (marioEntity.IsFacingRight)
                IdleTransition();
            else
            {

                   marioEntity.velocity = new Vector2(-50, 150);

            }
                
        }
        public override void TurnRight()
        {
            if (!marioEntity.IsFacingRight)
                IdleTransition();
            else
            {

                    marioEntity.velocity = new Vector2(50, 150);

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

        public override void Update(GameTime gameTime) { }
    }
}
