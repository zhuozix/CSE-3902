using Microsoft.Xna.Framework;
using Sprint0.Factory;
using Sprint0.MarioPlayer;
using Sprint0.MarioPlayer.State.PowerupState;
using Sprint0.NPC.Item;
using Sprint0.ObjectManager;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.NPC.StateChange
{
    class BlockChangeManager : IStateChangeManager
    {
        public ISprite blockSprite;
        SpritesFactory factory;
        GameObjectManager objManager;
        Game1 game;
        public BlockChangeManager(ISprite enterSprites, Game1 gameInsstance)
        {
            blockSprite = enterSprites;
            factory = gameInsstance.spritesFactory;
            objManager = gameInsstance.gameObjectManager;
            game = gameInsstance;
            
        }
        public void changeState()
        {
            blockTransition();
        }

        private void blockTransition()
        {
            switch (blockSprite.Name)
            {
                case "CoinBlock":
                    CoinBlockTransition(); break;
                case "BrickBlock":
                    brickBlockTransition(); break;              
                default: 
                    // No Change
                    break;
            }
        }

        private void CoinBlockTransition()
        {
            ISprite futureSprite = factory.getUsedBlockSprite();
            futureSprite.Position = blockSprite.Position;
            foreach (ISprite target in objManager.blocks)
            {
                if (blockSprite.Equals(target))
                {
                    objManager.blocks.Remove(target);
                    break;
                }
            }
            generateItem(futureSprite);
            objManager.addObject(futureSprite, "block");
        }

        private void brickBlockTransition()
        {
            foreach (ISprite target in objManager.blocks)
            {
                if (blockSprite.Equals(target))
                {
                    brickCrash(target);
                    objManager.blocks.Remove(target);
                    
                    break;
                }
            }
        }

        private void brickCrash(ISprite targetSprite)
        {
            ISprite futureSprite = factory.getShatteredBlock();
            futureSprite.Position = targetSprite.Position;
            objManager.addObject(futureSprite, "block");

        }

        private void generateItem(ISprite futureSprite)
        {
            Mario marioEntity = game.mario;
            /*
             * get mario power state
             */
            MarioPowerupStateType powerupStateType = marioEntity.CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Normal)
            {
                //generate redmush or coin
                if (hasRedMush())
                {
                    generateCoin(futureSprite);
                }
                else
                {
                    generateRedMush(futureSprite);
                }
            }
            else if (powerupStateType == MarioPowerupStateType.Super)
            {
                if (hasFireFlower())
                {
                    generateCoin(futureSprite);
                }
                else
                {
                    generateFireFlower(futureSprite);
                }
            }
            else if (powerupStateType != MarioPowerupStateType.Dead)
            {
                generateCoin(futureSprite);
            }


        }

        private void generateCoin(ISprite spriteIn)
        {
            ISprite ans = factory.getCoinSprite();
            ans.Position = new Vector2(spriteIn.Position.X + 10, spriteIn.Position.Y - 35);
            objManager.addObject(ans, "item");
        }

        private void generateRedMush(ISprite spriteIn)
        {
            ISprite ans = factory.getRedMushSprite();
            ans.Position = new Vector2(spriteIn.Position.X, spriteIn.Position.Y - 35);
            objManager.addObject(ans, "item");
        }

        private void generateFireFlower(ISprite spriteIn)
        {
            ISprite ans = factory.getFireFlowerSprite();
            ans.Position = new Vector2(spriteIn.Position.X - 3, spriteIn.Position.Y - 35);
            objManager.addObject(ans, "item");
        }

        private bool hasRedMush()
        {
            foreach(ISprite target in objManager.items)
            {
                if(target.Name == "RedMush")
                {
                    return true;
                }
            }
            return false;
        }

        private bool hasFireFlower()
        {
            foreach (ISprite target in objManager.items)
            {
                if (target.Name == "FireFlower")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
