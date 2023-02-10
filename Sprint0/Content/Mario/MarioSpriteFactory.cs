using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Content.Mario;
using Sprint0.Content;
using System;
using System.Collections.Generic;

namespace Sprint0.Content.Mario
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
            return null;
            
        }
        
     
    }
}