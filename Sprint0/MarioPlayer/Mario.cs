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
using Sprint0.Sprites.Lists;
using Sprint0.ObjectManager;

namespace Sprint0.MarioPlayer
{
    public class Mario : Entity
    {
        public IMarioActionState CurrentActionState { get; set; }
        public IMarioPowerupState CurrentPowerupState { get; set; }
        public bool IsFacingRight { get; set; }
        public SpritesFactory fireballFactory;
        private FireBallList fireBallList;
        public float ySpawnPosition;
        public MarioFactory marioFactory;

        public GameObjectManager gameObjectManager;
       
        public Mario(Vector2 spawnLocation,Game1 gameInstance)
        {
            marioFactory = new MarioFactory(gameInstance.Content);
            Sprite = marioFactory.buildSprites(MarioPowerupStateType.Normal, MarioActionStateType.Idle);
            this.fireballFactory = gameInstance.spritesFactory;
            this.fireBallList= gameInstance.fireBallList;
            Position = spawnLocation;
            Velocity = Vector2.Zero;
            Acceleration = Vector2.Zero;
            ySpawnPosition = spawnLocation.Y;
            
            CurrentActionState = new MarioIdleState(this, marioFactory);
            CurrentPowerupState = new MarioNormalState(this, marioFactory);

            CurrentActionState.Enter(null);
            CurrentPowerupState.Enter(null);

            IsFacingRight = true;

            gameObjectManager = gameInstance.gameObjectManager;
        }

        public void generateFireball()
        {           
            ISprite fireball = fireballFactory.getFireballSprite(Position, IsFacingRight);
            //fireBallList.add(fireball);
            gameObjectManager.addObject(fireball, "fireBall");
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

        public bool running()
        {
            return CurrentActionState.GetEnumValue() == MarioActionStateType.Running;
        }

        private string findLocation(MarioPowerupStateType powerUpType, MarioActionStateType actionType)
        {
            string spriteLocation;
            string fileNamePrefix;
            switch (powerUpType)
            {
                case MarioPowerupStateType.Normal:
                    fileNamePrefix = "NormalMario";
                    break;
                case MarioPowerupStateType.Super:
                    fileNamePrefix = "SuperMario";
                    break;
                case MarioPowerupStateType.Fire:
                    fileNamePrefix = "FireMario";
                    break;
                case MarioPowerupStateType.Dead:
                    fileNamePrefix = "";
                    break;
                default:
                    throw new ArgumentException("MarioSpriteFactory error: Invalid MarioPowerupStateType specified");
            }

            string fileNameSuffix;
            if (powerUpType != MarioPowerupStateType.Dead)
                switch (actionType)
                {
                    case MarioActionStateType.Idle:
                        fileNameSuffix = "IdleRight";
                        break;
                    case MarioActionStateType.Crouching:
                        fileNameSuffix = "CrouchRight";
                        break;
                    case MarioActionStateType.Jumping:
                    case MarioActionStateType.Falling:
                        fileNameSuffix = "JumpRight";
                        break;
                    case MarioActionStateType.Running:
                        fileNameSuffix = "WalkRight";
                        break;
                    default:
                        throw new ArgumentException("MarioSpriteFactory error: Invalid MarioActionStateType specified");
                }
            else
                fileNameSuffix = "";

            
            if (powerUpType == MarioPowerupStateType.Dead)
            {
                spriteLocation = "DeadMario/MarioDeath";
            }
            spriteLocation = fileNamePrefix + "/" + fileNamePrefix + fileNameSuffix;
            return spriteLocation;
        }
    }
}
