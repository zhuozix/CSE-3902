using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Blocks;
using Sprint0.Enemy;
using Sprint0.Item;
using Sprint0.Sprites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Factory
{
    internal class NPCFactory
    {
        private GraphicsDeviceManager _graphics;
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
        // Adam, Sprint 2
        public Texture2D texture_CoinBlock;
        public Texture2D texture_BrickBlock;

        // Seth Sprint 2
        public Texture2D texture_FireFlower;
        public Texture2D texture_Star;
        public Texture2D texture_GreenMush;
        public Texture2D texture_RedMush;
        public Texture2D texture_Coin;
        // Zhuozi Sprint 2
        public Texture2D texture_Gommba;
        public Texture2D texture_Koopa;

        public NPCFactory( GraphicsDeviceManager graphicsIn)
        {
            _graphics = graphicsIn;
        }

        public void initalize(ContentManager content)
        {
            // Adam Sprint 2
            texture_CoinBlock = content.Load<Texture2D>("CoinBlocksSpriteSheet");
            texture_BrickBlock = content.Load<Texture2D>("BrickBlocksSpriteSheet");

            // Seth Sprint 2
            texture_FireFlower = content.Load<Texture2D>("fireFlower");
            texture_Star = content.Load<Texture2D>("star");
            texture_GreenMush = content.Load<Texture2D>("greenMushroom");
            texture_RedMush = content.Load<Texture2D>("redMushroom");
            texture_Coin = content.Load<Texture2D>("coin");

            //Zhuozi Sprint 2
            texture_Gommba = content.Load<Texture2D>("EnemyContent/WalkingGoomba1");
            texture_Koopa = content.Load<Texture2D>("EnemyContent/WalkingGreenKoopa1");
        }
        public void addAllBlocks(ArrayList blockList)
        {
            blockList.Add(getCoinBlockSprite());
            blockList.Add(getBrickBlockSprite());
        }
        public void addAllItems(ArrayList itemList)
        {
            itemList.Add(getFireFlowerSprite());
            itemList.Add(getStarSprite());
            itemList.Add(getGreenMushSprite());
            itemList.Add(getRedMushSprite());
            itemList.Add(getCoinSprite());
        }

        public void addAllEnemies(ArrayList enemyList)
        {
            enemyList.Add(getGommbaSprite());
            enemyList.Add(getKoopaSprite());
        }
        public ISprite getCoinBlockSprite()
        {
            return new BlockSprite(texture_CoinBlock, 1, 5, new Vector2(100, 100));
        }
        public ISprite getBrickBlockSprite() 
        {
            return new BlockSprite(texture_BrickBlock, 1, 5, new Vector2(200, 100));
        }
        public ISprite getFireFlowerSprite()
        {
            return new FireFlowerSprite(texture_FireFlower, new Vector2(100, 300), 1, 4);
        }
        public ISprite getStarSprite()
        {
            return new StarSprite(texture_Star, new Vector2(200, 300), 1, 4, _graphics, 1);
        }
        public ISprite getGreenMushSprite() 
        {
            return new GreenMushroomSprite(texture_GreenMush, new Vector2(300, 300), 1, 1, _graphics, 1);
            
            
        }
        public ISprite getRedMushSprite() {
            return new RedMushroomSprite(texture_RedMush, new Vector2(400, 300), 1, 1, _graphics, 1);
        }
        public ISprite getCoinSprite() 
        {
            return new CoinSprite(texture_Coin, new Vector2(500, 300), 1, 4);
        }
        public ISpriteE getGommbaSprite() 
        {
            return new GommbaSprite(texture_Gommba, new Vector2(500, 400), 1, 2, _graphics);
            
        }
        public ISpriteE getKoopaSprite()
        {
            
            return new GommbaSprite(texture_Koopa, new Vector2(500, 400), 1, 2, _graphics);
        }
        
    }
}
