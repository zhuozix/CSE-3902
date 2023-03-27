using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer;
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

        public Teleporter(Rectangle box, String activator, int xDest, int yDest, bool underground)
        {
            this.box = box;
            this.activator = activator;
            this.xDest = xDest;
            this.yDest = yDest;
            this.underground = underground;
        }

        public void teleportPlayer(Mario receiver)
        {
            Sounds.SoundPlayer.playPipe();
            receiver.Position = new Vector2(xDest, yDest);
        }
    }
}
