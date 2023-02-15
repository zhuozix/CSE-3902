using Sprint0.MarioPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Command.PlayerCMD
{
    internal class MarioIdle : MarioReceiver<Mario>
    {
        public MarioIdle(Mario receiver)
            : base(receiver)
        { }

        public override void Execute()
        {
            receiver.idle();
        }
    }
}
