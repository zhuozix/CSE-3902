using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer;
using Sprint0.NPC.Blocks;
using Sprint0.NPC.Boss;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Collision.Logic
{
    public class BossVSMario :ICollisionLogic
    {
        private Collide collide;
        private CollisionDetection collisionDetection;
        private List<ISprite> marioList;
        private List<ISprite> bossList;
        private List<ISprite> fireballList;

        public BossVSMario(List<ISprite> marioListIn, List<ISprite> fireballListIn, List<ISprite> bossListIn, Collide collideInstance)
        {
            marioList = marioListIn;
            bossList = bossListIn;
            fireballList = fireballListIn;
            collide = collideInstance;
            collisionDetection = new CollisionDetection();
        }

        public void update(GameTime gameTime) 
        {
            BossHitMario(gameTime);
            MarioHitBoss(gameTime);
            FireBallHitBoss(gameTime);
        }

        public void BossHitMario(GameTime gameTime) {}
        public void MarioHitBoss(GameTime gameTime) { }
        public void FireBallHitBoss(GameTime gameTime) { }
    }
}
