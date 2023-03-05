using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer.State.PowerupState;

namespace Sprint0.MarioPlayer.State.ActionState
{
    public class MarioJumpState : MarioActionState
    {
        private static float VerticalVelocity = -100;
        private float timeSpent = 0f;

        public MarioJumpState(Mario marioEntity, MarioFactory marioFactory) : base(marioEntity, marioFactory)
        { }

        public override void Enter(IMarioActionState previousState)
        {
            base.Enter(previousState);
            marioEntity.velocity += new Vector2(0, VerticalVelocity);
        }

        public override MarioActionStateType GetEnumValue()
        {
            return MarioActionStateType.Jumping;
        }

        public override void FallingTransition()
        {
            //Exit();
            //CurrentState = new MarioFallState(marioEntity, marioFactory);
            //CurrentState.Enter(this);
        }

      
        public override void IdleTransition()
        {
            Exit();
            CurrentState = new MarioIdleState(marioEntity, marioFactory);
            CurrentState.Enter(this);
        }
        public override void CrouchingTransition()
        {
            IdleTransition();
        }

        public override void Attack()
        {
            MarioPowerupStateType powerupStateType = marioEntity.CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Fire)
            {
                marioEntity.generateFireball();

            }
        }


        public override void Update(GameTime gameTime)
        {
            if (marioEntity.velocity.Y < 0)
            {
                float accelaretion = 0.98f;
                timeSpent += (float)gameTime.ElapsedGameTime.TotalSeconds;
                marioEntity.velocity = new Vector2(marioEntity.velocity.X, marioEntity.velocity.Y + (timeSpent * accelaretion));
            }
            else
            {
                timeSpent = 0f;
                IdleTransition();
            }
           
           
        }

    }
}
