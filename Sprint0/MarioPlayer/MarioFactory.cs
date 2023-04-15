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
    public class MarioFactory
    {
        private ContentManager content;
        string spritesLocation;
        public Texture2D texture;
        private MarioActionStateType actionType;
        Mario player;

        public MarioFactory(ContentManager contentIn, Mario player)
        {
            this.content = contentIn;
            this.player = player;
        }

        public Sprite buildSprites(MarioPowerupStateType powerUpType, MarioActionStateType actionType)
        {
            findLocation(powerUpType, actionType);
            texture = content.Load<Texture2D>(spritesLocation);
            this.actionType = actionType;
            return spritesGenerator();

        }

        public Sprite spritesGenerator()
        {
            Sprite ans;
           
                if(this.actionType == MarioActionStateType.Running ||this.actionType == MarioActionStateType.Win)
                {
                   ans = new NoneMovingAnimatedSprite(texture, Vector2.Zero, 1, 3);
                player.columns = 3;
                player.cols = 3;
                }else if (this.actionType == MarioActionStateType.PoleSliding)
			{
				ans = new NoneMovingAnimatedSprite(texture, Vector2.Zero, 1, 2);
				player.columns = 1;
                player.cols = 1;
			}
                else {
                   ans = new NoneAnimatedNonMovingSprite(texture, Vector2.Zero, 1, 1);
                player.columns = 1;
                player.cols = 1;
                }
                
         
           
            return ans;
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
                    case MarioActionStateType.PoleSliding:
						fileNameSuffix = "RightFlagPole";
						break;
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
                    case MarioActionStateType.Win:
                        fileNameSuffix = "WalkRight";
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

        }
    }
}
