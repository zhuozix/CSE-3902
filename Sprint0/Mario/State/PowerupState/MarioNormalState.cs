using Microsoft.Xna.Framework;
using System;

namespace Sprint0.Mario.State.PowerupState
{
    public class MarioNormalState : MarioPowerupState
    {
        public MarioNormalState(Mario marioEntity)
            : base(marioEntity)
        { }

        public override MarioPowerupStateType GetEnumValue()
        {
            return MarioPowerupStateType.Normal;
        }

        public override void DeadMarioTransition()
        {
            Exit();
            CurrentState = new MarioDeadState(marioEntity);
            CurrentState.Enter(this);
        }

        public override void TakeDamage()
        {
            DeadMarioTransition();
        }
    }
}
