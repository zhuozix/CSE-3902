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
using Sprint0.Factory;
using Sprint0.MarioPlayer;

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
        private PlayerFactory playerFactory;

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
            playerFactory = new PlayerFactory(Content);

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

            /*
             * Load Sprites 
             */
            //Player
            mario = new Mario(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), playerFactory);
            // NPC lists
            npcSpritesFactory.addAllBlocks(blockList);
            npcSpritesFactory.addAllItems(itemList);
            npcSpritesFactory.addAllEnemies(enemyList);

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
            //Players
            mario.Update(gameTime);
            mario.Draw(_spriteBatch,true);
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
    }
}