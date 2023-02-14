using Microsoft.Xna.Framework;
using Sprint0.Mario.State.PowerupState;

namespace Sprint0.Mario.State.ActionState
{
    public class MarioIdleState : MarioActionState
    {
        public MarioIdleState(Mario marioEntity) :
            base(marioEntity)
        { }

        public override void Enter(IMarioActionState previousState)
        {
            base.Enter(previousState);
            marioEntity.Velocity = new Vector2(0, 0);
        }

        public override MarioActionStateType GetEnumValue()
        {
            return MarioActionStateType.Idle;
        }

        public override void CrouchingTransition()
        {
            MarioPowerupStateType powerupStateType = marioEntity.CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Super || powerupStateType == MarioPowerupStateType.Fire)
            {
                Exit();
                CurrentState = new MarioCrouchState(marioEntity);
                CurrentState.Enter(this);
            }
            else
                FallingTransition();
        }

        public override void JumpingTransition()
        {
            Exit();
            CurrentState = new MarioJumpState(marioEntity);
            CurrentState.Enter(this);
        }

        public override void FallingTransition()
        {
            Exit();
            CurrentState = new MarioFallState(marioEntity);
            CurrentState.Enter(this);
        }

        public override void RunningTransition()
        {
            Exit();
            CurrentState = new MarioRunState(marioEntity);
            CurrentState.Enter(this);
        }

        public override void TurnLeft()
        {
            if (marioEntity.IsFacingRight)
                marioEntity.IsFacingRight = false;
            else
                RunningTransition();
        }
        public override void TurnRight()
        {
            if (!marioEntity.IsFacingRight)
                marioEntity.IsFacingRight = true;
            else
                RunningTransition();
        }
        public override void Attack() { }

        public override void Update(GameTime gameTime) { }
    }
}
