using Microsoft.Xna.Framework;

namespace Sprint0.MarioPlayer.State.PowerupState
{
    public interface IMarioPowerupState
    {

        void Enter(IMarioPowerupState previousState);
        void Exit();

        MarioPowerupStateType GetEnumValue();

        void NormalMarioTransition();
        void SuperMarioTransition();
        void FireMarioTransition();
        void DeadMarioTransition();

        void TakeDamage();
    }
}
