using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.NPC.Boss
{


    public class BossFactory
    {
        private ContentManager _contentManager;
        private string _fileLocation;
        private Texture2D _texture;
        private Boss KingKoopa;

        private int rows;
        private int columns;

        public BossFactory(ContentManager contentManagerIn, Boss KingKoopaIn) 
        {
            _contentManager = contentManagerIn;
            KingKoopa = KingKoopaIn;
        }
        public Sprite buildSprite()
        {
            _fileLocation = findLocation();
            _texture = _contentManager.Load<Texture2D>("Bowser/bowser");
            updateRowsCols();
            KingKoopa.cols = columns;
            KingKoopa.rows = rows;
            return new NoneMovingAnimatedSprite(_texture, Vector2.Zero, rows, columns);

        }
        public void updateRowsCols()
        {
            rows = 1;
            if(KingKoopa.currentActionType == BossActionType.Running)
            {
                columns = 4;
            }
            else
            {
                columns = 4;
            }
        }
        public string findLocation()
        {
            string fileLocation = "Bowser/";

            if (KingKoopa.isFlying)
            {
                fileLocation += "fly";
            }
            else { 
                switch (KingKoopa.currentActionType)
                {
                    case BossActionType.Idle:
                        fileLocation += "bowser";
                        break;
                    case BossActionType.Running:
                        fileLocation += "bowser";
                        break;
                    case BossActionType.Jumping:
                        fileLocation += "bowser";
                        break;
                    case BossActionType.Falling:
                        fileLocation += "boswer";
                        break;
                    default:
                        throw new ArgumentException("SpriteFactory error: Invalid Type specified");
                }
            }
            return fileLocation;
        }
        

    }
}
