using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Content;
using Sprint0.MarioPlayer.State.ActionState;
using Sprint0.MarioPlayer.State.PowerupState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.MarioPlayer
{
    public class PlayerFactory
    {
        private ContentManager content;
        string spritesLocation;
        public Texture2D texture;
        bool faceToRight = false;


        public PlayerFactory(ContentManager contentIn)
        {         
            this.content = contentIn;
        }

        public Sprite buildSprites(MarioPowerupStateType powerUpType, MarioActionStateType actionType)
        {   
            findLocation(powerUpType, actionType);
            texture = content.Load<Texture2D>(spritesLocation);
            if (faceToRight)
            {
                return new NoneMovingAnimatedSprite(texture, Vector2.Zero, 1, 3);
            }
            else
            {
                return new NoneAnimatedNonMovingSprite(texture, Vector2.Zero, 1, 1);
            }

        }

        private void findLocation(MarioPowerupStateType powerUpType, MarioActionStateType actionType)
        {
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

            spritesLocation = fileNamePrefix + "/" + fileNamePrefix + fileNameSuffix;
            if(powerUpType == MarioPowerupStateType.Dead)
            {
                spritesLocation = "DeadMario/MarioDeath";
            }
            if (fileNameSuffix == "WalkRight")
            {
                faceToRight = true;
            }

        }
    }
}
