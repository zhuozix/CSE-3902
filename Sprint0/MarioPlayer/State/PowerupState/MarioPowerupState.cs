using Microsoft.Xna.Framework;
using Sprint0.Sounds;

namespace Sprint0.MarioPlayer.State.PowerupState
{

    public enum MarioPowerupStateType
    {
        Normal,
        Super,
        Fire,
        Dead,
    }

    public abstract class MarioPowerupState : IMarioPowerupState
    {
        protected Mario marioEntity;
        protected MarioFactory marioFactory;
        protected IMarioPowerupState previousState;
        protected IMarioPowerupState CurrentState
        {
            get
            {
                return marioEntity.CurrentPowerupState;
            }
            set
            {
                marioEntity.CurrentPowerupState = value;
            }
        }

        public MarioPowerupState(Mario marioEntity, MarioFactory marioFactory)
        {
            this.marioEntity = marioEntity;
            this.marioFactory= marioFactory;
        }

        public virtual void Enter(IMarioPowerupState previousState)
        {
            this.previousState = previousState;
            CurrentState = this;

            Vector2 previousPosition = marioEntity.Position;
            marioEntity.Sprite = marioFactory.buildSprites(marioEntity.CurrentPowerupState.GetEnumValue(), marioEntity.CurrentActionState.GetEnumValue());
            marioEntity.Position = previousPosition;
        }

        public virtual void Exit() { }

        public abstract MarioPowerupStateType GetEnumValue();

        // exit the previous state then enter the new state
        public virtual void NormalMarioTransition()
        {
            Exit();
            CurrentState = new MarioNormalState(marioEntity, marioFactory);
            CurrentState.Enter(this);
        }
        public virtual void SuperMarioTransition()
        {
            Exit();
            CurrentState = new MarioSuperState(marioEntity, marioFactory);
            CurrentState.Enter(this);
        }
        public virtual void FireMarioTransition()
        {
            Exit();
            CurrentState = new MarioFireState(marioEntity, marioFactory);
            CurrentState.Enter(this);
        }
        public virtual void DeadMarioTransition()
        {
            Exit();
            CurrentState = new MarioDeadState(marioEntity, marioFactory);
            CurrentState.Enter(this);
        }

        public virtual void TakeDamage() { }
    }
}
