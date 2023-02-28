using Sprint0.ObjectManager;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Factory;
using Sprint0.Sprites;
using Microsoft.Xna.Framework;

namespace Sprint0.LevelLoader
{
    public static class LevelLoader
    {
        public static void loadLevel(GameObjectManager gameObjectManager, String level, SpritesFactory factory)
        {
            
            String type = "";
            XmlReader xml = XmlReader.Create(level);
            while(xml.Read())
            {

                if (xml.Name == "Blocks")
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
                else
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
                            processPlayer(xml, gameObjectManager, factory);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public static void processItem(XmlReader xml, GameObjectManager gameObjectManager, SpritesFactory factory)
        {
            
        }

        public static void processBlock(XmlReader xml, GameObjectManager gameObjectManager, SpritesFactory factory)
        {
            //ISprite obj = factory.getCoinBlockSprite();
            //obj.Position = new Vector2(50, 50);
            //gameObjectManager.addObject(obj, "block");
            switch (xml.Name)
            {
                case "CoinBlock":


                    break;
                default:
                    break;
            }
        }

        public static void processEnemy(XmlReader xml, GameObjectManager gameObjectManager, SpritesFactory factory)
        {

        }

        public static void processPlayer(XmlReader xml, GameObjectManager gameObjectManager, SpritesFactory factory)
        {

        }
    }
}
