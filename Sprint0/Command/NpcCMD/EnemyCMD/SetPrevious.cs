using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Content;

namespace Sprint0.Command.NpcCMD.EnemyCMD
{
    // Zhuozi Sprint 2
    public class SetPrevious : ICommand
    {
        private Game1 game;
        private int displayBlock;
        // display the sprite one at a time
        public SetPrevious(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            int n = game.DisplayEnemy;
            displayBlock = n - 1;
            if (displayBlock < 0)
            {
                displayBlock = 1;
            }
            game.DisplayEnemy = displayBlock;

        }
    }
}
