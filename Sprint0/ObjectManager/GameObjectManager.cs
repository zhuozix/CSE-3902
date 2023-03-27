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
        public Texture2D background;
        private Game1 game;
        private Viewport viewport;

        private float timerOfCrashedBlock = 0f;
        private float timerOfDeadEnemy = 0f;
        private bool hasDeadEnemy = false;
        private bool hasCrashedBlocks = false;
        private bool timeUp = false;
        private float starTimer = 0f;
        private bool hasStarMario = false;
        private float timeSpent = 0f;

        private bool haveHurt = false;
        private float hurtTimer = 0f;

        private bool hasTempCoin = false;
        private float tempCoinTimer = 0f;
         
        public GameObjectManager(Game1 gameInstance) 
        { 
            this.blocks= new List<ISprite>();
            this.enemies= new List<ISprite>();
            this.players= new List<ISprite>();
            this.items= new List<ISprite>();
            this.fireBallList = new List<ISprite>();
            this.teleporters = new List<Teleporter>();
            game = gameInstance;
            this.viewport = gameInstance.viewport;
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
                    this.fireBallList.Add(obj);
                    break;
                default:
                    break;
            }
        }

        public void addTeleporter(Teleporter warp)
        {
            this.teleporters.Add(warp);
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
                        SoundPlayer.playBlockBreak();
                        this.blocks.Remove(obj);
                        break;
                    }
                }
                hasCrashedBlocks = false;
            }
        }
        public void updateStarMario(GameTime time)
        {
            foreach (ISprite obj in this.players)
            {
                if (hasStarMario)
                {
                    break;
                }
                if (obj.state == "Star")
                {
                    hasStarMario = true;
                }
            }

            if (hasStarMario)
            {
                starTimer += (float)time.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                starTimer = 0f;
            }

            if (starTimer >= 10.0f)
            {

                foreach (ISprite obj in this.players)
                {
                    if (obj.state == "Star")
                    {
                        obj.state = "Normal";
                        break;
                    }
                }
                hasStarMario = false;
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

        private void fireBallLimit()
        {
                if (fireBallList.Count > 3)
                {
                    fireBallList.RemoveAt(0);
                }
                
            
        }

        private void fireBallTimesUp()
        {

            foreach (FireBallInstance sprite in fireBallList)
            {
                if (sprite.time >= 2f)
                {
                    timeUp = true;
                }
            }

            if (timeUp)
            {
                fireBallList.RemoveAt(0);
                timeUp = false;
            }

        }

        private void gameExit()
        {
            if (game.life < 0)
            {
                ICommand exit = new Exit(game);
                exit.Execute();
            }
        }

        private void moreLife()
        {
            if (game.coins >= 3)
            {
                game.coins = 0;
                game.life++;
            }
        }

        private void updateMario(GameTime time, Mario player)
        {

            MarioPowerupStateType powerupStateType = player.CurrentPowerupState.GetEnumValue();
            if (powerupStateType == MarioPowerupStateType.Dead)
            {
                timeSpent += (float)time.ElapsedGameTime.TotalSeconds;
                if (timeSpent > 3f)
                {
                    timeSpent = 0f;
                    ICommand reset = new Reset(game);
                    reset.Execute();
                    game.life--;
                }
            }
            if (player.Position.Y > 2000 || player.Position.X < 0)
            {
                timeSpent += (float)time.ElapsedGameTime.TotalSeconds;
                if (timeSpent >= 3f)
                {
                    timeSpent = 0f;
                    ICommand reset = new Reset(game);
                    reset.Execute();
                    game.life--;
                }
            }
        }

        private void activateEnemy()
        {
            foreach (ISprite a in enemies)
            {
                if (a.state == "out")
                {
                    Rectangle enemy = new Rectangle((int)a.Position.X - 450, (int)a.Position.Y - 2000, 100, 2000);
                    Rectangle mario = new Rectangle((int)game.mario.Position.X, (int)game.mario.Position.Y - 2000,100,2000);
                    if (enemy.Intersects(mario))
                    {
                        a.state = "Normal";
                    }
                }
            }
        }

        private void hurtUpdate(GameTime time)
        {
            if (!haveHurt)
            {
                foreach(Mario a in players)
                {
                    if (a.state == "Hurt")
                    {
                        haveHurt = true;
                    }
                    else
                    {
                        haveHurt = false;
                    }
                }
            } else
            {
                hurtTimer += (float)time.ElapsedGameTime.TotalSeconds;
                if(hurtTimer >= 2)
                {
                    hurtTimer = 0;
                    foreach (Mario a in players)
                    {
                        if (a.state == "Hurt")
                        {
                            a.state = "Normal";
                        }
                    }
                }
            }
           

            
        }

        private void eatTempItem(GameTime time)
        {
           foreach(ISprite a in items)
            {
                if(a.state == "Temp")
                {
                    hasTempCoin = true;
                    break;
                }
            }

            if (hasTempCoin)
            {
                tempCoinTimer += (float)time.ElapsedGameTime.TotalSeconds;
            }

            if(tempCoinTimer >= 0.3f)
            {
                tempCoinTimer = 0f;
                hasTempCoin = false;

                foreach (ISprite a in items)
                {
                    if (a.state == "Temp")
                    {
                        items.Remove(a);
                        game.coins++;
                        break;
                    }
                }

            }
        }

        public void update(GameTime time)
        {
            gameExit();
            moreLife();
            activateEnemy();
            updateShatteredBlocks(time);
            updateDeadEnemy(time);
            deleteDropedEnemy();
            deleteDropedItems();
            fireBallLimit();
            fireBallTimesUp();
            updateStarMario(time);
            hurtUpdate(time);
            eatTempItem(time);

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
                updateMario(time, obj);
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
