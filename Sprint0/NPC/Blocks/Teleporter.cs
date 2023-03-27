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

        public Teleporter(Rectangle box, String activator, int xDest, int yDest)
        {
            this.box = box;
            this.activator = activator;
            this.xDest = xDest;
            this.yDest = yDest;
        }

        public void teleportPlayer(Mario receiver)
        {
            receiver.Position = new Vector2(xDest, yDest);
        }
    }
}
