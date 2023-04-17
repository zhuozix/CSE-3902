using Sprint0.MarioPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.NPC.Boss.AI
{
    public class CommonLogic
    {
        public Mario player;
        public Boss boss;
        public CommonLogic(Mario player, Boss boss) 
        {
            this.player = player;
            this.boss = boss;
        }

        public void findPlayerDirection() 
        {
            if(player.Position.X > boss.Position.X)
            {
                boss.isFacingRight = true;
            }
            else
            {
                boss.isFacingRight = false;
            }

        }

        public bool marioOnGround()
        {
            if(player.Position.Y > 350)
            {
                return true;
            }
            return false;
        }
        public void findPlayerCurrentLevel() { }

    }
}
