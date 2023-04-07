using Microsoft.Xna.Framework;
using Sprint0.Factory;
using Sprint0.MarioPlayer;
using Sprint0.MarioPlayer.State.PowerupState;
using Sprint0.NPC.Blocks;
using Sprint0.NPC.Item;
using Sprint0.ObjectManager;
using Sprint0.Sounds;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Sprint0.NPC.StateChange
{
    class BlockChangeManager : IStateChangeManager
    {
        public ISprite blockSprite;
        SpritesFactory factory;
        GameObjectManager objManager;
        Game1 game;
        Random rand = new Random();
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
                case "InvisibleBlock":
                    InvisibleBlockTransition(); break;
                default: 
                    // No Change
                    break;
            }
        }

        private void InvisibleBlockTransition()
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
            generateGreenMush(futureSprite);
                objManager.addObject(futureSprite, "block");
            

        }

        private void CoinBlockTransition()
        {

            int randomNumber = rand.Next(1, 5);
            if (randomNumber == 1)
            {
                //Feeling lucky
                generateCoin(blockSprite);
                game.mario.Position = new Vector2(game.mario.Position.X, game.mario.Position.Y + 10);

            } else
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
                //generateStar(futureSprite);
                generateItem(futureSprite);
                objManager.addObject(futureSprite, "block");
            }
            
        }

        private void brickBlockTransition()
        {
            MarioPowerupStateType powerupStateType = game.mario.CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Normal)
            {
                foreach (BlockSprite target in objManager.blocks)
                {
                    if (blockSprite.Equals(target))
                    {
                        SoundPlayer.playBump();
                        target.originalPosition = target.Position;
                        target.velocity = new Vector2(0, -3);
                        break;
                    }
                }
            }
            else if(powerupStateType != MarioPowerupStateType.Dead)
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
                    int randomNumber = rand.Next(1, 20);
                    if (randomNumber == 1)
                    {
                        generateGreenMush(futureSprite);
                    } 
                    else if(randomNumber == 2)
                    {
                        generateStar(futureSprite);
                    }
                    else
                    {
                        generateCoin(futureSprite);
                    }
                    
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
                    int randomNumber = rand.Next(1, 20);
                    if (randomNumber == 1)
                    {
                        generateGreenMush(futureSprite);
                    }
                    else if (randomNumber == 2)
                    {
                        generateStar(futureSprite);
                    }
                    else
                    {
                        generateCoin(futureSprite);
                    }
                }
                else
                {
                    generateFireFlower(futureSprite);
                }
            }
            else if (powerupStateType != MarioPowerupStateType.Dead)
            {
                int randomNumber = rand.Next(1, 10);
                if (randomNumber == 1)
                {
                    generateGreenMush(futureSprite);
                }
                else if (randomNumber == 2)
                {
                    generateStar(futureSprite);
                }
                else
                {
                    generateCoin(futureSprite);
                }
            }


        }

        private void generateCoin(ISprite spriteIn)
        {
            ISprite ans = factory.getCoinSprite();
            ans.Position = new Vector2(spriteIn.Position.X + 10, spriteIn.Position.Y - 35);
            ans.state = "Temp";
            ans.velocity = new Vector2(0, -100);
            SoundPlayer.playCoin();
            objManager.addObject(ans, "item");
        }

        private void generateRedMush(ISprite spriteIn)
        {
            ISprite ans = factory.getRedMushSprite();
            ans.Position = new Vector2(spriteIn.Position.X, spriteIn.Position.Y - 35);
            SoundPlayer.playPowerupAppears();
            objManager.addObject(ans, "item");
        }

        private void generateGreenMush(ISprite spriteIn)
        {
            ISprite ans = factory.getGreenMushSprite();
            ans.Position = new Vector2(spriteIn.Position.X, spriteIn.Position.Y - 35);
            SoundPlayer.playPowerupAppears();
            objManager.addObject(ans, "item");
        }

        private void generateFireFlower(ISprite spriteIn)
        {
            ISprite ans = factory.getFireFlowerSprite();
            ans.Position = new Vector2(spriteIn.Position.X - 3, spriteIn.Position.Y - 35);
            SoundPlayer.playPowerupAppears();
            objManager.addObject(ans, "item");
        }

        private void generateStar(ISprite spriteIn)
        {
            ISprite ans = factory.getStarSprite();
            ans.Position = new Vector2(spriteIn.Position.X -3 , spriteIn.Position.Y - 35);
            SoundPlayer.playPowerupAppears();
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
