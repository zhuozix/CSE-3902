using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Content;
using System;
using Sprint0.MarioPlayer.State.ActionState;
using Sprint0.MarioPlayer.State.PowerupState;
using Sprint0.Factory;
using Sprint0.NPC.Item;
using Sprint0.Sprites;
using System.Collections;

using Sprint0.ObjectManager;
using Sprint0.Command.GameControlCMD;

namespace Sprint0.MarioPlayer
{
    public class Mario : Entity
    {
        public IMarioActionState CurrentActionState { get; set; }
        public IMarioPowerupState CurrentPowerupState { get; set; }
        public bool IsFacingRight { get; set; }
        public SpritesFactory fireballFactory;
        private ArrayList fireBallList;
        public float ySpawnPosition;
        public MarioFactory marioFactory;
        public bool crash { get; set; }
        public GameObjectManager gameObjectManager;
        private float timeSpent = 0f;
        Game1 game;

        public Mario(Vector2 spawnLocation,Game1 gameInstance)
        {
            game= gameInstance;
            marioFactory = new MarioFactory(gameInstance.Content);
            Sprite = marioFactory.buildSprites(MarioPowerupStateType.Normal, MarioActionStateType.Idle);
            this.fireballFactory = gameInstance.spritesFactory;
            this.fireBallList= gameInstance.fireBallList;
            Position = spawnLocation;
            velocity = new Vector2(0,150);
            Acceleration = Vector2.Zero;
            ySpawnPosition = spawnLocation.Y;
            
            CurrentActionState = new MarioIdleState(this, marioFactory);
            CurrentPowerupState = new MarioNormalState(this, marioFactory);

            CurrentActionState.Enter(null);
            CurrentPowerupState.Enter(null);

            IsFacingRight = true;

            gameObjectManager = gameInstance.gameObjectManager;
            crash = false;
        }

        public void generateFireball()
        {           
            ISprite fireball = fireballFactory.getFireballSprite(Position, IsFacingRight);
            //fireBallList.add(fireball);
            gameObjectManager.addObject(fireball, "fireBall");
        }


        #region Action state transitions

        public void idle()
        {

            CurrentActionState.IdleTransition();
        }
        public void Jump()
        {
            MarioPowerupStateType powerupStateType = this.CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Dead)
            {
                CurrentActionState.IdleTransition();
            }
            CurrentActionState.JumpingTransition();
        }

        public void fallAfterJump()
        {
            MarioPowerupStateType powerupStateType = this.CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Dead)
            {
                CurrentActionState.IdleTransition();
            }
            CurrentActionState.FallingTransition();
        }

        public void MoveLeft()
        {
            MarioPowerupStateType powerupStateType = this.CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Dead)
            {
                CurrentActionState.IdleTransition();
            }
            CurrentActionState.TurnLeft();
        }

        public void MoveRight()
        {
            MarioPowerupStateType powerupStateType = this.CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Dead)
            {
                CurrentActionState.IdleTransition();
            }
            CurrentActionState.TurnRight();
        }

        public void Crouch()
        {
            MarioPowerupStateType powerupStateType = this.CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Dead)
            {
                CurrentActionState.IdleTransition();
            }
            CurrentActionState.CrouchingTransition();
        }

        public void Attack()
        {
            MarioPowerupStateType powerupStateType = this.CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Dead)
            {
                CurrentActionState.IdleTransition();
            }
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

            MarioPowerupStateType powerupStateType = this.CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Dead)
            {
                timeSpent += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(timeSpent > 3f)
                {
                    timeSpent = 0f;
                    ICommand reset = new Reset(game);
                    reset.Execute();
                }
            }
            if(this.Position.Y > 800)
            {
                timeSpent += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeSpent >= 3f)
                {
                    timeSpent = 0f;
                    ICommand reset = new Reset(game);
                    reset.Execute();
                }
            }


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
