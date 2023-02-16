using Sprint0.MarioPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Command.PlayerCMD
{
 
    internal class MarioJumpCommand : MarioReceiver<Mario>
    {
        public MarioJumpCommand(Mario receiver)
            : base(receiver)
        { }

        public override void Execute()
        {
            receiver.Jump();
            
        }
    }

    internal class MarioMoveLeftCommand : MarioReceiver<Mario>
    {
        public MarioMoveLeftCommand(Mario receiver)
            : base(receiver)
        { }

        public override void Execute()
        {
            receiver.MoveLeft();
        }
    }

    internal class MarioCrouchCommand : MarioReceiver<Mario>
    {
        public MarioCrouchCommand(Mario receiver)
            : base(receiver)
        { }

        public override void Execute()
        {
            receiver.Crouch();
        }
    }

    internal class MarioMoveRightCommand : MarioReceiver<Mario>
    {
        public MarioMoveRightCommand(Mario receiver)
            : base(receiver)
        { }

        public override void Execute()
        {
            receiver.MoveRight();
        }
    }

    internal class fireFireball : MarioReceiver<Mario>
    {
        public fireFireball(Mario receiver) : base(receiver) { }

        public override void Execute()
        {
            receiver.Attack();
        }
    }

    internal class fallAfterJump : MarioReceiver<Mario> 
    {
        public fallAfterJump(Mario receiver) : base(receiver) { }

        public override void Execute()
        {
            receiver.fallAfterJump();
        }
    }
}
