using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer.State.PowerupState;

namespace Sprint0.MarioPlayer.State.ActionState
{
    public class MarioIdleState : MarioActionState
    {
        public MarioIdleState(Mario marioEntity, PlayerFactory marioFactory) : base(marioEntity, marioFactory)
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
                CurrentState = new MarioCrouchState(marioEntity, marioFactory);
                CurrentState.Enter(this);
            }
            else
                FallingTransition();
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

        public override void RunningTransition()
        {
            Exit();
            CurrentState = new MarioRunState(marioEntity, marioFactory);
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
