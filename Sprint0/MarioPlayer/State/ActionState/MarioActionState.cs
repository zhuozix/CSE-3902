using Microsoft.Xna.Framework;

namespace Sprint0.MarioPlayer.State.ActionState
{

    public enum MarioActionStateType
    {
        Idle,
        Crouching,
        Jumping,
        Falling,
        Running,
        PoleSliding,
        Win,
    }
    public abstract class MarioActionState : IMarioActionState
    {
        protected Mario marioEntity;
        protected IMarioActionState previousState;
        protected IMarioActionState CurrentState { get { return marioEntity.CurrentActionState; } set { marioEntity.CurrentActionState = value; } }
        protected MarioFactory marioFactory;
        public MarioActionState(Mario marioEntity, MarioFactory marioFactory)
        {
            this.marioEntity = marioEntity;
            this.marioFactory = marioFactory;
        }

        public virtual void Enter(IMarioActionState previousState)
        {
            this.previousState = previousState;
            CurrentState = this;

            Vector2 previousPosition = marioEntity.Position;
            marioEntity.Sprite = marioFactory.buildSprites(marioEntity.CurrentPowerupState.GetEnumValue(), marioEntity.CurrentActionState.GetEnumValue());
            marioEntity.Position = previousPosition;
        }

        public virtual void Exit() { }

        public abstract MarioActionStateType GetEnumValue();

        public virtual void IdleTransition() { }
        public virtual void CrouchingTransition() { }
        public virtual void JumpingTransition() { }
        public virtual void FallingTransition() { }
        public virtual void RunningTransition() { }
		public virtual void PoleSlidingTransition() { }
        public virtual void WinStateTransition() { }
		public virtual void TurnLeft() { }
        public virtual void TurnRight() { }
        public virtual void Attack() { }


        public virtual void Update(GameTime gameTime) { }
    }
}
