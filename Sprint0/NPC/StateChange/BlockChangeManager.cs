using Sprint0.Factory;
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

        public BlockChangeManager(ISprite enterSprites, SpritesFactory factoryIn, GameObjectManager objManagerIn)
        {
            blockSprite = enterSprites;
            factory = factoryIn;
            objManager = objManagerIn;
            
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
                case "FloorBlock":
                // No Change
                case "StairBlock":
                // No change
                case "UsedBlock":
                // No change               
                default: break;
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
            objManager.addObject(futureSprite, "block");
        }

        private void brickBlockTransition()
        {
            foreach (ISprite target in objManager.blocks)
            {
                if (blockSprite.Equals(target))
                {
                    objManager.blocks.Remove(target);
                    break;
                }
            }
        }
    }
}
