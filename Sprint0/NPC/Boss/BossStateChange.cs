using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.NPC.Boss
{
    public class BossStateChange
    {
        public Boss boss;
        public Game1 game;

        public BossStateChange(Boss boss, Game1 game)
        {
            this.boss = boss;
            this.game = game;
        }

        public void toIdleState()
        {
            boss.currentActionType = BossActionType.Idle;
            boss.changeSprite();
        }

        public void toRunningState()
        {

        }

        public void hitByMario() 
        {
            game.bossHP -= 10;
        }

        public void hitByFireball() 
        {
            game.bossHP--;
        } 
    }
}
