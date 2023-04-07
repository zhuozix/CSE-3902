using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Command.GameControlCMD;
using Sprint0.Command.PlayerCMD;
using Sprint0.Content;
using Sprint0.MarioPlayer;
using Sprint0.MarioPlayer.State.PowerupState;
using Sprint0.NPC.Blocks;
using Sprint0.NPC.Enemy;
using Sprint0.NPC.Fireball;
using Sprint0.Sprites;
using Sprint0.Sounds;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Sprint0.UI.State;
using Sprint0.UI.Title;

namespace Sprint0.ObjectManager
{
    public class GameObjectManager
    {
        public List<ISprite> blocks;
        public List<ISprite> enemies;
        public List<ISprite> players;
        public List<ISprite> items;
        public List<ISprite> fireBallList;
        public List<Teleporter> teleporters;
        public List<FlagPoleHitbox> flagpoles;
        public Texture2D background;
        public Game1 game;
        private ObjectLogicManager logic;
        //private Viewport viewport;

         
        public GameObjectManager(Game1 gameInstance) 
        { 
            this.blocks= new List<ISprite>();
            this.enemies= new List<ISprite>();
            this.players= new List<ISprite>();
            this.items= new List<ISprite>();
            this.fireBallList = new List<ISprite>();
            this.teleporters = new List<Teleporter>();
            this.flagpoles = new List<FlagPoleHitbox>();
            game = gameInstance;
            //this.viewport = gameInstance.viewport;
            this.logic = new ObjectLogicManager(this);
        }

        public void addObject(ISprite obj, String objectType)
        {
            switch (objectType) {

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
                    this.fireBallList.Add(obj);
                    break;
                default:
                    break;
            }
        }

		public void addFlagPoleHitbox(FlagPoleHitbox fp_hb)
		{
			this.flagpoles.Add(fp_hb);
		}

		public void addTeleporter(Teleporter warp)
        {
            this.teleporters.Add(warp);
        }

        public void addBackground(Texture2D background)
        {
            this.background = background;
        }



        public void update(GameTime time)
        {

            this.logic.update(time);

            foreach (ISprite obj in this.blocks)
            {
                obj.Update(time);
            }
            foreach (ISprite obj in this.enemies)
            {
                obj.Update(time);
            }
            foreach (Mario obj in this.players)
            {
               
                obj.Update(time);
            }
            foreach (ISprite obj in this.items)
            {
                obj.Update(time);
            }
            foreach (ISprite obj in this.fireBallList)
            {
                obj.Update(time);
            }

            game.time -= (float)time.ElapsedGameTime.TotalSeconds;
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
                bool filppedEnemy;
                if (obj.velocity.X < 0)
                {
                    filppedEnemy = true;
                }
                else
                {
                    filppedEnemy = false;
                }
                obj.Draw(_spriteBatch, filppedEnemy);
            }
            foreach (ISprite obj in this.players)
            {
                obj.Draw(_spriteBatch, isFlipped);
            }
            foreach (ISprite obj in this.items)
            {
                obj.Draw(_spriteBatch, isFlipped);
            }
            foreach (ISprite obj in this.fireBallList)
            {
                obj.Draw(_spriteBatch, isFlipped);
            }
        }
    }
}
