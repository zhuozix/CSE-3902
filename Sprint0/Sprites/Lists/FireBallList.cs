using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.NPC.Item;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Sprites.Lists
{
    public class FireBallList : IList
    {
        private ArrayList list;
        bool timesUP = false;    
        public FireBallList() {
            this.list = new ArrayList();
        }

        public void add(ISprite sprite)
        {
            if(this.size() == 3)
            {
                this.list.RemoveAt(0);
            }
            list.Add(sprite);
        }

        public int size() { return list.Count; }

        public void Update(GameTime gameTime)
        {
            foreach (FireBallSprite sprite in this.list)
            {
                if(sprite.time >= 2f)
                {
                    timesUP = true;
                }
            }

            if (timesUP)
            {
                this.list.RemoveAt(0);
                timesUP = false;
            }

            foreach (FireBallSprite sprite in this.list)
            {
                sprite.Update(gameTime);
            }

        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach (FireBallSprite sprite in this.list)
            {
                sprite.Draw(_spriteBatch, sprite.isFliped);
            }
        }
    }
}
