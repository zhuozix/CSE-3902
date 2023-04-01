using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer.State.PowerupState;
using System;

namespace Sprint0.MarioPlayer.State.ActionState
{
    public class MarioFallState : MarioActionState
    {
        private float timeSpent = 0f;
        public MarioFallState(Mario marioEntity, MarioFactory marioFactory) :
            base(marioEntity, marioFactory)
        { }

        public override void Enter(IMarioActionState previousState)
        {
            base.Enter(previousState);
 
            marioEntity.velocity = new Vector2(marioEntity.velocity.X, 70);

            
            
            
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
			MarioPowerupStateType powerupStateType = marioEntity.CurrentPowerupState.GetEnumValue();
			if (powerupStateType != MarioPowerupStateType.Dead)
			{
				Exit();
				CurrentState = new MarioFallState(marioEntity, marioFactory);
				CurrentState.Enter(this);
			}
		}

        public override void PoleSlidingTransition() 
        {
			Exit();
			CurrentState = new MarioPoleslideState(marioEntity, marioFactory);
			CurrentState.Enter(this);
		}
        public override void TurnLeft()
        {

                if (marioEntity.IsFacingRight)
                {
                    marioEntity.IsFacingRight = false;
                }
               if(Math.Abs(marioEntity.velocity.X - 50) <= 100)
            {
                marioEntity.velocity = new Vector2(marioEntity.velocity.X - 20, marioEntity.velocity.Y);
            }
            else
            {
                marioEntity.velocity = new Vector2(-20, marioEntity.velocity.Y);
            }
              
           
            
        }
        public override void TurnRight()
        {

                if (!marioEntity.IsFacingRight)
                {
                    marioEntity.IsFacingRight = true;
                }
            if (marioEntity.velocity.X + 50 <= 100)
            {
                marioEntity.velocity = new Vector2(marioEntity.velocity.X + 20, marioEntity.velocity.Y);
            }
            else
            {
                marioEntity.velocity = new Vector2( 20, marioEntity.velocity.Y);
            }
               
            
         
        }

        public override void Attack()
        {
            MarioPowerupStateType powerupStateType = marioEntity.CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Fire)
            {
                marioEntity.generateFireball();

            }
        }

        public override MarioActionStateType GetEnumValue()
        {
            return MarioActionStateType.Falling;
        }

        public override void Update(GameTime gameTime)
        {
            if (marioEntity.crash || marioEntity.velocity.Y == 0)
            {
                Exit();
                CurrentState = new MarioIdleState(marioEntity, marioFactory);
                CurrentState.Enter(this);
            }

            else if (marioEntity.velocity.Y < 150)
            {
                float accelaretion = 9.8f;
                timeSpent += (float)gameTime.ElapsedGameTime.TotalSeconds;
                marioEntity.velocity = new Vector2(marioEntity.velocity.X, marioEntity.velocity.Y + (timeSpent * accelaretion));
            }
            

        }
    }
}
