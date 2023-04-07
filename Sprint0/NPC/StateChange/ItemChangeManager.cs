using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer;
using Sprint0.MarioPlayer.State.PowerupState;
using Sprint0.ObjectManager;
using Sprint0.ObjectManager.Sounds;
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
                    SoundPlayer.playPowerup();
                    starTransition(); break;
                case "RedMush":
                    SoundPlayer.playPowerup();
                    redMushTransition(); break;
                case "GreenMush":
                    SoundPlayer.playOneUp();
                    greenMushTransition(); break;
                case "Coin":
                    SoundPlayer.playCoin();
                    CoinTransition(); break;
                case "FireFlower":
                    SoundPlayer.playPowerup();
                    fireFlowerTransition(); break;
                default:
                    
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
            game1.mario.state = "Star";
            disapperTransition();
        }
        public void redMushTransition()
        {
            MarioPowerupStateType powerupStateType = player.CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Normal)
            {
                player.UseSuperMushroom();
            }
            disapperTransition();
        }
        public void greenMushTransition()
        {
            game1.life++;
            disapperTransition();
        }
        public void CoinTransition()
        {
            if(item.state != "Temp")
            {
                
            game1.coins++;
            if (game1.coins == 5)
            {
                game1.life++;
                game1.coins = 0;
            }

            }
            
            disapperTransition();
        }
        public void fireFlowerTransition()
        {
            MarioPowerupStateType powerupStateType = player.CurrentPowerupState.GetEnumValue();
            if (powerupStateType != MarioPowerupStateType.Fire)
            {
                player.UseFireMushroom();
            }
            disapperTransition();
        }
    }
}
