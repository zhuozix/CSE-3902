using Sprint0.Command.PlayerCMD;
using Sprint0.ObjectManager;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.NPC.StateChange
{
    internal class FireballChangeManager:IStateChangeManager
    {
        ISprite fireBall;
        GameObjectManager ObjManager;

        public FireballChangeManager(ISprite fireballIn, Game1 gameInstance) 
        {
            fireBall = fireballIn;
            ObjManager = gameInstance.gameObjectManager;
        }

        public void changeState()
        {
            deleteFireball();
        }

        private void deleteFireball() 
        {
            foreach(ISprite target in ObjManager.fireBallList)
            {
                if (fireBall.Equals(target))
                {
                    ObjManager.fireBallList.Remove(target);
                    break;
                }
            }
        }
    }
}
