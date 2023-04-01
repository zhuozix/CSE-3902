using Sprint0.Content;
using Sprint0.Sounds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Command.GameControlCMD
{
    public class gamePause : ICommand
    {
        Game1 game;

        public gamePause(Game1 gameInstance)
        {
            this.game = gameInstance;
        }

        public void Execute()
        {
            if (!game.IsPaused)
            {
       
                game.IsPaused = true;
                SoundPlayer.PauseMusic();
            }
            else
            {
                game.IsPaused = false;
                SoundPlayer.ResumeMusic();
            }
        }
    }
}
