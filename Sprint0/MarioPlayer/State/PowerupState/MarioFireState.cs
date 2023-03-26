using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer.State.PowerupState;
using Sprint0.Sounds;
using System;

namespace Sprint0.MarioPlayer.State.PowerupState
{
    public class MarioFireState : MarioPowerupState
    {
        public MarioFireState(Mario marioEntity,MarioFactory marioFactory)
            : base(marioEntity, marioFactory)
        { }

        public override MarioPowerupStateType GetEnumValue()
        {
            return MarioPowerupStateType.Fire;
        }

        public override void TakeDamage()
        {
            SoundPlayer.playMarioHit();
            SuperMarioTransition();
        }
    }
}
