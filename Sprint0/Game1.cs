using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Content;
using System.Collections;
using System.Collections.Generic;

namespace Sprint0
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MouseController mouseController;
        private KeyboardController keyboardController;
        public ISprite NoneMovingNoneAnimatedSprite;
        public ISprite NoneMovingAnimatedSprite;
        public ISprite MovingNoneAnimatedSprite;
        public ISprite MovingAnimatedSprite;
		// Adam Sprint 2
		public ISprite brickBlockSprite;
		public ISprite coinBlockSprite;
        //
		// Adam, Sprint 2
		public Texture2D texture_CoinBlock; 
		public Texture2D texture_BrickBlock;
        //
		public Texture2D MarioSprite;
        public Texture2D MarioDeathSprite;
        public Texture2D MarioWalkRight;
        private ArrayList spritesList;
		// Adam Sprint 2
		private ArrayList blockList;
        //
		private ArrayList controllerList;
        private SpriteFont font;
        private Rectangle topLeft = new Rectangle(0, 0, 400, 220);
        private Rectangle topRight = new Rectangle(400, 0, 800, 220);
        private Rectangle bottomLeft = new Rectangle(0, 220, 400, 440);
        private Rectangle bottomRight = new Rectangle(400, 220, 800,440);
        public int DisplaySprite { get; set; }
        // Adam Sprint 2
        public int DisplayBlock { get; set; }
        //
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
            //
			#region initialize variables
			keyboardController = new KeyboardController();
            mouseController = new MouseController();
            spritesList = new ArrayList();
            // Adam Sprint 2
            blockList = new ArrayList();
            //
            controllerList = new ArrayList();
            #endregion
            #region load textures
            MarioSprite = Content.Load<Texture2D>("Mario");
            MarioDeathSprite = Content.Load<Texture2D>("MarioDeath");
            MarioWalkRight=(Content.Load<Texture2D>("MarioWalkRight"));

            font = Content.Load<SpriteFont>("font");
            #endregion
            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            CreateSprites();
            #region create command
            controllerList.Add(keyboardController);
            controllerList.Add(mouseController);
            ICommand NoneMovingNoneAnimatedCommand = new CommandList(this, 0);
            ICommand MovingNoneAnimatedCommand = new CommandList(this, 3);
            ICommand MovingAnimatedSpriteCommand = new CommandList(this, 1);
            ICommand NoneMovingAnimatedSpriteCommand = new CommandList(this, 2);
			// Adam Sprint 2
			ICommand SetBlockBrick = new SetBlockIndex(this, 0);
            ICommand SetBlockCoin = new SetBlockIndex(this, 1);
            //
			keyboardController.AddCommand(Keys.D1, NoneMovingNoneAnimatedCommand);
            keyboardController.AddCommand(Keys.D2, NoneMovingAnimatedSpriteCommand);
            keyboardController.AddCommand(Keys.D3, MovingNoneAnimatedCommand);
            keyboardController.AddCommand(Keys.D4, MovingAnimatedSpriteCommand);
            keyboardController.AddCommand(Keys.D0, new CommandExit(this));
			// Adam Sprint 2
			keyboardController.AddCommand(Keys.T, SetBlockBrick);
			keyboardController.AddCommand(Keys.Y, SetBlockCoin);
            //
			#endregion
		}

        protected override void Update(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            #region create command
            ICommand NoneMovingNoneAnimatedCommand = new CommandList(this, 0);
            ICommand MovingNoneAnimatedCommand = new CommandList(this, 3);
            ICommand MovingAnimatedSpriteCommand = new CommandList(this, 1);
            ICommand NoneMovingAnimatedSpriteCommand = new CommandList(this, 2);

			#endregion
			//display the sprite from the sprite list one at a time.
			#region implement command to mouse and keyboard
			ISprite currentSprite = (ISprite)spritesList[DisplaySprite];
            // Adam Sprint 2
            ISprite currentBlock = (ISprite)blockList[DisplayBlock];
            //
			// implement command to mouse and keyboard
			foreach (IController controller in controllerList)
            {
                controller.UpdateInput();
            }
            MouseState mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                Point mousePosition = new Point(mouse.X, mouse.Y);
                if (topLeft.Contains(mousePosition))
                {
                    NoneMovingNoneAnimatedCommand.Execute();
                }
                else if (topRight.Contains(mousePosition))
                {
                    NoneMovingAnimatedSpriteCommand.Execute();
                }
                else if (bottomLeft.Contains(mousePosition))
                {
                    MovingNoneAnimatedCommand.Execute();
                }
                else if (bottomRight.Contains(mousePosition))
                {
                    MovingAnimatedSpriteCommand.Execute();
                }
            }
            if (mouse.RightButton == ButtonState.Pressed)
            {
                ICommand exitCommand = new CommandExit(this);
                exitCommand.Execute();
            }
            #endregion
            currentSprite.Update(gameTime);
            currentSprite.Draw(_spriteBatch,true);

            currentBlock.Update(gameTime);
			currentBlock.Draw(_spriteBatch, true);

			base.Update(gameTime);
           
        }

        private void CreateSprites()
        {
            NoneMovingNoneAnimatedSprite = new NoneAnimatedNonMovingSprite(MarioSprite, new Vector2(600, 100),1,1);
            NoneMovingAnimatedSprite = new NoneMovingAnimatedSprite(MarioWalkRight, new Vector2(600,300),1,3);
            MovingAnimatedSprite = new MovingAnimatedSprite(MarioWalkRight, new Vector2(400, 200),1,3,_graphics,-1);
            MovingNoneAnimatedSprite = new MovingNoneAnimatedSprite(MarioDeathSprite, new Vector2(250, 330),1,1,_graphics,-1);
            // Adam Sprint 2
            coinBlockSprite = new BlockSprite(texture_CoinBlock, 1, 5, new Vector2(100, 100));
			brickBlockSprite = new BlockSprite(texture_BrickBlock, 1, 5, new Vector2(200, 100));
            //
			spritesList.Add(NoneMovingNoneAnimatedSprite);
            spritesList.Add(MovingAnimatedSprite);
            spritesList.Add(NoneMovingAnimatedSprite);
            spritesList.Add(MovingNoneAnimatedSprite);
			// Adam Sprint 2
			blockList.Add(coinBlockSprite);
			blockList.Add(brickBlockSprite);
            //
		}
    }
}