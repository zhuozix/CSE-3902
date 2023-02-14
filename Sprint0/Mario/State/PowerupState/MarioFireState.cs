using Microsoft.Xna.Framework;
using System;

namespace Sprint0.Mario.State.PowerupState
{
    public class MarioFireState : MarioPowerupState
    {
        public MarioFireState(Mario marioEntity)
            : base(marioEntity)
        { }

        public override MarioPowerupStateType GetEnumValue()
        {
            return MarioPowerupStateType.Fire;
        }

        public override void TakeDamage()
        {
            NormalMarioTransition();
        }
    }
}
