using Sprint0.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Command.PlayerCMD
{
    public abstract class MarioReceiver<ReceiverType> : ICommand
    {
        internal ReceiverType receiver;

        public MarioReceiver(ReceiverType receiverIn) 
        {
            receiver = receiverIn;
        }
        public abstract void Execute();
    }
}
