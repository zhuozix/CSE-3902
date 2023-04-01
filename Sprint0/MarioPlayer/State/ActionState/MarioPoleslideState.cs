using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer.State.PowerupState;
using System;

namespace Sprint0.MarioPlayer.State.ActionState
{
	public class MarioPoleslideState : MarioActionState
	{
		private float timeSpent = 0f;
		public MarioPoleslideState(Mario marioEntity, MarioFactory marioFactory) :
			base(marioEntity, marioFactory)
		{ }

		public override void Enter(IMarioActionState previousState)
		{
			base.Enter(previousState);

			marioEntity.velocity = new Vector2(0, 10);




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
			Exit();
			CurrentState = new MarioPoleslideState(marioEntity, marioFactory);
			CurrentState.Enter(this);
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

		public override MarioActionStateType GetEnumValue()
		{
			return MarioActionStateType.PoleSliding;
		}

		public override void Update(GameTime gameTime)
		{
			//if (marioEntity.crash || marioEntity.velocity.Y == 0)
			//{
			//	Exit();
			//	CurrentState = new MarioIdleState(marioEntity, marioFactory);
			//	CurrentState.Enter(this);
			//}

			//else if (marioEntity.velocity.Y < 150)
			//{
			//	float accelaretion = 9.8f;
			//	timeSpent += (float)gameTime.ElapsedGameTime.TotalSeconds;
			//	marioEntity.velocity = new Vector2(marioEntity.velocity.X, marioEntity.velocity.Y + (timeSpent * accelaretion));
			//}

			if (marioEntity.velocity.Y == 0)
			{
				Exit();
				CurrentState = new MarioIdleState(marioEntity, marioFactory);
				CurrentState.Enter(this);
			}


		}
	}
}
