﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Blocks;
using Sprint0.Command;
using Sprint0.Content;
using Sprint0.Controller;
using Sprint0.Item;
using Sprint0.Sprites;
using System.Collections;
using System.Collections.Generic;
using Sprint0.Enemy;
using Sprint0.Factory;
using Sprint0.MarioPlayer;
using Microsoft.Xna.Framework.Content;

namespace Sprint0
{
    public class Game1 : Game
    {
        private static Game1 instance;
        public static Game1 Instance
        {
            get
            {
                return instance;
            }
        }
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        /*
         * Controllers
         */
        private IController keyboardController;
        private IController playerController;

        /*
         * SpriteFactories
         */
        private NPCFactory npcSpritesFactory;
        public PlayerFactory playerFactory;
        public BulletFactory fireballFactory;

        /*
         * Current Sprites
         */
        //Mario
        public Mario mario;
        //Npc
        public ISprite currentEnemy;
        public ISprite currentBlock;
        public ISprite currentItem;

        /*
         *  Sprites Lists
         */
        private ArrayList blockList;
        private ArrayList itemList;
        private ArrayList enemyList;
        public ArrayList bulletList;

        /*
         * Command Control
         */
        public int DisplaySprite { get; set; }
        public int DisplayBlock { get; set; }
        public int DisplayItem { get; set; }
        public int DisplayEnemy { get; set; }


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //Factories
            npcSpritesFactory = new NPCFactory(_graphics);
            npcSpritesFactory.initalize(Content);
            playerFactory = new PlayerFactory(Content);
            fireballFactory = new BulletFactory(_graphics);
            fireballFactory.initalize(Content);


            //controllers
            keyboardController = new KeyboardController();
            playerController = new PlayerController();
            

            //Lists
            blockList = new ArrayList();
            itemList = new ArrayList();
            enemyList = new ArrayList();
            bulletList = new ArrayList();


            base.Initialize();
        }

        protected override void LoadContent()
        {         
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            /*
             * Load Sprites 
             */
            //Player
            mario = new Mario(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), this);
            // NPC lists
            npcSpritesFactory.addAllBlocks(blockList);
            npcSpritesFactory.addAllItems(itemList);
            npcSpritesFactory.addAllEnemies(enemyList);


            //Load commands to controller
            keyboardController.loadCommonCommand(this);
            playerController.loadCommonCommand(this);
        }

        protected override void Update(GameTime gameTime)
        {
            

            //display the sprite from the sprite list one at a time.
            #region implement command
            // Zhuozi Sprint 2
            currentEnemy = (ISprite)enemyList[DisplayEnemy];
            // Adam Sprint 2
            currentBlock = (ISprite)blockList[DisplayBlock];
            // Seth Sprint 2
            currentItem = (ISprite)itemList[DisplayItem];
            //
            keyboardController.UpdateInput();
            playerController.UpdateInput(); 
            #endregion
            //Players
            mario.Update(gameTime);
            
            //Blocks
            currentBlock.Update(gameTime);
			
            //Items
            currentItem.Update(gameTime);
            
            //Enemies
            currentEnemy.Update(gameTime);

            foreach(ISprite sprite in bulletList)
            {
                sprite.Update(gameTime);
            }

            fireballFactory.update(bulletList,gameTime);
            base.Update(gameTime);
           
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            mario.Draw(_spriteBatch, true);
            currentBlock.Draw(_spriteBatch, true);
            currentItem.Draw(_spriteBatch, true);
            currentEnemy.Draw(_spriteBatch, true);

            foreach (FireBallSprite sprite in bulletList)
            {
                sprite.Draw(_spriteBatch, sprite.isFliped);
            }

            _spriteBatch.End();
        } 

        public void GameReset()
        {
            this.Initialize();
        }
    }
}