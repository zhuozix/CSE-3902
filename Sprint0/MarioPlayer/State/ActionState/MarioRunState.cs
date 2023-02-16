using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer.State.PowerupState;

namespace Sprint0.MarioPlayer.State.ActionState
{
    public class MarioRunState : MarioActionState
    {
        public MarioRunState(Mario marioEntity, PlayerFactory marioFactory) : base(marioEntity, marioFactory)
        { }

        public override void Enter(IMarioActionState previousState)
        {
            base.Enter(previousState);
            if (marioEntity.IsFacingRight)
            {
                marioEntity.Velocity = new Vector2(50, 0);
            }
            else
            {
                marioEntity.Velocity = new Vector2(-50, 0);
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
            CurrentState = new MarioJumpState(marioEntity, marioFactory);
            CurrentState.Enter(this);
        }

        public override void FallingTransition()
        {
            Exit();
            CurrentState = new MarioFallState(marioEntity, marioFactory);
            CurrentState.Enter(this);
        }

        public override void IdleTransition()
        {
            Exit();
            CurrentState = new MarioIdleState(marioEntity, marioFactory);
            CurrentState.Enter(this);
        }

        public override void TurnLeft()
        {
            if (marioEntity.IsFacingRight)
                IdleTransition();
        }
        public override void TurnRight()
        {
            if (!marioEntity.IsFacingRight)
                IdleTransition();
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
