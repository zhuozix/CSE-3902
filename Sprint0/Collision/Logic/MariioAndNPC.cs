using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer;
using Sprint0.MarioPlayer.State.PowerupState;
using Sprint0.NPC.Blocks;
using Sprint0.NPC.Enemy;
using Sprint0.NPC.StateChange;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Collision.Logic
{
    public class MarioAndNPC : ICollisionLogic
    {
        private Collide collide;
        private CollisionDetection collisionDetection;
        private List<ISprite> marioList;
        private List<ISprite> blockList;
        private List<ISprite> itemList;
        private List<ISprite> enemyList;
        private List<FireballSpawner> spawnerList;
        private float invisibeleTimer = 0f;
        private bool timerGo = false;
        public bool win;
        public MarioAndNPC(List<ISprite> marioListIn, List<ISprite> blockListIn, List<ISprite> itemListIn,List<ISprite>enemyListIn, List<FireballSpawner> spawners, Collide collideInstance)
        {
            marioList = marioListIn;
            blockList = blockListIn;
            itemList = itemListIn;
            enemyList = enemyListIn;
            spawnerList = spawners;
            collide = collideInstance;
            collisionDetection = new CollisionDetection();
            win = false;
        }
        public void update(GameTime gameTime)
        {
            
            if (timerGo)
            {
                invisibeleTimer = (float)gameTime.TotalGameTime.TotalMilliseconds;
            }

            if (invisibeleTimer >= 2f)
            {
                invisibeleTimer = 0f;
                timerGo = false;
            }
            if (!win)
            {
            marioAndblock(gameTime);
            marioAndEnemy(gameTime);
            marioAndItem(gameTime);
            }
            

        }

        public void marioAndItem(GameTime gameTime)
        {
            foreach (Mario a in marioList)
            {
                foreach (ISprite b in itemList)
                {
                    Rectangle RectangleA = collisionDetection.getRectangle(a);
                    Rectangle RectangleB = collisionDetection.getRectangle(b);
                    if (RectangleA.Intersects(RectangleB))
                    {
                        MarioPowerupStateType powerupStateType = a.CurrentPowerupState.GetEnumValue();
                        if (a.state == "Hurt" || powerupStateType == MarioPowerupStateType.Dead)
                        {
                            break;
                        }
                        //eat the item, and item disappear
                        ItemChangeManager changeState = new ItemChangeManager(b, collide.game);
                        changeState.changeState();
                        break;

                    }
                }
            }
        }

        public void marioAndEnemy(GameTime gameTime)
        {
            foreach (Mario a in marioList)
            {
                foreach (FireballSpawner b in spawnerList)
                {

                    if (b.fireBall == null) { break; }

                    Rectangle RectangleA = collisionDetection.getRectangle(a);
                    Rectangle RectangleB = collisionDetection.getRectangle(b.fireBall);
                   
                    if (RectangleA.Intersects(RectangleB))
                    {
                        if (a.state == "Hurt")
                        {
                            break;
                        }

                        if (a.state == "Star")
                        {
                            
                            break;
                        }
                        
                        else if (a.state == "Normal")
                        {

                            
                            a.crash = true;
                            a.TakeDamage();

                            a.state = "Hurt";
                            

                        }
                    }
                }

                foreach (EnemySprite b in enemyList)
                {

                    //if (found) { break; }
                    Rectangle RectangleA = collisionDetection.getRectangle(a);
                    Rectangle RectangleB = collisionDetection.getRectangle(b);
                    if (RectangleA.Intersects(RectangleB))
                    {
                        if (a.state == "Hurt")
                        {
                            break;
                        }

                        if (a.state == "Star")
                        {
                            EnemyChangeManager changeState = new EnemyChangeManager(b, collide.game);
                            changeState.attackedByFireball();
                            changeState.changeState();
                            break;
                        }
                        else if (b.state == "idle" || collisionDetection.touchBottomEnemy(RectangleA, RectangleB))
                        {

                            EnemyChangeManager changeState = new EnemyChangeManager(b, collide.game);
                            changeState.changeState();
                            break;


                        }
                        else if (a.state == "Normal")
                        {

                            if (b.state == "Normal" || b.state == "Rolling")
                            {
                                a.crash = true;
                                a.TakeDamage();

                                a.state = "Hurt";
                            }

                        }

                    }
                }


            }
        }

        public void marioAndblock(GameTime gameTime)
        {
            foreach (Mario a in marioList)
            {
                foreach (BlockSprite b in blockList)
                {
                    Rectangle RectangleMain = collisionDetection.getRectangle(a);
                    Rectangle RectangleOBJ = collisionDetection.getRectangle(b);
                    if (RectangleMain.Intersects(RectangleOBJ))
                    {
                        if (b.state == "Crashed")
                        {
                            break;
                        }


                        if (collisionDetection.touchRight(RectangleMain, RectangleOBJ))
                        {
                            cannotMoveRight(a);
                            int intersect = RectangleMain.Right - RectangleOBJ.Left;
                            if (intersect >= 2 && intersect <= 6) a.Position = new Vector2(a.Position.X - 2, a.Position.Y);
                            if (onlyOneTouchedBlock(a, b))
                            {
                                // a.fallAfterJump();
                                //a.velocity = new Vector2(a.velocity.X, 100);
                            }
                        }
                        else if (collisionDetection.touchLeft(RectangleMain, RectangleOBJ, a))
                        {
                            cannotMoveLeft(a);
                            int intersect = RectangleOBJ.Right - RectangleMain.Left;
                            if (intersect >= 2 && intersect <= 6) a.Position = new Vector2(a.Position.X + 2, a.Position.Y);
                            if (onlyOneTouchedBlock(a, b))
                            {
                                // a.fallAfterJump();
                                //a.velocity = new Vector2(a.velocity.X, 100);
                            }
                        }

                        if (collisionDetection.touchBottom(RectangleMain, RectangleOBJ) && b.Name != "InvisibleBlock")
                        {
                            
                            
                            cannotMoveDown(a);
                            int intersect = RectangleMain.Bottom - RectangleOBJ.Top;
                            if (intersect >= 2) a.Position = new Vector2(a.Position.X, a.Position.Y - 2);
                            

                        }
                        else if (collisionDetection.touchTop(RectangleMain, RectangleOBJ) && b.Name != "InvisibleBlock" && b.Name != "Platform")
                        {
                            
                            cannotMoveUP(a);

                            //a.fallAfterJump();
                            BlockChangeManager changeState = new BlockChangeManager(b, collide.game);
                            changeState.changeState();
                            break;
                        }





                        if (b.Name == "InvisibleBlock")
                        {
                            if (!collisionDetection.touchTop(RectangleMain, RectangleOBJ))
                            {
                                timerGo = true;
                            }

                            if (!timerGo && collisionDetection.touchTop(RectangleMain, RectangleOBJ))
                            {
                                cannotMoveUP(a);
                                //a.fallAfterJump();
                                BlockChangeManager changeState = new BlockChangeManager(b, collide.game);
                                changeState.changeState();
                                break;
                            }
                        }
                        if (b.Name == "FlagPoleBlock" && !win)
                        {
                            
                            a.poleslide();
                            win = true;
                            break;
                        }
                    }

                }

            }
        }

        public void cannotMoveDown(ISprite target)
        {
            //target.crash = true;
            if (target.velocity.Y > 0)
            {
                target.velocity = new Vector2(target.velocity.X, 0);
            }
        }
        public void cannotMoveUP(ISprite target)
        {
            //target.crash = true;
            if (target.velocity.Y < 0)
            {
                target.velocity = new Vector2(target.velocity.X, 0);
            }
        }

        public void cannotMoveLeft(ISprite target)
        {
            //target.crash = true;
            //target.Position= new Vector2(target.Position.X+1, target.Position.Y);
            if (target.velocity.X < 0)
            {
                target.velocity = new Vector2(0, target.velocity.Y);
            }
        }

        public void cannotMoveRight(ISprite target)
        {
            //target.crash = true;
            //target.Position = new Vector2(target.Position.X - 1, target.Position.Y);
            if (target.velocity.X > 0)
            {
                target.velocity = new Vector2(0, target.velocity.Y);
            }
        }

        public bool onlyOneTouchedBlock(ISprite a, ISprite b)
        {
            foreach (ISprite target in blockList)
            {
                Rectangle rect1 = collisionDetection.getRectangle(a);
                Rectangle rect2 = collisionDetection.getRectangle(target);
                if (rect1.Intersects(rect2))
                {
                    if (!target.Equals(b)) { return false; }
                }
            }

            return true;
        }

    }
}
