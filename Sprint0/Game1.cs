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

namespace Sprint0
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private KeyboardController keyboardController;

		// Adam Sprint 2 
		public ISprite brickBlockSprite;
		public ISprite coinBlockSprite;
        // Seth Sprint 2
        public ISprite fireFlowerSprite;
        public ISprite starSprite;
        public ISprite redMushSprite;
        public ISprite greenMushSprite;
        public ISprite coinSprite;
        // Zhuozi Sprint 2
        public ISpriteE Gommba;
        public ISpriteE Koopa;
        //
        // Adam, Sprint 2
        public Texture2D texture_CoinBlock; 
		public Texture2D texture_BrickBlock;
        //
        // Seth Sprint 2
        public Texture2D texture_FireFlower;
        public Texture2D texture_Star;
        public Texture2D texture_GreenMush;
        public Texture2D texture_RedMush;
        public Texture2D texture_Coin;
        // Zhuozi Sprint 2
        public Texture2D texture_Gommba;
        public Texture2D texture_Koopa;

        public ISpriteE currentEnemy;
        // Adam Sprint 2
        public ISprite currentBlock;
        // Seth Sprint 2
        public ISprite currentItem;

        // Adam Sprint 2
        private ArrayList blockList;
        // Seth Sprint 2
        private ArrayList itemList;
        // Zhuozi Sprint 2
        private ArrayList enemyList;

        public int DisplaySprite { get; set; }
        // Adam Sprint 2
        public int DisplayBlock { get; set; }
        // Seth Sprint 2
        public int DisplayItem { get; set; }
        // Zhuozi Sprint 2
        public int DisplayEnemy { get; set; }


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
			// Adam Sprint 2
			texture_CoinBlock = Content.Load<Texture2D>("CoinBlocksSpriteSheet");
			texture_BrickBlock = Content.Load<Texture2D>("BrickBlocksSpriteSheet");
            
            // Seth Sprint 2
            texture_FireFlower = Content.Load<Texture2D>("fireFlower");
            texture_Star = Content.Load<Texture2D>("star");
            texture_GreenMush = Content.Load<Texture2D>("greenMushroom");
            texture_RedMush = Content.Load<Texture2D>("redMushroom");
            texture_Coin = Content.Load<Texture2D>("coin");

            //Zhuozi Sprint 2
            texture_Gommba = Content.Load<Texture2D>("EnemyContent/WalkingGoomba1");
            texture_Koopa = Content.Load<Texture2D>("EnemyContent/WalkingGreenKoopa1");

            keyboardController = new KeyboardController();
            // Adam Sprint 2
            blockList = new ArrayList();
            // Seth Sprint 2
            itemList = new ArrayList();
            // Zhuozi Sprint 2
            enemyList = new ArrayList();


            base.Initialize();
        }

        protected override void LoadContent()
        {         
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            CreateSprites();
            keyboardController.loadCommonCommand(this);
        }

        protected override void Update(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //display the sprite from the sprite list one at a time.
            #region implement command to mouse and keyboard
            // Zhuozi Sprint 2
            currentEnemy = (ISpriteE)enemyList[DisplayEnemy];
            // Adam Sprint 2
            currentBlock = (ISprite)blockList[DisplayBlock];
            // Seth Sprint 2
            currentItem = (ISprite)itemList[DisplayItem];
            //
            // implement command to mouse and keyboard
            keyboardController.UpdateInput();
           
            #endregion
            

            currentBlock.Update(gameTime);
			currentBlock.Draw(_spriteBatch, true);

            currentItem.Update(gameTime);
            currentItem.Draw(_spriteBatch, true);

            // Zhuozi Sprint2
            currentEnemy.Update(gameTime);
            currentEnemy.Draw(_spriteBatch);

            base.Update(gameTime);
           
        }

        private void CreateSprites()
        {

            // Adam Sprint 2
            coinBlockSprite = new BlockSprite(texture_CoinBlock, 1, 5, new Vector2(100, 100));
			brickBlockSprite = new BlockSprite(texture_BrickBlock, 1, 5, new Vector2(200, 100));
            //
            // Seth Sprint 2
            fireFlowerSprite = new FireFlowerSprite(texture_FireFlower, new Vector2(100, 300), 1, 4);
            starSprite = new StarSprite(texture_Star, new Vector2(200, 300), 1, 4, _graphics, 1);
            greenMushSprite = new GreenMushroomSprite(texture_GreenMush, new Vector2(300,300), 1, 1, _graphics,1);
            redMushSprite = new RedMushroomSprite(texture_RedMush, new Vector2(400,300), 1, 1, _graphics, 1);
            coinSprite = new CoinSprite(texture_Coin, new Vector2(500, 300), 1, 4);
            //Zhuozi Sprint 2
            Gommba = new GommbaSprite(texture_Gommba, new Vector2(500, 400), 1, 2, _graphics);
            Koopa = new GommbaSprite(texture_Koopa, new Vector2(500, 400), 1, 2, _graphics);
           
          
			// Adam Sprint 2
			blockList.Add(coinBlockSprite);
			blockList.Add(brickBlockSprite);
            //

            //Seth Sprint 2
            itemList.Add(fireFlowerSprite);
            itemList.Add(starSprite);
            itemList.Add(greenMushSprite);
            itemList.Add(redMushSprite);
            itemList.Add(coinSprite);
            // Zhuozi Sprint 2
            enemyList.Add(Gommba);
            enemyList.Add(Koopa);
        }
        public void gameReset()
        {
            DisplayBlock = 0;
            DisplayEnemy = 0;
            DisplayItem = 0;
        }
    }
}