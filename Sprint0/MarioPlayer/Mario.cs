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
using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Sprint0.Sounds;
using Sprint0.NPC.Blocks;

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
        public int columns = 1;

        private bool isVisible = true;
        private float visibleTimer = 0f;



        public Mario(Vector2 spawnLocation,Game1 gameInstance)
        {
            game= gameInstance;
            marioFactory = new MarioFactory(gameInstance.Content, this);
            Sprite = marioFactory.buildSprites(MarioPowerupStateType.Normal, MarioActionStateType.Idle);
            this.fireballFactory = gameInstance.spritesFactory;
            this.fireBallList= gameInstance.fireBallList;
            Position = spawnLocation;
            velocity = new Vector2(0,150);
            Acceleration = Vector2.Zero;
            ySpawnPosition = spawnLocation.Y;
            this.cols = 1;
            this.rows = 1;
            CurrentActionState = new MarioIdleState(this, marioFactory);
            CurrentPowerupState = new MarioNormalState(this, marioFactory);

            CurrentActionState.Enter(null);
            CurrentPowerupState.Enter(null);

            IsFacingRight = true;

            gameObjectManager = gameInstance.gameObjectManager;
            crash = false;
            state = "Normal";
        }

        public void generateFireball()
        {           
            ISprite fireball = fireballFactory.getFireballSprite(Position, IsFacingRight);
            if(this.velocity.X != 0)
            {
                fireball.velocity = new Vector2((float)(fireball.velocity.X * 1.5), fireball.velocity.Y);
            }
            SoundPlayer.playFireball();
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

            if (CurrentActionState.GetEnumValue() == MarioActionStateType.Running ||
                    CurrentActionState.GetEnumValue() == MarioActionStateType.Idle)
            {
                if (powerupStateType == MarioPowerupStateType.Normal) SoundPlayer.playJumpSmall();
                if (powerupStateType == MarioPowerupStateType.Super ||
                    powerupStateType == MarioPowerupStateType.Fire) SoundPlayer.playJumpSuper();
            }

            
            {
                
            }

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
            foreach (Teleporter warp in game.gameObjectManager.teleporters)
            {
                Rectangle teleport = warp.box;
                Rectangle player = new Rectangle((int)base.Position.X, (int)base.Position.Y, base.Width * Game1.scale, base.Height * Game1.scale);
                if (player.Intersects(teleport) && warp.activator.Equals("down"))
                {
                    warp.teleportPlayer(this);
                    break;
                }
            }
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
            MarioPowerupStateType powerupStateType = CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Super)
            {
                Position = new Vector2(Position.X, Position.Y + 20);
                velocity = new Vector2(velocity.X, velocity.Y + 20);
            }
            CurrentPowerupState.NormalMarioTransition();
        }

        public void UseSuperMushroom()
        {
            MarioPowerupStateType powerupStateType = CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Normal)
            {
                Position = new Vector2(Position.X, Position.Y - 30);
                velocity = new Vector2(velocity.X, velocity.Y + 20 );
            }
                
            CurrentPowerupState.SuperMarioTransition();
        }

        public void UseFireMushroom()
        {
            MarioPowerupStateType powerupStateType = CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Normal)
            {
                Position = new Vector2(Position.X, Position.Y - 30);
                velocity = new Vector2(velocity.X, velocity.Y + 20);
            }
            CurrentPowerupState.FireMarioTransition();
        }

        public void TakeDamage()
        {
            MarioPowerupStateType powerupStateType = CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Super)
            {
                Position = new Vector2(Position.X, Position.Y + 30);
                velocity = new Vector2(velocity.X, velocity.Y + 20);
            }
            CurrentPowerupState.TakeDamage();
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            if(this.state == "Hurt")
            {
                visibleTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(visibleTimer >= 0.4f)
                {
                    visibleTimer = 0f;
                    changeVisibleStatus();
                }
            } else if (this.state == "Star")
            {
                visibleTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (visibleTimer >= 0.1f)
                {
                    visibleTimer = 0f;
                    changeVisibleStatus();
                }
            }
            else
            {
                this.isVisible = true;
            }
            this.cols = columns;
            this.rows = 1;
            CurrentActionState.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, bool isFlipped)
        {
            if (isVisible){
                
                base.Draw(spriteBatch, !IsFacingRight);
            }
            
        }

        private void changeVisibleStatus()
        {
           if (isVisible) { isVisible = false; }
            else { isVisible= true; }
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
