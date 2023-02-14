using Microsoft.Xna.Framework;
using System;

namespace Sprint0.Mario.State.PowerupState
{
    public class MarioDeadState : MarioPowerupState
    {
        public MarioDeadState(Mario marioEntity)
            : base(marioEntity)
        { }

        public override MarioPowerupStateType GetEnumValue()
        {
            return MarioPowerupStateType.Dead;
        }
    }
}
