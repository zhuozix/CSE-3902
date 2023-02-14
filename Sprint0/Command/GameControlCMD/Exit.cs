﻿using Sprint0.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Command.GameControlCMD
{
    public class Exit : ICommand
    {
        Game1 game;

        public Exit(Game1 gameInstance) {
            this.game = gameInstance;
        }
        public void Execute()
        {
            this.game.Exit();
        }
    }
}
