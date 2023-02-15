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
    public class IncreaseBlockIndex : ICommand
    {
		private Game1 game;
		public IncreaseBlockIndex(Game1 game)
		{
			this.game = game;
		}

		public void Execute()
		{
			if (game.DisplayBlock < 5)
			{
				game.DisplayBlock = game.DisplayBlock + 1;
			}
			else
			{
				game.DisplayBlock = 0;
			}

		}
	}

	public class decreaseBlockIndex : ICommand
	{
		private Game1 game;

		public decreaseBlockIndex(Game1 game)
		{
			this.game = game;
		}

		public void Execute()
		{
			if (game.DisplayBlock > 0)
			{
				game.DisplayBlock = game.DisplayBlock - 1;
			}
			else
			{
				game.DisplayBlock = game.blockList.Count;
			}

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
