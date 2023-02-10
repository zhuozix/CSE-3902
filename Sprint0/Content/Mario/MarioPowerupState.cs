using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0.Content.Mario
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

        public MarioPowerupState(Mario marioEntity)
        {
            this.marioEntity = marioEntity;
        }

        public virtual void Enter(IMarioPowerupState previousState)
        {
            this.previousState = previousState;
            CurrentState = this;

            Vector2 previousPosition = marioEntity.Position;
            marioEntity.Sprite = Mario.SpriteFactory.BuildSprite(marioEntity.CurrentPowerupState.GetEnumValue(), marioEntity.CurrentActionState.GetEnumValue());
            marioEntity.Position = previousPosition;
        }

        public virtual void Exit() { }

        public abstract MarioPowerupStateType GetEnumValue();
        /*
        public virtual void NormalMarioTransition()
        {
            Exit();
            CurrentState = new MarioNormalState(marioEntity);
            CurrentState.Enter(this);
        }
        public virtual void SuperMarioTransition()
        {
            Exit();
            CurrentState = new MarioSuperState(marioEntity);
            CurrentState.Enter(this);
        }
        public virtual void FireMarioTransition()
        {
            Exit();
            CurrentState = new MarioFireState(marioEntity);
            CurrentState.Enter(this);
        }
        public virtual void DeadMarioTransition()
        {
            Exit();
            CurrentState = new MarioDeadState(marioEntity);
            CurrentState.Enter(this);
        }
        */

        public virtual void TakeDamage() { }
    }
}
