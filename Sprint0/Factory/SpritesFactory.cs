using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.NPC.Blocks;
using Sprint0.NPC.Enemy;
using Sprint0.NPC.Item;
using Sprint0.Sprites;
using Sprint0.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.MarioPlayer;
using Sprint0.MarioPlayer.State.ActionState;
using Sprint0.ObjectManager;
using Sprint0.NPC.Fireball;
using Sprint0.UI.Text;
using System.Reflection.Metadata;
using Sprint0.UI.Text.BossFightText;

namespace Sprint0.Factory
{
    public class SpritesFactory
    {
        private GraphicsDeviceManager _graphics;
        Game1 gameInstance;
  		// Blocks
		public Texture2D texture_CoinBlock;
		public Texture2D texture_BrickBlock;
        public Texture2D texture_BlueBrickBlock;
        public Texture2D texture_BlueFloorBlock;
		public Texture2D texture_FloorBlock;
		public Texture2D texture_StairBlock;
		public Texture2D texture_UsedBlock;
		public Texture2D texture_GreenPipeLarge;
        public Texture2D texture_GreenPipeMedium;
        public Texture2D texture_GreenPipeSmall;
        public Texture2D texture_SidewaysGreenPipe;
        public Texture2D texture_GreenTube;
        public Texture2D texture_ShatteredBlock;
        public Texture2D texture_InvisibleBlock;
		public Texture2D texture_FlagPoleBlock;
        public Texture2D texture_Platform;

		// Items
		public Texture2D texture_FireFlower;
        public Texture2D texture_Star;
        public Texture2D texture_GreenMush;
        public Texture2D texture_RedMush;
        public Texture2D texture_Coin;
        public Texture2D texture_Machinegun;
        public Texture2D texture_Moonjump;

        // Enemies
        public Texture2D texture_Gommba;
        public Texture2D texture_DeadGommba;
        public Texture2D texture_Koopa;
        public Texture2D texture_KoopaShell;

        // Bullets
        public Texture2D texture_FireBall;

        // Mario
        public Texture2D texture_Mario;

        //Font
        public SpriteFont texture_scorefont;
        public SpriteFont texture_HUDFont;

        public SpritesFactory(Game1 gameInstance)
        {
            this._graphics = gameInstance._graphics;
            this.gameInstance = gameInstance;
            initalize();
        }

        public void initalize()
        {
            ContentManager content = gameInstance.Content;
            // Blocks
            texture_BrickBlock = content.Load<Texture2D>("Blocks/brickBlock");
            texture_BlueBrickBlock = content.Load<Texture2D>("Blocks/blueBrickBlock");
            texture_BlueFloorBlock = content.Load<Texture2D>("Blocks/blueFloorBlock");
            texture_CoinBlock = content.Load<Texture2D>("Blocks/questionBlock");
            texture_FloorBlock = content.Load<Texture2D>("Blocks/floorBlock");
			texture_StairBlock = content.Load<Texture2D>("Blocks/stairblock");
			texture_UsedBlock = content.Load<Texture2D>("Blocks/usedblock");
			texture_GreenPipeLarge = content.Load<Texture2D>("Blocks/GreenPipeLarge");
            texture_GreenPipeMedium = content.Load<Texture2D>("Blocks/GreenPipeMedium");
            texture_GreenPipeSmall = content.Load<Texture2D>("Blocks/GreenPipeSmall");
            texture_SidewaysGreenPipe = content.Load<Texture2D>("Blocks/sidewaysGreenPipe");
            texture_GreenTube = content.Load<Texture2D>("Blocks/greenTube");
            texture_ShatteredBlock = content.Load<Texture2D>("Blocks/ShatteredBlock");
            texture_InvisibleBlock = content.Load<Texture2D>("Blocks/HiddenBloc");
			texture_FlagPoleBlock = content.Load<Texture2D>("Blocks/flagPoleBlock");
            texture_Platform = content.Load<Texture2D>("Blocks/platform");

			// Items
			texture_FireFlower = content.Load<Texture2D>("fireFlower");
            texture_Star = content.Load<Texture2D>("star");
            texture_GreenMush = content.Load<Texture2D>("greenMushroom");
            texture_RedMush = content.Load<Texture2D>("redMushroom");
            texture_Coin = content.Load<Texture2D>("coin");
            texture_Machinegun = content.Load<Texture2D>("machinegun");
            texture_Moonjump = content.Load<Texture2D>("moonjump");


            // Enemies
            texture_Gommba = content.Load<Texture2D>("EnemyContent/WalkingGoomba1");
            texture_Koopa = content.Load<Texture2D>("EnemyContent/WalkingGreenKoopa1");
            texture_KoopaShell = content.Load<Texture2D>("EnemyContent/GreenKoopaTroopaShell");
            texture_DeadGommba = content.Load<Texture2D>("EnemyContent/deadGoomba");

            // Bullets
            texture_FireBall = content.Load<Texture2D>("FireMario/fireball");

            // Font
            texture_scorefont = content.Load<SpriteFont>("Font/Score");
            texture_HUDFont = content.Load<SpriteFont>("Font/HUDFont");
        }

        public Texture2D getBackgroundSprite(String background)
        {
            ContentManager content = gameInstance.Content;
            return content.Load<Texture2D>(background);
        }
        public ISprite getCoinBlockSprite()
        {
            ISprite sprite = new BlockSprite(texture_CoinBlock, 1, 3, new Vector2(100, 100));
            sprite.Name = "CoinBlock";
            sprite.state = "Normal";
            return sprite;
        }

        

        public ISprite getBrickBlockSprite() 
        {
            ISprite sprite = new BlockSprite(texture_BrickBlock, 1, 1, new Vector2(200, 100));
            sprite.Name = "BrickBlock";
            sprite.state = "Normal";
            return sprite;
        }

        public ISprite getBlueBrickBlockSprite()
        {
            ISprite sprite = new BlockSprite(texture_BlueBrickBlock, 1, 1, new Vector2(200, 100));
            sprite.Name = "BrickBlock";
            sprite.state = "Normal";
            return sprite;
        }
        public ISprite getFloorBlockSprite()
		{
            ISprite sprite = new BlockSprite(texture_FloorBlock, 1, 1, new Vector2(300, 100));
            sprite.Name = "FloorBlock";
            sprite.state = "Normal";
            return sprite;
		}
        public ISprite getBlueFloorBlockSprite()
        {
            ISprite sprite = new BlockSprite(texture_BlueFloorBlock, 1, 1, new Vector2(300, 100));
            sprite.Name = "FloorBlock";
            sprite.state = "Normal";
            return sprite;
        }
        public ISprite getStairBlockSprite()
		{
            ISprite sprite = new BlockSprite(texture_StairBlock, 1, 1, new Vector2(400, 100));
            sprite.Name = "StairBlock";
            sprite.state = "Normal";
            return sprite;
		}
		public ISprite getUsedBlockSprite()
		{
            ISprite sprite = new BlockSprite(texture_UsedBlock, 1, 1, new Vector2(500, 100));
            sprite.Name = "UsedBlock";
            sprite.state = "Normal";
            return sprite;
		}
		public ISprite getGreenPipeLargeSprite()
		{
            ISprite sprite = new BlockSprite(texture_GreenPipeLarge, 1, 1, new Vector2(600, 100));
            sprite.Name = "GreenPipe";
            sprite.state = "Normal";
            return sprite;
		}

        public ISprite getGreenPipeMediumSprite()
        {
            ISprite sprite = new BlockSprite(texture_GreenPipeMedium, 1, 1, new Vector2(600, 100));
            sprite.Name = "GreenPipe";
            sprite.state = "Normal";
            return sprite;
        }

        public ISprite getGreenPipeSmallSprite()
        {
            ISprite sprite = new BlockSprite(texture_GreenPipeSmall, 1, 1, new Vector2(600, 100));
            sprite.Name = "GreenPipe";
            sprite.state = "Normal";
            return sprite;
        }

        public ISprite getSidewaysGreenPipeSprite()
        {
            ISprite sprite = new BlockSprite(texture_SidewaysGreenPipe, 1, 1, new Vector2(600, 100));
            sprite.Name = "GreenPipe";
            sprite.state = "Normal";
            return sprite;
        }

        public ISprite getGreenTubeSprite()
        {
            ISprite sprite = new BlockSprite(texture_GreenTube, 1, 1, new Vector2(600, 100));
            sprite.Name = "GreenPipe";
            sprite.state = "Normal";
            return sprite;
        }
        public ISprite getInvisibleBlock()
        {
            ISprite sprite = new BlockSprite(texture_InvisibleBlock, 1, 1, new Vector2(500, 100));
            sprite.Name = "InvisibleBlock";
            sprite.state = "Normal";
            return sprite;
        }
		public ISprite getFlagPoleBlock()
		{
			ISprite sprite = new BlockSprite(texture_FlagPoleBlock, 1, 1, new Vector2(500, 100));
			sprite.Name = "FlagPoleBlock";
			sprite.state = "Normal";
			return sprite;
		}
		public ISprite getShatteredBlock()
        {
            ISprite sprite = new BlockSprite(texture_ShatteredBlock, 1, 1, new Vector2(600, 100));
            sprite.Name = "ShatteredBlock";
            sprite.state = "Crashed";
            return sprite;
        }

        public ISprite getPlatformSprite()
        {
            ISprite sprite = new BlockSprite(texture_Platform, 1, 1, new Vector2(600, 100));
            sprite.Name = "Platform";
            sprite.state = "Normal";
            return sprite;
        }

        public ISprite getFireFlowerSprite()
        {
            ISprite sprite = new FireFlowerSprite(texture_FireFlower, new Vector2(100, 300), 1, 4);
            sprite.Name = "FireFlower";
            sprite.state = "Normal";
            return sprite;
        }
        public ISprite getMachinegunSprite()
        {
            ISprite sprite = new FireFlowerSprite(texture_Machinegun, new Vector2(100, 300), 1, 1);
            sprite.Name = "Machinegun";
            sprite.state = "Normal";
            return sprite;
        }

        public ISprite getMoonjumpSprite()
        {
            ISprite sprite = new FireFlowerSprite(texture_Moonjump, new Vector2(100, 300), 1, 1);

            sprite.Name = "Moonjump";
            sprite.state = "Normal";
            return sprite;
        }

        public ISprite getStarSprite()
        {
            ISprite sprite = new StarSprite(texture_Star, new Vector2(200, 300), 1, 4);
            sprite.Name = "Star";
            sprite.state = "Normal";
            return sprite;
        }
        public ISprite getGreenMushSprite() 
        {
            ISprite sprite = new RedMushroomSprite(texture_GreenMush, new Vector2(300, 300), 1, 1, 1);
            sprite.Name = "GreenMush";
            sprite.state = "Normal";
            return sprite;
            
            
        }
        public ISprite getRedMushSprite() {
            ISprite sprite = new RedMushroomSprite(texture_RedMush, new Vector2(400, 300), 1, 1, 1);
            sprite.Name = "RedMush";
            sprite.state = "Normal";
            return sprite;
        }
        public ISprite getCoinSprite() 
        {
            ISprite sprite = new CoinSprite(texture_Coin, new Vector2(500, 300), 1, 4);
            sprite.velocity = Vector2.Zero;
            sprite.Name = "Coin";
            sprite.state = "Normal";
            return sprite;
        }
        public ISprite getGommbaSprite() 
        {
            ISprite sprite = new EnemySprite(texture_Gommba, new Vector2(500, 400), 1, 2);
            sprite.Name = "Gommba";
            sprite.state = "out";
            return sprite;
            
        }
        public ISprite getKoopaSprite()
        {
            
            ISprite sprite = new EnemySprite(texture_Koopa, new Vector2(500, 400), 1, 2);
            sprite.Name = "Koopa";
            sprite.state = "out";
            return sprite;
        }

         public ISprite getKoopaShellSprite()
        {
            ISprite sprite = new EnemySprite(texture_KoopaShell, new Vector2(500, 400), 1, 1);
            sprite.Name = "KoopaShell";
            sprite.state = "Normal";
            return sprite;
        }
         public ISprite getDeadGommbaSprite()
        {
            ISprite sprite = new EnemySprite(texture_DeadGommba, new Vector2(500, 400), 1, 1);
            sprite.Name = "DeadGommba";
            sprite.state = "Normal";
            return sprite;
        }

        public ISprite getFireballSprite(Vector2 currentLocation, bool isFacingRight, bool mode)
        {
            if (isFacingRight)
            {
                if (mode)
                {
                    return new FireBallInstance(texture_FireBall, new Vector2(currentLocation.X + 5, currentLocation.Y), 2, 2,0);
                }
                else
                {
                    return new SuperFireball(texture_FireBall, new Vector2(currentLocation.X + 5, currentLocation.Y), 2, 2, 0);
                }
                
            }
            else
            {
                if (mode)
                {
                    return new FireBallInstance(texture_FireBall, new Vector2(currentLocation.X - 5, currentLocation.Y), 2, 2, 1);
                }
                else
                {
                    return new SuperFireball(texture_FireBall, new Vector2(currentLocation.X - 5, currentLocation.Y), 2, 2, 1);
                }
                
            }

        }

        public ISprite getSuperFireballSprite(Vector2 currentLocation, bool isFacingRight)
        {
            if (isFacingRight)
            {
                return new SuperFireball(texture_FireBall, new Vector2(currentLocation.X + 5, currentLocation.Y), 2, 2, 0);
            }
            else
            {
                return new SuperFireball(texture_FireBall, new Vector2(currentLocation.X - 5, currentLocation.Y), 2, 2, 1);
            }

        }

        public Sprite getMarioSprite(string fileLocation)
        {
            texture_Mario = gameInstance.Content.Load<Texture2D>(fileLocation);
            if (gameInstance.mario.running())
            {
                return new NoneMovingAnimatedSprite(texture_Mario, Vector2.Zero, 1, 3);
            }
            else
            {
                return new NoneAnimatedNonMovingSprite(texture_Mario, Vector2.Zero, 1, 1);
            }
        }
        
        //UHD
        public ISprite getCoinFontSprite()
        {
            ISprite sprite = new CoinTextSprite(texture_HUDFont, "", new Vector2(30, 50), Color.White, gameInstance);
            sprite.Name = "Font";
            sprite.state = "Normal";
            return sprite;
        }
        public ISprite getLifeFontSprite()
        {
            ISprite sprite = new LifeTextSprite(texture_HUDFont, "", new Vector2(150, 50), Color.White, gameInstance);
            sprite.Name = "Font";
            sprite.state = "Normal";
            return sprite;
        }
        public ISprite getTimeFontSprite()
        {
            ISprite sprite = new TimeTextSprite(texture_HUDFont, "", new Vector2(650, 50), Color.White, gameInstance);
            sprite.Name = "Font";
            sprite.state = "Normal";
            return sprite;
        }
        public ISprite getMoonJumpFontSprite()
        {
            ISprite sprite = new MoonJumpText(texture_HUDFont, "", new Vector2(550, 50), Color.White, gameInstance);
            sprite.Name = "Font";
            sprite.state = "Normal";
            return sprite;
        }
        public ISprite getGunFontSprite()
        {
            ISprite sprite = new MachineGunText(texture_HUDFont, "", new Vector2(550, 50), Color.White, gameInstance);
            sprite.Name = "Font";
            sprite.state = "Normal";
            return sprite;
        }
        public ISprite getBossFontSprite()
        {
            ISprite sprite = new BossHP(texture_HUDFont, "", new Vector2(300, 50), Color.White, gameInstance);
            sprite.Name = "Font";
            sprite.state = "Normal";
            return sprite;
        }
        // game state
        public ISprite getTitleFontSprite(GameTime gameTime)
        {
            float pulsate = (float)(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 6.0) + 1.0) / 2.0f;
            Color color = Color.Lerp(new Color(255, 192, 203), Color.White, pulsate);
            ISprite sprite = new TitleTextSprite(gameInstance,texture_scorefont, "", new Vector2(345, 395), color);
            sprite.Name = "Font";
            sprite.state = "Normal";
            return sprite;
        }
        public ISprite getDeathFontSprite()
        {
            ISprite sprite = new DeathTextSprite(texture_scorefont, "", new Vector2(250, 200), Color.Black, gameInstance);
            sprite.Name = "Font";
            sprite.state = "Normal";
            return sprite;
        }
        public ISprite getTimeUpFontSprite()
        {
            ISprite sprite = new TimeUpTextSprite(texture_scorefont, "", new Vector2(250, 200), Color.Black, gameInstance);
            sprite.Name = "Font";
            sprite.state = "Normal";
            return sprite;
        }
        public ISprite getWinFontSprite()
        {
            ISprite sprite = new WinTextSprite(gameInstance,texture_scorefont, "", new Vector2(50, 100), Color.White);
            sprite.Name = "Font";
            sprite.state = "Normal";
            return sprite;
        }
        public ISprite getGameOverFontSprite(GameTime gameTime)
        {
            float pulsate = (float)(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 6.0) + 1.0) / 2.0f;
            Color color = Color.Lerp(Color.Yellow, Color.White, pulsate);
            ISprite sprite = new GameOverTextSprite(gameInstance,texture_scorefont, "", new Vector2(50, 200), color);
            sprite.Name = "Font";
            sprite.state = "Normal";
            return sprite;
        }

        //boss


    }
}
