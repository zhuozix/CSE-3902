using Sprint0.MarioPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Command.PlayerCMD
{
    internal class MaioStateChange
    {
    }

    internal class MarioNormalCheatCommand : MarioReceiver<Mario>
    {
        public MarioNormalCheatCommand(Mario receiver)
            : base(receiver)
        { }

        public override void Execute()
        {
            receiver.RevertToNormal();
        }
    }

    internal class MarioSuperCheatCommand : MarioReceiver<Mario>
    {
        public MarioSuperCheatCommand(Mario receiver)
            : base(receiver)
        { }

        public override void Execute()
        {
            receiver.UseSuperMushroom();
        }
    }

    internal class MarioFireCheatCommand : MarioReceiver<Mario>
    {
        public MarioFireCheatCommand(Mario receiver)
            : base(receiver)
        { }

        public override void Execute()
        {
            receiver.UseFireMushroom();
        }
    }

    internal class MarioDamageCheatCommand : MarioReceiver<Mario>
    {
        public MarioDamageCheatCommand(Mario receiver)
            : base(receiver)
        { }

        public override void Execute()
        {
            receiver.TakeDamage();
        }
    }
}
