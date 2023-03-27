using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.NPC.Blocks
{
    public class Teleporter
    {
        private Rectangle box { get; set; }
        private String activator { get; set; }
        int xDest { get; set; }
        int yDest { get; set; }

        public Teleporter(Rectangle box, String activator, int xDest, int yDest)
        {
            this.box = box;
            this.activator = activator;
            this.xDest = xDest;
            this.yDest = yDest;
        }
    }
}
