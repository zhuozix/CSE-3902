using Sprint0.MarioPlayer;
using Sprint0.MarioPlayer.State.PowerupState;
using Sprint0.ObjectManager;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.NPC.StateChange
{
     class ItemChangeManager : IStateChangeManager
    {
        ISprite item;
        GameObjectManager objManager;
        Mario player;
        Game1 game1;
        public ItemChangeManager(ISprite spriteIn,Game1 gameInstance) 
        {
            item = spriteIn;
            objManager = gameInstance.gameObjectManager;
            player = gameInstance.mario;
            game1 = gameInstance;
        }


        public void changeState()
        {
            itemTransition();
        }

        public void itemTransition()
        {
            switch (item.Name)
            {
                case "Star": 
                    starTransition(); break;
                case "RedMush": 
                    redMushTransition(); break;
                case "GreenMush":
                    greenMushTransition(); break;
                case "Coin":
                    CoinTransition(); break;
                case "FireFlower":
                    fireFlowerTransition(); break;
                default:
                    disapperTransition();
                    break;
            }
        }

        public void disapperTransition()
        {
            foreach(ISprite target in objManager.items)
            {
                if (item.Equals(target))
                {
                    objManager.items.Remove(target);
                    break;
                }
            }
        }

        public void starTransition()
        {

        }
        public void redMushTransition()
        {
            MarioPowerupStateType powerupStateType = player.CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Normal)
            {
                player.UseSuperMushroom();
            }
        }
        public void greenMushTransition()
        {
            game1.life++;
        }
        public void CoinTransition()
        {
            game1.coins++;
        }
        public void fireFlowerTransition()
        {
            MarioPowerupStateType powerupStateType = player.CurrentPowerupState.GetEnumValue();
            if (powerupStateType != MarioPowerupStateType.Fire)
            {
                player.UseFireMushroom();
            }
        }
    }
}
