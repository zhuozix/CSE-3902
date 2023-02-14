using Microsoft.Xna.Framework;
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
using System;
using Sprint0.Mario;
using Sprint0.Factory;

namespace Sprint0
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        /*
         * Controllers
         */
        private KeyboardController keyboardController;

        /*
         * SpriteFactories
         */
        private NPCFactory npcSpritesFactory;

        /*
         * Current Sprites
         */
        //Mario
        public ISprite mario;
        public Texture2D texture_Mario;
        public ISpriteE currentEnemy;
        public ISprite currentBlock;
        public ISprite currentItem;

        /*
         *  Sprites Lists
         */
        private ArrayList blockList;
        private ArrayList itemList;
        private ArrayList enemyList;

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
            //Test for player
            texture_Mario = Content.Load<Texture2D>("Mario");
            //controllers
            keyboardController = new KeyboardController();
            //Lists
            blockList = new ArrayList();
            itemList = new ArrayList();
            enemyList = new ArrayList();


            base.Initialize();
        }

        protected override void LoadContent()
        {         
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //Load Sprites to lists
            CreateSprites();
            //Load commands to controller
            keyboardController.loadCommonCommand(this);
        }

        protected override void Update(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //display the sprite from the sprite list one at a time.
            #region implement command
            // Zhuozi Sprint 2
            currentEnemy = (ISpriteE)enemyList[DisplayEnemy];
            // Adam Sprint 2
            currentBlock = (ISprite)blockList[DisplayBlock];
            // Seth Sprint 2
            currentItem = (ISprite)itemList[DisplayItem];
            //
            keyboardController.UpdateInput();

            #endregion
            //Test
            mario.Update(gameTime); 
            mario.Draw(_spriteBatch, true);
            //Blocks
            currentBlock.Update(gameTime);
			currentBlock.Draw(_spriteBatch, true);
            //Items
            currentItem.Update(gameTime);
            currentItem.Draw(_spriteBatch, true);
            //Enemies
            currentEnemy.Update(gameTime);
            currentEnemy.Draw(_spriteBatch);

            base.Update(gameTime);
           
        }

        private void CreateSprites()
        {
            //Test
            mario = new MarioSpritesTest(texture_Mario,new Vector2(280, 300), 1, 1);
            // NPC lists
            npcSpritesFactory.addAllBlocks(blockList);
            npcSpritesFactory.addAllItems(itemList);
            npcSpritesFactory.addAllEnemies(enemyList);
        }
        public void gameReset()
        {
            DisplayBlock = 0;
            DisplayEnemy = 0;
            DisplayItem = 0;
        }
    }
}