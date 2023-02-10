using Microsoft.Xna.Framework;

namespace Sprint0.Content.Mario
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
        
        void TurnLeft();
        void TurnRight();
        void Attack();

        void Update(GameTime gameTime);
    }
}
