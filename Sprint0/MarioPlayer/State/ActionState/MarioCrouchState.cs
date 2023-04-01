using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer.State.PowerupState;

namespace Sprint0.MarioPlayer.State.ActionState
{
    public class MarioCrouchState : MarioActionState
    {
        public MarioCrouchState(Mario marioEntity, MarioFactory marioFactory) :
            base(marioEntity, marioFactory)
        { }

        public override void Enter(IMarioActionState previousState)
        {
            base.Enter(previousState);
            marioEntity.velocity = new Vector2(0, 0);
        }

        public override MarioActionStateType GetEnumValue()
        {
            return MarioActionStateType.Crouching;
        }

        public override void CrouchingTransition()
        {
            Exit();
            CurrentState = new MarioCrouchState(marioEntity, marioFactory);
            CurrentState.Enter(this);
        }
        public override void IdleTransition()
        {
            Exit();
            CurrentState = new MarioIdleState(marioEntity, marioFactory);
            CurrentState.Enter(this);
        }
        public override void JumpingTransition()
        {
            Exit();
            CurrentState = new MarioIdleState(marioEntity, marioFactory);
            CurrentState.Enter(this);
        }

		public override void PoleSlidingTransition()
		{
			Exit();
			CurrentState = new MarioPoleslideState(marioEntity, marioFactory);
			CurrentState.Enter(this);
		}

		public override void FallingTransition()
        {
            Exit();
            CurrentState = new MarioFallState(marioEntity, marioFactory);
            CurrentState.Enter(this);
        }

        public override void RunningTransition()
        {
            Exit();
            CurrentState = new MarioRunState(marioEntity, marioFactory);
            CurrentState.Enter(this);
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
        
        }
    }
}
