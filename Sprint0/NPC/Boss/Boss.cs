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

namespace Sprint0.NPC.Boss
{
    public enum BossActionType
    {
        Idle,
        Jumping,
        Falling,
        Running,
    }

    public class Boss : Entity
    {

        //property of boss
        public bool isFacingRight { get; set; }
        public bool isFlying { get; set; }
        public BossActionType currentActionType { get; set; }
        public BossFactory factory { get; set; }
        public bool activated = false;
        
        private bool isVisible = true;
        private float visibleTimer = 0f;

        //property of ai
        public BossAI _ai { get; set; }

        //resources
        public Game1 game;
        internal int currentFrame;
        internal int totalFrames;

        /*
         * Constructor
         */
        public Boss(Vector2 spawnLocation, Game1 gameInstanceIn)
        {
            game = gameInstanceIn; 
            Initiallize();
            Position = spawnLocation;
            //ai
            loadAI();
            this.currentFrame = 0;
            this.totalFrames = 4;
            this.Name = "Boss";
            this.state = "Angry";
        }

        public void changeSprite()
        {
            Vector2 temp = this.Position;
            //factory = new BossFactory(game.Content, this);
            this.Sprite = factory.buildSprite();
            this.Position = temp;
        }

        public void Initiallize()
        {
            //set velocity
            this.velocity = Vector2.Zero;
            this.Acceleration = Vector2.Zero;
            
            // set the initial state of the boss
            isFacingRight = true;
            isFlying = false;

            // get sprites
            factory = new BossFactory(game.Content,this);
            currentActionType = BossActionType.Idle;
            this.Sprite = factory.buildSprite();


        }

        public void loadAI()
        {
            Mario player = null;
            foreach(Mario obj in game.gameObjectManager.players)
            {
                player = obj;
                break;
            }
            _ai = new BossAI(this,player,game);
        }

        public void visibleManager(GameTime gameTime)
        {
            if (_ai.hitAndCannotMove || _ai.noFireballDmgTimer != 0f)
            {
                visibleTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (visibleTimer >= 0.1f)
                {
                    visibleTimer = 0f;
                    changeVisibleStatus();
                }
            }
            else
            {
                isVisible = true;
            }
        }
        public override void Update(GameTime gameTime) 
        {
            if(game.bossHP <= 0)
            {
                isVisible = false;
            }
            else if (activated)
            {
                visibleManager(gameTime);
                _ai.Update(gameTime);
                base.Update(gameTime);
            }

            
        }

        public override void Draw(SpriteBatch spriteBatch, bool isFlipped)
        {
            if (_ai.noDmgLock && _ai.angryMode)
            {
                base.DrawPink(spriteBatch, isFacingRight);
            }
            else if (_ai.noDmgLock)
            {
                base.DrawGold(spriteBatch, isFacingRight);
            }
            else if (_ai.angryMode && isVisible)
            {
                base.DrawRed(spriteBatch, isFacingRight);
            }
            else if (isVisible)
            {
                base.Draw(spriteBatch, isFacingRight);
            }
            
        }

        private void changeVisibleStatus()
        {
            if (isVisible) { isVisible = false; }
            else { isVisible = true; }
        }


    }
}
