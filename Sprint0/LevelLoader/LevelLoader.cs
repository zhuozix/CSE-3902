using Sprint0.ObjectManager;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Factory;
using Sprint0.Sprites;
using Sprint0.LevelLoader;
using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.ComponentModel;
using Sprint0.Collision;
using Sprint0.NPC.Blocks;

namespace Sprint0.LevelLoader
{
    public static class LevelLoader
    {
        public static void loadLevel(GameObjectManager gameObjectManager, String level, SpritesFactory factory, Game1 game)
        {
            
            String type = "";
            XmlReader xml = XmlReader.Create(Directory.GetCurrentDirectory().Replace(@"bin\Debug\net6.0", @"\LevelLoader\") + level);
            while(xml.Read())
            {
                if (xml.Name == "Background")
                {
                    type = "background";
                }
                else if (xml.Name == "Blocks")
                {
                    type = "block";
                }
                else if (xml.Name == "Enemies")
                {
                    type = "enemy";
                }
                else if (xml.Name == "Items")
                {
                    type = "item";
                }
                else if (xml.Name == "Player")
                {
                    type = "player";
                } 
                else if (xml.NodeType == XmlNodeType.Element)
                {
                    switch (type)
                    {
                        case "block":
                            processBlock(xml, gameObjectManager, factory);
                            break;
                        case "item":
                            processItem(xml, gameObjectManager, factory); 
                            break;
                        case "enemy":
                            processEnemy(xml, gameObjectManager, factory);
                            break;
                        case "player":
                            processPlayer(xml, gameObjectManager, factory, game);
                            break;
                        case "background":
                            processBackground(xml, gameObjectManager, factory);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public static void processItem(XmlReader xml, GameObjectManager gameObjectManager, SpritesFactory factory)
        {
            long xPos = Int64.Parse(xml.GetAttribute("xPos"));
            long yPos = Int64.Parse(xml.GetAttribute("yPos"));
            ISprite obj;
            switch (xml.Name)
            {
                case "Coin":
                    obj = factory.getCoinSprite();
                    obj.Position = new Vector2(xPos, yPos);
                    gameObjectManager.addObject(obj, "item");
                    break;
                case "FireFlower":
                    obj = factory.getFireFlowerSprite();
                    obj.Position = new Vector2(xPos, yPos);
                    gameObjectManager.addObject(obj, "item");
                    break;
                case "Star":
                    obj = factory.getStarSprite();
                    obj.Position = new Vector2(xPos, yPos);
                    gameObjectManager.addObject(obj, "item");
                    break;
                case "RedMushroom":
                    obj = factory.getRedMushSprite();
                    obj.Position = new Vector2(xPos, yPos);
                    gameObjectManager.addObject(obj, "item");
                    break;
                case "GreenMushroom":
                    obj = factory.getGreenMushSprite();
                    obj.Position = new Vector2(xPos, yPos);
                    gameObjectManager.addObject(obj, "item");
                    break;
                default:
                    break;
            }
        }

        public static void processBlock(XmlReader xml, GameObjectManager gameObjectManager, SpritesFactory factory)
        {
            long xPos2;
            long yPos2;
            long xPos = Int64.Parse(xml.GetAttribute("xPos"));
            long yPos = Int64.Parse(xml.GetAttribute("yPos"));
            ISprite obj;
            switch (xml.Name)
            {
                case "CoinBlock":
                    obj = factory.getCoinBlockSprite();
                    obj.Position = new Vector2(xPos, yPos);
                    gameObjectManager.addObject(obj, "block");
                    break;
                case "FloorBlock":
                    obj = factory.getFloorBlockSprite();
                    obj.Position = new Vector2(xPos, yPos);
                    gameObjectManager.addObject(obj, "block");
                    break;
                case "StairBlock":
                    obj = factory.getStairBlockSprite();
                    obj.Position = new Vector2(xPos, yPos);
                    gameObjectManager.addObject(obj, "block");
                    break;
                case "BrickBlock":
                    obj = factory.getBrickBlockSprite();
                    obj.Position = new Vector2(xPos, yPos);
                    gameObjectManager.addObject(obj, "block");
                    break;
                case "InvisibleBlock":
                    obj = factory.getInvisibleBlock();
                    obj.Position = new Vector2(xPos, yPos);
                    gameObjectManager.addObject(obj, "block");
                    break;
                case "UsedBlock":
                    obj = factory.getUsedBlockSprite();
                    obj.Position = new Vector2(xPos, yPos);
                    gameObjectManager.addObject(obj, "block");
                    break;
                case "GreenPipeLarge":
                    obj = factory.getGreenPipeLargeSprite();
                    obj.Position = new Vector2(xPos, yPos);
                    gameObjectManager.addObject(obj, "block");
                    break;
                case "GreenPipeMedium":
                    obj = factory.getGreenPipeMediumSprite();
                    obj.Position = new Vector2(xPos, yPos);
                    gameObjectManager.addObject(obj, "block");
                    break;
                case "GreenPipeSmall":
                    obj = factory.getGreenPipeSmallSprite();
                    obj.Position = new Vector2(xPos, yPos);
                    gameObjectManager.addObject(obj, "block");
                    break;
                case "FloorBlockStretch":
                    xPos2 = Int64.Parse(xml.GetAttribute("xPos2"));
                    while(xPos <= xPos2)
                    {
                        obj = factory.getFloorBlockSprite();
                        obj.Position = new Vector2(xPos, yPos);
                        gameObjectManager.addObject(obj, "block");
                        xPos += 16 * Game1.scale;
                    }
                    break;
                case "StairBlockStretch":
                    xPos2 = Int64.Parse(xml.GetAttribute("xPos2"));
                    while (xPos <= xPos2)
                    {
                        obj = factory.getStairBlockSprite();
                        obj.Position = new Vector2(xPos, yPos);
                        gameObjectManager.addObject(obj, "block");
                        xPos += 16 * Game1.scale;
                    }
                    break;
                case "Teleporter":
                    xPos2 = xPos + Int64.Parse(xml.GetAttribute("width"));
                    yPos2 = yPos - Int64.Parse(xml.GetAttribute("height"));
                    Rectangle rec = new Rectangle((int)xPos, (int)yPos2, (int)xPos2, (int)yPos);
                    gameObjectManager.addTeleporter(new Teleporter(rec, xml.GetAttribute("activation"), Int32.Parse(xml.GetAttribute("xDest")), Int32.Parse(xml.GetAttribute("yDest")), xml.GetAttribute("underground").Equals("True")));
                    break;
                default:
                    break;
            }
        }

        public static void processEnemy(XmlReader xml, GameObjectManager gameObjectManager, SpritesFactory factory)
        {
            long xPos = Int64.Parse(xml.GetAttribute("xPos"));
            long yPos = Int64.Parse(xml.GetAttribute("yPos"));
            ISprite obj;
            switch (xml.Name)
            {
                case "Goomba":
                    obj = factory.getGommbaSprite();
                    obj.Position = new Vector2(xPos, yPos);
                    
                    gameObjectManager.addObject(obj, "enemy");
                    break;
                case "Koopa":
                    obj = factory.getKoopaSprite();
                    obj.Position = new Vector2(xPos, yPos);
                    gameObjectManager.addObject(obj, "enemy");
                    break;
                default:
                    break;
            }
        }

        public static void processPlayer(XmlReader xml, GameObjectManager gameObjectManager, SpritesFactory factory, Game1 game)
        {
            long xPos = Int64.Parse(xml.GetAttribute("xPos"));
            long yPos = Int64.Parse(xml.GetAttribute("yPos"));
            switch (xml.Name)
            {
                case "Mario":
                    game.mario = new Mario(new Vector2(xPos, yPos), game);
                    game.mario.Position = new Vector2(xPos, yPos);
                    gameObjectManager.addObject(game.mario, "player");

                    break;
                default:
                    break;
            }
        }

        public static void processBackground(XmlReader xml, GameObjectManager gameObjectManager, SpritesFactory factory)
        {
            if (xml.Name.Contains("bg"))
            {
                gameObjectManager.addBackground(factory.getBackgroundSprite(xml.Name));
            }
        }
    }
}
