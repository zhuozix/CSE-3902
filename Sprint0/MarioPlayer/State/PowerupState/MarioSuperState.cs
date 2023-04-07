using Microsoft.Xna.Framework;
using Sprint0.ObjectManager.Sounds;
using System;

namespace Sprint0.MarioPlayer.State.PowerupState
{
    public class MarioSuperState : MarioPowerupState
    {
        public MarioSuperState(Mario marioEntity, MarioFactory marioFactory) : base(marioEntity, marioFactory)
        { }

        public override MarioPowerupStateType GetEnumValue()
        {
            return MarioPowerupStateType.Super;
        }

        public override void TakeDamage()
        {
            SoundPlayer.playMarioHit();
            NormalMarioTransition();
        }
    }
}
