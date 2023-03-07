using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;
using Sprint0.Sprites.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.ObjectManager
{
    public class GameObjectManager
    {
        public List<ISprite> blocks;
        public List<ISprite> enemies;
        public List<ISprite> players;
        public List<ISprite> items;
        public FireBallList fireBallList;
        public Texture2D background;

        private float timerOfCrashedBlock = 0f;
        private float timerOfDeadEnemy = 0f;
        private bool hasDeadEnemy = false;
        private bool hasCrashedBlocks = false;

        public GameObjectManager() 
        { 
            this.blocks= new List<ISprite>();
            this.enemies= new List<ISprite>();
            this.players= new List<ISprite>();
            this.items= new List<ISprite>();
            this.fireBallList = new FireBallList();
        }

        public void addObject(ISprite obj, String objectType)
        {

            switch(objectType) {

                case "block":
                    this.blocks.Add(obj); 
                    break;
                case "enemy":
                    this.enemies.Add(obj);
                    break;
                case "player":
                    this.players.Add(obj);
                    break;
                case "item":
                    this.items.Add(obj);
                    break;
                case "fireBall":
                    this.fireBallList.add(obj);
                    break;
                default:
                    break;
            }
        }

        public void addBackground(Texture2D background)
        {
            this.background = background;
        }

        public void updateShatteredBlocks(GameTime time)
        {
            foreach (ISprite obj in this.blocks)
            {
                if (hasCrashedBlocks)
                {
                    break;
                }
                if (obj.state == "Crashed")
                {
                    hasCrashedBlocks = true;
                }
            }

            if (hasCrashedBlocks)
            {
                timerOfCrashedBlock += (float)time.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                timerOfCrashedBlock = 0f;
            }

            if(timerOfCrashedBlock >= 0.3f)
            {

                foreach (ISprite obj in this.blocks)
                {
                    if (obj.state == "Crashed")
                    {
                        this.blocks.Remove(obj);
                        break;
                    }
                }
                hasCrashedBlocks = false;
            }
        }
        public void updateDeadEnemy(GameTime time)
        {
            foreach (ISprite obj in this.enemies)
            {
                if (hasDeadEnemy)
                {
                    break;
                }
                if (obj.state == "Dead")
                {
                    hasDeadEnemy = true;
                }
            }

            if (hasDeadEnemy)
            {
                timerOfDeadEnemy += (float)time.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                timerOfDeadEnemy = 0f;
            }

            if (timerOfDeadEnemy >= 1.0f)
            {

                foreach (ISprite obj in this.enemies)
                {
                    if (obj.state == "Dead")
                    {
                        this.enemies.Remove(obj);
                        break;
                    }
                }
                hasDeadEnemy = false;
            }
        }

        private void deleteDropedItems()
        {
            foreach(ISprite obj in this.items)
            {
                if(obj.Position.Y >= 600)
                {
                    this.items.Remove(obj);
                    break;
                }
            }
        }

        private void deleteDropedEnemy()
        {
            foreach (ISprite obj in this.enemies)
            {
                if (obj.Position.Y >= 600)
                {
                    this.enemies.Remove(obj);
                    break;
                }
            }
        }

        public void update(GameTime time)
        {

            updateShatteredBlocks(time);
            updateDeadEnemy(time);
            deleteDropedEnemy();
            deleteDropedItems();
            foreach (ISprite obj in this.blocks)
            {
                obj.Update(time);
            }
            foreach (ISprite obj in this.enemies)
            {
                obj.Update(time);
            }
            foreach (ISprite obj in this.players)
            {
                obj.Update(time);
            }
            foreach (ISprite obj in this.items)
            {
                obj.Update(time);
            }
            this.fireBallList.Update(time);
        }

        public void Draw(SpriteBatch _spriteBatch, Boolean isFlipped)
        {
            
            _spriteBatch.Draw(this.background, new Rectangle(0,0,this.background.Width * Game1.scale,this.background.Height * Game1.scale), Color.White);
            

            foreach (ISprite obj in this.blocks)
            {
                obj.Draw(_spriteBatch, isFlipped);
            }
            foreach (ISprite obj in this.enemies)
            {
                obj.Draw(_spriteBatch, isFlipped);
            }
            foreach (ISprite obj in this.players)
            {
                obj.Draw(_spriteBatch, isFlipped);
            }
            foreach (ISprite obj in this.items)
            {
                obj.Draw(_spriteBatch, isFlipped);
            }
            this.fireBallList.Draw(_spriteBatch);
        }
    }
}
