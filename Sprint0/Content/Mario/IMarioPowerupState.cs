using System;

namespace Sprint0.Content.Mario
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
