using Microsoft.Xna.Framework;
using System;

namespace Sprint0.Mario.State.PowerupState
{
    public class MarioSuperState : MarioPowerupState
    {
        public MarioSuperState(Mario marioEntity)
            : base(marioEntity)
        { }

        public override MarioPowerupStateType GetEnumValue()
        {
            return MarioPowerupStateType.Super;
        }

        public override void TakeDamage()
        {
            NormalMarioTransition();
        }
    }
}
