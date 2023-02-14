using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Content;
using System;
using Sprint0.MarioPlayer.State.ActionState;
using Sprint0.MarioPlayer.State.PowerupState;

namespace Sprint0.MarioPlayer
{
    public class Mario : Entity
    {
        public IMarioActionState CurrentActionState { get; set; }
        public IMarioPowerupState CurrentPowerupState { get; set; }
        public bool IsFacingRight { get; set; }
        private PlayerFactory marioFactory;
 
        public Mario(Vector2 spawnLocation,PlayerFactory marioFactoryIn)
        {
            marioFactory = marioFactoryIn;
            Sprite = marioFactory.buildSprites(MarioPowerupStateType.Normal, MarioActionStateType.Idle);
           

            Position = spawnLocation;
            Velocity = Vector2.Zero;
            Acceleration = Vector2.Zero;

            CurrentActionState = new MarioIdleState(this, marioFactory);
            CurrentPowerupState = new MarioNormalState(this, marioFactory);

            CurrentActionState.Enter(null);
            CurrentPowerupState.Enter(null);

            IsFacingRight = true;
        }

        #region Action state transitions
        public void Jump()
        {
            CurrentActionState.JumpingTransition();
        }

        public void MoveLeft()
        {
            CurrentActionState.TurnLeft();
        }

        public void MoveRight()
        {
            CurrentActionState.TurnRight();
        }

        public void Crouch()
        {
            CurrentActionState.CrouchingTransition();
        }

        public void Attack()
        {
            CurrentActionState.Attack();
        }
        #endregion

        #region Powerup state transitions
        public void RevertToNormal()
        {
            CurrentPowerupState.NormalMarioTransition();
        }

        public void UseSuperMushroom()
        {
            CurrentPowerupState.SuperMarioTransition();
        }

        public void UseFireMushroom()
        {
            CurrentPowerupState.FireMarioTransition();
        }

        public void TakeDamage()
        {
            CurrentPowerupState.TakeDamage();
        }
        #endregion

        public override void Update(GameTime gameTime)
        {

            CurrentActionState.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, bool isFlipped)
        {
            base.Draw(spriteBatch, !IsFacingRight);
        }
    }
}
