using Microsoft.Xna.Framework;
using Sprint0.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Command
{

    public class CommandExit : ICommand
    {
        private Game game;
        public CommandExit(Game game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Exit();
        }
    }
    public class CommandList : ICommand
    {
        private Game1 game;
        private int displaySprite;
        // display the sprite one at a time
        public CommandList(Game1 game, int displaySprite)
        {
            this.game = game;
            this.displaySprite = displaySprite;
        }

        public void Execute()
        {
            game.DisplaySprite = displaySprite;
        }
    }

    // Adam Sprint 2
    public class SetBlockIndex : ICommand
    {
        private Game1 game;
        private int displayBlock;
        // display the sprite one at a time
        public SetBlockIndex(Game1 game, int blockDisplaySprite)
        {
            this.game = game;
            displayBlock = blockDisplaySprite;
        }

        public void Execute()
        {
            game.DisplayBlock = displayBlock;
        }
    }
    //

    // Seth Sprint 2

    public class increaseItemIndex : ICommand
    {
        private Game1 game;

        public increaseItemIndex(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            if (game.DisplayItem < 4)
            {
                game.DisplayItem = game.DisplayItem + 1;
            }
            else
            {
                game.DisplayItem = 0;
            }

        }
    }

    public class decreaseItemIndex : ICommand
    {
        private Game1 game;

        public decreaseItemIndex(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            if (game.DisplayItem > 0)
            {
                game.DisplayItem = game.DisplayItem - 1;
            }
            else
            {
                game.DisplayItem = 4;
            }

        }
    }


}
