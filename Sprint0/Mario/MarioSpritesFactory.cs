using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Sprint0.Content;
using Sprint0.Mario.State.PowerupState;
using Sprint0.Mario.State.ActionState;

namespace Sprint0.Mario
{
    public class MarioSpriteFactory
    {
        private Dictionary<MarioPowerupStateType, Dictionary<MarioActionStateType, Sprite>> cache;
        private static MarioSpriteFactory instance;

        public static MarioSpriteFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MarioSpriteFactory();
                }
                return instance;
            }
        }

        private MarioSpriteFactory()
        {
            cache = new Dictionary<MarioPowerupStateType, Dictionary<MarioActionStateType, Sprite>>();
            foreach (MarioPowerupStateType powerupState in Enum.GetValues(typeof(MarioPowerupStateType)))
            {
                cache.Add(powerupState, new Dictionary<MarioActionStateType, Sprite>());
            }
        }

        public Sprite BuildSprite(MarioPowerupStateType powerupState, MarioActionStateType actionState)
        {
            if (cache[powerupState].ContainsKey(actionState))
                return cache[powerupState][actionState];

            string fileNamePrefix;
            switch (powerupState)
            {
                case MarioPowerupStateType.Normal:
                    fileNamePrefix = "NormalMario/Mario";
                    break;
                case MarioPowerupStateType.Super:
                    fileNamePrefix = "SuperMario/SuperMario";
                    break;
                case MarioPowerupStateType.Fire:
                    fileNamePrefix = "FireMario/FireMario";
                    break;
                case MarioPowerupStateType.Dead:
                    fileNamePrefix = "NormalMario/MarioDeath";
                    break;
                default:
                    throw new ArgumentException("MarioSpriteFactory error: Invalid MarioPowerupStateType specified");
            }

            string fileNameSuffix;
            if (powerupState != MarioPowerupStateType.Dead)
                switch (actionState)
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


            Sprite newSprite;
            if (fileNameSuffix == "WalkRight")
                newSprite = new NoneMovingAnimatedSprite(Game1.Instance.Content.Load<Texture2D>(fileNamePrefix + fileNameSuffix), Vector2.Zero, 1, 3);
            else
                newSprite = new NoneAnimatedNonMovingSprite(Game1.Instance.Content.Load<Texture2D>(fileNamePrefix + fileNameSuffix), Vector2.Zero, 1, 1);
            cache[powerupState].Add(actionState, newSprite);
            return newSprite;
        }

        public void ClearCache()
        {
            foreach (Dictionary<MarioActionStateType, Sprite> subCache in cache.Values)
            {
                subCache.Clear();
            }
            cache.Clear();
        }
    }
}
