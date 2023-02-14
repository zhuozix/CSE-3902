using Microsoft.Xna.Framework;

namespace Sprint0.MarioPlayer.State.ActionState
{
    public class MarioCrouchState : MarioActionState
    {
        public MarioCrouchState(Mario marioEntity, PlayerFactory marioFactory) :
            base(marioEntity, marioFactory)
        { }

        public override void Enter(IMarioActionState previousState)
        {
            base.Enter(previousState);
            marioEntity.Velocity = new Vector2(0, 50);
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

        public override void JumpingTransition()
        {
            Exit();
            CurrentState = new MarioIdleState(marioEntity, marioFactory);
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

        public override void Update(GameTime gameTime) { }
    }
}
