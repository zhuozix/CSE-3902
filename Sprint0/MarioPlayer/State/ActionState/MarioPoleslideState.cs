using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer.State.PowerupState;
using Sprint0.Sounds;
using System;

namespace Sprint0.MarioPlayer.State.ActionState
{
    public class MarioPoleslideState : MarioActionState
	{
		public MarioPoleslideState(Mario marioEntity, MarioFactory marioFactory) :
			base(marioEntity, marioFactory)
		{ }

		public override void Enter(IMarioActionState previousState)
		{
			base.Enter(previousState);

			marioEntity.velocity = new Vector2(0, 70);

		}

		public override void JumpingTransition()
		{

		}

		public override void IdleTransition()
		{


		}

		public override void RunningTransition()
		{
		}

		public override void FallingTransition()
		{

		}
		public override void PoleSlidingTransition()
		{
            //if (previousState.GetEnumValue() != CurrentState.GetEnumValue()) SoundPlayer.playStageClear();
            //Exit();
			//CurrentState = new MarioPoleslideState(marioEntity, marioFactory);
			//CurrentState.Enter(this);
		}
		public override void TurnLeft()
		{




		}
		public override void TurnRight()
		{



		}

		public override void Attack()
		{

		}

        public override void WinStateTransition()
        {
            Exit();
            CurrentState = new MarioWinState(marioEntity, marioFactory);
            CurrentState.Enter(this);
        }


        public override MarioActionStateType GetEnumValue()
		{
			return MarioActionStateType.PoleSliding;
		}

		public override void Update(GameTime gameTime)
		{
            MarioPowerupStateType powerupStateType = marioEntity.CurrentPowerupState.GetEnumValue();
			if(powerupStateType != MarioPowerupStateType.Normal)
			{
				if(marioEntity.Position.Y >= 366)
				{
                    marioEntity.velocity = new Vector2(50, 0);
                    WinStateTransition();
				}
			}
            else if (marioEntity.Position.Y >= 386)
			{
				WinStateTransition();
			}


		}
	}
}
