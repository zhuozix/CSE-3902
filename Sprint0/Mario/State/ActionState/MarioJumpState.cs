using Microsoft.Xna.Framework;

namespace Sprint0.Mario.State.ActionState
{
    public class MarioJumpState : MarioActionState
    {
        public MarioJumpState(Mario marioEntity) :
            base(marioEntity)
        { }

        public override void Enter(IMarioActionState previousState)
        {
            base.Enter(previousState);
            marioEntity.Velocity = new Vector2(0, -50);
        }

        public override MarioActionStateType GetEnumValue()
        {
            return MarioActionStateType.Jumping;
        }

        public override void FallingTransition()
        {
            Exit();
            CurrentState = new MarioFallState(marioEntity);
            CurrentState.Enter(this);
        }

        //  temp
        public override void IdleTransition()
        {
            Exit();
            CurrentState = new MarioIdleState(marioEntity);
            CurrentState.Enter(this);
        }
        public override void CrouchingTransition()
        {
            IdleTransition();
        }
        //  end temp

        public override void Update(GameTime gameTime)
        {
            
        }

    }
}
