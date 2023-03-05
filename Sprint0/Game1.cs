using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Content;
using Sprint0.Sprites;
using System.Collections;
using Sprint0.Enemy;
using Sprint0.Factory;
using Sprint0.MarioPlayer;
using Sprint0.Sprites.Lists;
using Sprint0.ObjectManager;
using Sprint0.LevelLoader;
using Sprint0.Collision;

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
        public const int scale = 2;

        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        /*
         * Controllers
         */
        private IController keyboardController;

        private ICollision Collision;
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
        public ISprite currentEnemy;
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

        /*
         * Camera
         */
        private Camera camera;

        public GameObjectManager gameObjectManager;
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
            gameObjectManager = new GameObjectManager();

            blockList = new ArrayList();
            itemList = new ArrayList();
            enemyList = new ArrayList();
            fireBallList = new FireBallList();



            Collision = new Collide(gameObjectManager);

            base.Initialize();
        }

        protected override void LoadContent()
        {         
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            camera = new Camera();

            /*
             * Load Sprites 
             */
          LevelLoader.LevelLoader.loadLevel(gameObjectManager, "level1-1.xml", spritesFactory, this);

            //Player
            //mario = new Mario(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), this);
            // NPC lists
            //spritesFactory.addAllBlocks(blockList);
            //spritesFactory.addAllItems(itemList);
            //spritesFactory.addAllEnemies(enemyList);

            foreach (ISprite obj in blockList)
            {
                gameObjectManager.addObject(obj, "block");
            }
            foreach (ISprite obj in enemyList)
            {
                gameObjectManager.addObject(obj, "enemy");
            }
            foreach (ISprite obj in itemList)
            {
                gameObjectManager.addObject((ISprite)obj, "item");
            }
            gameObjectManager.addObject(mario, "player");
            //Load commands to controller
            keyboardController.loadCommonCommand();
        }

        protected override void Update(GameTime gameTime)
        {
            

            //display the sprite from the sprite list one at a time.
            //currentEnemy = (ISprite)enemyList[DisplayEnemy];
            //currentBlock = (ISprite)blockList[DisplayBlock];
            //currentItem = (ISprite)itemList[DisplayItem];
            
            keyboardController.UpdateInput();
            //Players
            //mario.Update(gameTime);
            Collision.Update(gameTime);
            //Blocks
            //currentBlock.Update(gameTime);
            gameObjectManager.update(gameTime);

            //Items
            //currentItem.Update(gameTime);


            
            //Enemies
            //currentEnemy.Update(gameTime);

            //Fireballs
            //fireBallList.Update(gameTime);

            camera.MoveCamera(mario);

            base.Update(gameTime);
           
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(transformMatrix: camera.Transform);

            //mario.Draw(_spriteBatch, true);
            //currentBlock.Draw(_spriteBatch, true);
            gameObjectManager.Draw(_spriteBatch, true);
            //currentItem.Draw(_spriteBatch, true);
            //currentEnemy.Draw(_spriteBatch, true);
            //fireBallList.Draw(_spriteBatch);


            _spriteBatch.End();
        } 

        public void GameReset()
        {
            this.Initialize();
        }
    }
}