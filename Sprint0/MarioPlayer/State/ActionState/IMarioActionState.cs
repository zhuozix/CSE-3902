using Microsoft.Xna.Framework;

namespace Sprint0.MarioPlayer.State.ActionState
{
    public interface IMarioActionState
    {

        void Enter(IMarioActionState previousState);
        void Exit();

        MarioActionStateType GetEnumValue();

        void IdleTransition();
        void CrouchingTransition();
        void JumpingTransition();
        void FallingTransition();
        void RunningTransition();
        void PoleSlidingTransition();
		void TurnLeft();
        void TurnRight();
        void Attack();

        void Update(GameTime gameTime);

	}
}
