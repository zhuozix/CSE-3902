using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer;
using Sprint0.NPC.Blocks;
using Sprint0.NPC.Boss;
using Sprint0.NPC.StateChange;
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
        private Game1 _game;

        public BossVSMario(List<ISprite> marioListIn, List<ISprite> fireballListIn, List<ISprite> bossListIn, Collide collideInstance, Game1 game)
        {
            marioList = marioListIn;
            bossList = bossListIn;
            fireballList = fireballListIn;
            collide = collideInstance;
            collisionDetection = new CollisionDetection();
            _game = game;
        }

        public void update(GameTime gameTime) 
        {
            BossAndMario(gameTime);
            FireBallHitBoss(gameTime);
        }

        public void hurtMario(Mario m)
        {
            m.crash = true;
            m.TakeDamage();
            m.state = "Hurt";
        }

        public void BossAndMario(GameTime gameTime) {
            foreach(Mario player in marioList)
            {
                foreach(Boss boss in bossList)
                {
                    if(_game.bossHP <= 0)
                    {
                        break;
                    }
                    Rectangle playerRectangle = collisionDetection.getRectangle(player);
                    Rectangle bossRectangle = collisionDetection.getRectangle(boss);
                    if (playerRectangle.Intersects(bossRectangle))
                    {
                        
                        if (collisionDetection.touchBottomEnemy(playerRectangle, bossRectangle))
                        {
                            if (!boss._ai.noDmgLock)
                            {
                                BossStateChange stateChange = new BossStateChange(boss, _game);
                                stateChange.hitByMario();
                            }
                            
                            //push mario
                            player.Position = new Vector2(player.Position.X, player.Position.Y - 10);
                            int xVelocity = -100;
                            if (player.IsFacingRight)
                            {
                                xVelocity = 100;
                            }
                            int yVelocity = -280;
                            if(player.Position.Y <= 90)
                            {
                                yVelocity = -50;
                            }
                            player.velocity = new Vector2(xVelocity, yVelocity);
                            player.Jump();
                            
                        }
                        else
                        {
                            if (player.state != "Hurt")
                            {
                                hurtMario(player);
                            }
                            
                        }

                    }
                }
            }
        }
        public void FireBallHitBoss(GameTime gameTime) {
            foreach (ISprite a in fireballList)
            {
                bool find = false;
                foreach (Boss b in bossList)
                {
                    Rectangle RectangleA = collisionDetection.getRectangle(a);
                    Rectangle RectangleB = collisionDetection.getRectangle(b);
                    if (RectangleA.Intersects(RectangleB))
                    {
                        find = true;
                        if (!b._ai.noDmgLock)
                        {
                            BossStateChange stateChange = new BossStateChange(b, _game);
                            stateChange.hitByFireball();
                        }
                        FireballChangeManager changeFireBall = new FireballChangeManager(b, collide.game);
                        changeFireBall.changeState();

                        break;
                    }
                    if (find) { break; }
                }
            }
        }
    }
}
