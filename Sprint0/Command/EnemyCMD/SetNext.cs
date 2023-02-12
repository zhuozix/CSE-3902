using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Content
{
    public class SetNext : ICommand
    {
        private Game1 game;
        private int displayBlock;

        // display the sprite one at a time
        public SetNext(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            int n = game.DisplayEnemy;
            displayBlock = n + 1;
            if (displayBlock > 1)
            {
                displayBlock = 0;
            }
            game.DisplayEnemy = displayBlock;

        }
    }
}
