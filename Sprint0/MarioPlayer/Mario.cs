using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Content;
using System;
using Sprint0.MarioPlayer.State.ActionState;
using Sprint0.MarioPlayer.State.PowerupState;
using Sprint0.Factory;
using Sprint0.Item;
using Sprint0.Sprites;
using System.Collections;

namespace Sprint0.MarioPlayer
{
    public class Mario : Entity
    {
        public IMarioActionState CurrentActionState { get; set; }
        public IMarioPowerupState CurrentPowerupState { get; set; }
        public bool IsFacingRight { get; set; }
        public BulletFactory fireballFactory;
        private ArrayList bulletList;
        public float ySpawnPosition;
        public Mario(Vector2 spawnLocation,Game1 gameInstance)
        {
            Sprite = gameInstance.playerFactory.buildSprites(MarioPowerupStateType.Normal, MarioActionStateType.Idle);

            this.fireballFactory = gameInstance.fireballFactory;
            this.bulletList = gameInstance.bulletList;
            Position = spawnLocation;
            Velocity = Vector2.Zero;
            Acceleration = Vector2.Zero;
            ySpawnPosition = spawnLocation.Y;

            CurrentActionState = new MarioIdleState(this, gameInstance.playerFactory);
            CurrentPowerupState = new MarioNormalState(this, gameInstance.playerFactory);

            CurrentActionState.Enter(null);
            CurrentPowerupState.Enter(null);

            IsFacingRight = true;
        }

        public void generateFireball()
        {
            if(bulletList.Count < 3)
            {
                ISprite fireball = fireballFactory.getFireballSprite(Position, IsFacingRight);
                bulletList.Add(fireball);
            }
            else
            {
                bulletList.RemoveAt(0);
                ISprite fireball = fireballFactory.getFireballSprite(Position, IsFacingRight);
                bulletList.Add(fireball);
            }
            
        }

        public bool yPositionChecker()
        {
            return Position.Y < ySpawnPosition;
        }

        #region Action state transitions

        public void idle()
        {
            CurrentActionState.IdleTransition();
        }
        public void Jump()
        {
            CurrentActionState.JumpingTransition();
        }

        public void fallAfterJump()
        {
            CurrentActionState.FallingTransition();
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
