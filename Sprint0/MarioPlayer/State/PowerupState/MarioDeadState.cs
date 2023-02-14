using Microsoft.Xna.Framework;
using System;
using Sprint0.MarioPlayer.State.PowerupState;

namespace Sprint0.MarioPlayer.State.PowerupState
{
    public class MarioDeadState : MarioPowerupState
    {
        public MarioDeadState(Mario marioEntity, PlayerFactory marioFactory) : base(marioEntity, marioFactory)
        { }

        public override MarioPowerupStateType GetEnumValue()
        {
            return MarioPowerupStateType.Dead;
        }
    }
}
