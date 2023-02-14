﻿using Microsoft.Xna.Framework;

namespace Sprint0.MarioPlayer.State.ActionState
{
    public class MarioFallState : MarioActionState
    {
        public MarioFallState(Mario marioEntity, PlayerFactory marioFactory) :
            base(marioEntity, marioFactory)
        { }

        public override void Enter(IMarioActionState previousState)
        {
            base.Enter(previousState);
            marioEntity.Velocity = new Vector2(0, 50);
        }

        //  temp
        public override void JumpingTransition()
        {
            IdleTransition();
        }
        //  end temp

        public override void IdleTransition()
        {
            Exit();
            CurrentState = new MarioIdleState(marioEntity, marioFactory);
            CurrentState.Enter(this);
        }

        public override MarioActionStateType GetEnumValue()
        {
            return MarioActionStateType.Falling;
        }

        public override void Update(GameTime gameTime) { }
    }
}