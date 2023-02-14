﻿using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer.State.PowerupState;
using System;

namespace Sprint0.MarioPlayer.State.PowerupState
{
    public class MarioNormalState : MarioPowerupState
    {
        public MarioNormalState(Mario marioEntity, PlayerFactory marioFactory) : base(marioEntity, marioFactory)
        { }

        public override MarioPowerupStateType GetEnumValue()
        {
            return MarioPowerupStateType.Normal;
        }

        public override void DeadMarioTransition()
        {
            Exit();
            CurrentState = new MarioDeadState(marioEntity, marioFactory);
            CurrentState.Enter(this);
        }

        public override void TakeDamage()
        {
            DeadMarioTransition();
        }
    }
}