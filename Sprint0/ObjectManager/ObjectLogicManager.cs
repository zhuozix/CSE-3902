using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer.State.PowerupState;
using Sprint0.MarioPlayer;
using Sprint0.NPC.Fireball;
using Sprint0.Sprites;
using Sprint0.UI.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Sounds;
using Sprint0.Content;
using Sprint0.Factory;
using System.Diagnostics;
using Sprint0.NPC.Boss;

namespace Sprint0.ObjectManager
{
    public class ObjectLogicManager
    {
        private Game1 game;
        
        public List<ISprite> blocks;
        public List<ISprite> enemies;
        public List<ISprite> players;
        public List<ISprite> items;
        public List<ISprite> fireBallList;

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

        private float winTimer = 3f;

        public ObjectLogicManager(GameObjectManager ManagerIn) {
            this.blocks = ManagerIn.blocks;
            this.enemies = ManagerIn.enemies;
            this.players = ManagerIn.players;
            this.items = ManagerIn.items;
            this.fireBallList = ManagerIn.fireBallList;
            this.game = ManagerIn.game;
        }

        public void toWinState(GameTime gameTime)
        {
            winTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (winTimer < 0f)
            {
                game.ChangeState(new WinState(game));
            }
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

            if (timerOfCrashedBlock >= 0.3f)
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
            foreach (ISprite obj in this.items)
            {
                if (obj.Position.Y >= 600)
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

        private void fireBall()
        {
            foreach(Mario mario in players)
            {
                if (mario.mode)
                {
                    fireBallLimit();
                    fireBallTimesUp();
                }
                else
                {
                    fireBallTimesUp();
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

            foreach (Fireball sprite in fireBallList)
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
            if (game.life < 1)
            {
                game.ChangeState(new GameOverState(game));
            }
            if(game.time < 0)
            {
                game.ChangeState(new TimeUpState(game));
            }
        }

        private void moreLife()
        {
            if (game.coins >= 8)
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
                    game.ChangeState(new DeathState(game));
                    game.life--;
                }
            }
        }
        /*
        private void activateEnemy()
        {
            foreach (ISprite a in enemies)
            {
                if (a.state != "out")
                {
                    Rectangle enemy = new Rectangle((int)a.Position.X - 450, (int)a.Position.Y - 2000, 100, 2000);
                    Rectangle mario = new Rectangle((int)game.mario.Position.X, (int)game.mario.Position.Y - 2000, 100, 2000);
                    if (enemy.Intersects(mario))
                    {
                        a.state = "Normal";
                    }
                }
            }
        }
        */
        private void hurtUpdate(GameTime time)
        {
            if (!haveHurt)
            {
                foreach (Mario a in players)
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
            }
            else
            {
                hurtTimer += (float)time.ElapsedGameTime.TotalSeconds;
                if (hurtTimer >= 2)
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
            foreach (ISprite a in items)
            {
                if (a.state == "Temp")
                {
                    hasTempCoin = true;
                    break;
                }
            }

            if (hasTempCoin)
            {
                tempCoinTimer += (float)time.ElapsedGameTime.TotalSeconds;
            }

            if (tempCoinTimer >= 0.3f)
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

        public void activateBossFight()
        {
            if(game.bowser != null && Math.Abs(game.mario.Position.X - game.bowser.Position.X) <= 650)
            {
                game.bowser.activated = true;
            }
        }

        public void update(GameTime time)
        {
          
            if(game.bossHP <= 0)
            {
                SoundPlayer.playStageClear();
                toWinState(time); return;
            }
            gameExit();
            moreLife();
            activateBossFight();
            updateShatteredBlocks(time);
            updateDeadEnemy(time);
            deleteDropedEnemy();
            deleteDropedItems();
            fireBall();
            updateStarMario(time);
            hurtUpdate(time);
            eatTempItem(time);

            foreach (Mario obj in this.players)
            {
                updateMario(time, obj);
            }
        }
    }
}
