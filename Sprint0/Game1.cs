using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Content;
using Sprint0.Sprites;
using System.Collections;
using Sprint0.Enemy;
using Sprint0.Factory;
using Sprint0.MarioPlayer;
using Sprint0.Sprites.Lists;

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

        /*
         * SpriteFactories
         */
        public SpritesFactory spritesFactory;

        /*
         * Current Sprites
         */
        //Mario
        public Mario mario;
        //Npc
        public ISpriteE currentEnemy;
        public ISprite currentBlock;
        public ISprite currentItem;

        /*
         *  Sprites Lists
         */
        private ArrayList blockList;
        private ArrayList itemList;
        private ArrayList enemyList;
        public FireBallList fireBallList;

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
            spritesFactory = new SpritesFactory(this);
            spritesFactory.initalize();


            //controllers
            keyboardController = new KeyboardController(this);
            

            //Lists
            blockList = new ArrayList();
            itemList = new ArrayList();
            enemyList = new ArrayList();
            fireBallList = new FireBallList();


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
            spritesFactory.addAllBlocks(blockList);
            spritesFactory.addAllItems(itemList);
            spritesFactory.addAllEnemies(enemyList);

            //Load commands to controller
            keyboardController.loadCommonCommand();
        }

        protected override void Update(GameTime gameTime)
        {
            

            //display the sprite from the sprite list one at a time.
            #region implement command
           
            currentEnemy = (ISpriteE)enemyList[DisplayEnemy];
            
            currentBlock = (ISprite)blockList[DisplayBlock];
            
            currentItem = (ISprite)itemList[DisplayItem];
            
            keyboardController.UpdateInput();
            #endregion
            //Players
            mario.Update(gameTime);
            
            //Blocks
            currentBlock.Update(gameTime);
			
            //Items
            currentItem.Update(gameTime);
            
            //Enemies
            currentEnemy.Update(gameTime);

            //Fireballs
            fireBallList.Update(gameTime);

            base.Update(gameTime);
           
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            mario.Draw(_spriteBatch, true);
            currentBlock.Draw(_spriteBatch, true);
            currentItem.Draw(_spriteBatch, true);
            currentEnemy.Draw(_spriteBatch);
            fireBallList.Draw(_spriteBatch);


        } 

        public void GameReset()
        {
            this.Initialize();
        }
    }
}