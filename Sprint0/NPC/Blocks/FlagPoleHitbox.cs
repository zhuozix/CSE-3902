using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Content;
using Sprint0.MarioPlayer;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.NPC.Blocks
{
	public class FlagPoleHitbox
	{ 
		public Rectangle box { get; set; }

		public FlagPoleHitbox(Rectangle box)
		{
			this.box = box;

		}

	}
}
