using Microsoft.Xna.Framework;
using Sprint0.Factory;
using Sprint0.NPC.Item;
using Sprint0.MarioPlayer.State.PowerupState;
using Sprint0.Sprites;

namespace Sprint0.MarioPlayer.State.ActionState
{
    public class MarioIdleState : MarioActionState
    {
        public MarioIdleState(Mario marioEntity, MarioFactory marioFactory) : base(marioEntity, marioFactory)
        {
        }


        public override void Enter(IMarioActionState previousState)
        {
            base.Enter(previousState);
             if (marioEntity.velocity.Y == 0)
            {
                marioEntity.velocity = new Vector2(0, 0);
            }
            else
            {
                FallingTransition();
            }     
        }

        public override MarioActionStateType GetEnumValue()
        {
            return MarioActionStateType.Idle;
        }

        public override void CrouchingTransition()
        {
            MarioPowerupStateType powerupStateType = marioEntity.CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Super || powerupStateType == MarioPowerupStateType.Fire)
            {
                Exit();
                CurrentState = new MarioCrouchState(marioEntity, marioFactory);
                CurrentState.Enter(this);
            }
        }

        public override void JumpingTransition()
        {
            MarioPowerupStateType powerupStateType = marioEntity.CurrentPowerupState.GetEnumValue();
            if (powerupStateType != MarioPowerupStateType.Dead)
            {
                Exit();
                CurrentState = new MarioJumpState(marioEntity, marioFactory);
                CurrentState.Enter(this); 
            }
            
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

        public override void RunningTransition()
        {
            MarioPowerupStateType powerupStateType = marioEntity.CurrentPowerupState.GetEnumValue();
            if (powerupStateType != MarioPowerupStateType.Dead)
            {
                Exit();
                CurrentState = new MarioRunState(marioEntity, marioFactory);
                CurrentState.Enter(this);
            }
            
        }

        public override void TurnLeft()
        {
            MarioPowerupStateType powerupStateType = marioEntity.CurrentPowerupState.GetEnumValue();
            if (powerupStateType != MarioPowerupStateType.Dead)
            {
                if (marioEntity.IsFacingRight)
                    marioEntity.IsFacingRight = false;
                else
                    RunningTransition();
            }
            
        }
        public override void TurnRight()
        {
            MarioPowerupStateType powerupStateType = marioEntity.CurrentPowerupState.GetEnumValue();
            if (powerupStateType != MarioPowerupStateType.Dead)
            {
                if (!marioEntity.IsFacingRight)
                    marioEntity.IsFacingRight = true;
                else
                    RunningTransition();
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

        public override void Update(GameTime gameTime) {


        }
    }
}
