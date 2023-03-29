using Sprint0.Content;
using Sprint0.UI.Title;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Command.GameControlCMD
{
    public class Reset : ICommand
    {
        Game1 game;

        public Reset(Game1 gameInstance) {
            this.game = gameInstance;
        }
        public void Execute()
        {
            game.coins = 0;
            game.life = 3;
            game.time = 400f;
            game.ChangeState(new TitleScreenState(game));
        }
    }
}
