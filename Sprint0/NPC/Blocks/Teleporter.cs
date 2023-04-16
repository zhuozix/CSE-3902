using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer;
using Sprint0.MarioPlayer.State.ActionState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.NPC.Blocks
{
    public class Teleporter
    {
        public Rectangle box { get; set; }
        public String activator { get; set; }
        public int xDest { get; set; }
        public int yDest { get; set; }
        public Boolean underground { get; set; }

        public string destDir {  get; set; }

        public Teleporter(Rectangle box, String activator, int xDest, int yDest, bool underground, string destDir)
        {
            this.box = box;
            this.activator = activator;
            this.xDest = xDest;
            this.yDest = yDest;
            this.underground = underground;
            this.destDir = destDir;
        }

        public void teleportPlayer(Mario receiver)
        {
            if (underground && receiver.CurrentActionState.GetEnumValue() != MarioActionStateType.Piping) Sounds.SoundPlayer.playPipe();


            IMarioActionState currentState = new MarioPipeState(receiver, receiver.marioFactory, new Vector2(xDest, yDest), activator, destDir);
            currentState.Enter(receiver.CurrentActionState);

           
            
            /*
            if (activator.Equals("down"))
            {
                
                float yPos = receiver.Position.Y;
                while (receiver.Position.Y < yPos - 32)
                {
                    receiver.Update();
                    receiver.Draw();
                }
            }*/
            
            

        }
    }
}
