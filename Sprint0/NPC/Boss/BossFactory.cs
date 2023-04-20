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
            return new NoneMovingAnimatedSprite(_texture, Vector2.Zero, KingKoopa.rows, KingKoopa.cols);

        }
        public void updateRowsCols()
        {
            KingKoopa.rows = 1;
            if(KingKoopa.currentActionType == BossActionType.Running)
            {
                KingKoopa.cols = 4;
            }
            else
            {
                KingKoopa.cols = 1;
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
                    case BossActionType.Running:
                        fileLocation += "bowser";
                        break;
                    default:
                        fileLocation += "idle";
                        break;
                }
            }
            return fileLocation;
        }
        

    }
}
