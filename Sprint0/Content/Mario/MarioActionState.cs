using Microsoft.Xna.Framework;

namespace Sprint0.Content.Mario
{

    public enum MarioActionStateType
    {
        Idle,
        Crouching,
        Jumping,
        Falling,
        Running,
    }
    public abstract class MarioActionState : IMarioActionState
    {
        protected Mario marioEntity;
        protected IMarioActionState previousState;
        protected IMarioActionState CurrentState { get { return marioEntity.CurrentActionState; } set { marioEntity.CurrentActionState = value; } }

        public MarioActionState(Mario marioEntity)
        {
            this.marioEntity = marioEntity;
        }

        public virtual void Enter(IMarioActionState previousState)
        {
            this.previousState = previousState;
            CurrentState = this;

            Vector2 previousPosition = marioEntity.Position;
            marioEntity.Sprite = Mario.SpriteFactory.BuildSprite(marioEntity.CurrentPowerupState.GetEnumValue(), marioEntity.CurrentActionState.GetEnumValue());
            marioEntity.Position = previousPosition;
        }

        public virtual void Exit() { }

        public abstract MarioActionStateType GetEnumValue();

        public virtual void IdleTransition() { }
        public virtual void CrouchingTransition() { }
        public virtual void JumpingTransition() { }
        public virtual void FallingTransition() { }
        public virtual void RunningTransition() { }

        public virtual void TurnLeft() { }
        public virtual void TurnRight() { }
        public virtual void Attack() { }

        public virtual void Update(GameTime gameTime) { }
    }
}
