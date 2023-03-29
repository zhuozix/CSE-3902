using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.NPC.HUD
{
    public class Hud
    {
        private SpriteFont font;
        public Hud()
        {
            ContentManager content = Game1.Instance.Content;
            font = content.Load<SpriteFont>("font");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "LIVES: " + Game1.Instance.life,new Vector2(50,100), Color.White);
            spriteBatch.DrawString(font, "COINS: " + Game1.Instance.coins, new Vector2(600,100), Color.White);
        }
    }
}
