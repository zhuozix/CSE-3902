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
        Boss KingKoopa;

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
            _texture = _contentManager.Load<Texture2D>(_fileLocation);
            updateRowsCols();
            return new NoneMovingAnimatedSprite(_texture, KingKoopa.Position, rows, columns);

        }
        public void updateRowsCols()
        {
            rows = 1;
            if(KingKoopa.currentActionType == BossActionType.Running)
            {
                columns = 3;
            }
            else
            {
                columns = 1;
            }
        }
        public string findLocation()
        {
            string fileLocation = "Boss/";

            if (KingKoopa.isFlying)
            {
                fileLocation += "fly";
            }
            else { 
                switch (KingKoopa.currentActionType)
                {
                    case BossActionType.Idle:
                        fileLocation += "idle";
                        break;
                    case BossActionType.Running:
                        fileLocation += "run";
                        break;
                    case BossActionType.Jumping:
                        fileLocation += "jump";
                        break;
                    case BossActionType.Falling:
                        fileLocation += "fall";
                        break;
                    default:
                        throw new ArgumentException("SpriteFactory error: Invalid Type specified");
                }
            }
            return fileLocation;
        }
        

    }
}
