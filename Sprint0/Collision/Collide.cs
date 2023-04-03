using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.ObjectManager;
using Sprint0.Sprites;
using System.Collections;
using Sprint0.NPC.Blocks;
using Sprint0.MarioPlayer;
using Sprint0.Content;
using Sprint0.Factory;
using Sprint0.NPC.Enemy;
using Sprint0.NPC.StateChange;
using Sprint0.Command.PlayerCMD;
using Sprint0.NPC.Item;
using Sprint0.MarioPlayer.State.PowerupState;
using Sprint0.MarioPlayer.State.ActionState;
using static System.Formats.Asn1.AsnWriter;

namespace Sprint0.Collision
{
    public class Collide : ICollision
    {

        private Texture2D _texture;
        public Vector2 Position;
        public Vector2 Velocity { get; set; }
        public Rectangle Rectangle { get; set; }
        private GameObjectManager gobj;
        private SpritesFactory factory;
        // public bool collideA { get; set; }
        public bool collideblock { get; set; }
        public bool collideMario { get; set; }
        public List<bool> collideEnemy { get; set; }
        public List<bool> collidePlayer { get; set; }
        public bool fall { get; set; }
        Game1 game;

        private float invisibeleTimer = 0f;
        private bool timerGo = false;

        public Collide(Game1 gameInstance)
        {
            this.gobj = gameInstance.gameObjectManager;
            this.factory = gameInstance.spritesFactory;
            game = gameInstance;
        }

        public void Update(GameTime gameTime)
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


            // enemy and enemy 
            foreach (ISprite a in gobj.enemies)
            {
                bool killed = false;

                foreach (ISprite b in gobj.enemies)
                {
                    if (a.Equals(b))
                    {
                        break;
                    }
                    Rectangle RectangleEnemyA = getRectangle(b);
                    Rectangle RectangleEnemyB = getRectangle(a);
                    if (RectangleEnemyA.Intersects(RectangleEnemyB))
                    {
                        if (a.state == "Rolling")
                        {
                            EnemyChangeManager changeState = new EnemyChangeManager(b, game);
                            changeState.attackedByFireball();
                            changeState.changeState();
                            killed = true;
                            break;
                        }
                        else
                        {
                            a.crash = true;
                            b.crash = true;
                        }
                    }
                }
                if (killed)
                {
                    break;
                }
            }
            // enemy and item does not collide

            // enemy and block
            foreach (EnemySprite a in gobj.enemies)
            {
                bool touched = false;
                foreach (ISprite b in gobj.blocks)
                {

                    Rectangle RectangleBlock = getRectangle(b);
                    Rectangle RectangleItem = getRectangle(a);
                    if (RectangleBlock.Intersects(RectangleItem))
                    {
                        touched = true;
                        if (touchBottom(RectangleItem, RectangleBlock, a))
                        {
                            a.velocity = new Vector2(a.velocity.X, 0);
                        }

                        if (touchRight(RectangleItem, RectangleBlock, a))
                        {
                            a.crash = true;
                        }
                        else if (touchLeft(RectangleItem, RectangleBlock, a))
                        {
                            a.crash = true;
                        }


                    }
                }
                if (!touched)
                {
                    a.velocity = new Vector2(a.velocity.X, 100);
                }
            }

            // Item and block

            foreach (ISprite a in gobj.items)
            {
                bool touched = false;
                foreach (ISprite b in gobj.blocks)
                {

                    Rectangle RectangleBlock = getRectangle(b);
                    Rectangle RectangleItem = getRectangle(a);
                    if (RectangleBlock.Intersects(RectangleItem))
                    {
                        //block hit item
                        if(b.velocity.X < 0)
                        {
                            a.velocity = new Vector2(b.velocity.X, a.velocity.Y);
                            break;
                        }

                        //item hit block
                        touched = true;
                        if (touchBottom(RectangleItem, RectangleBlock, a))
                        {
                            a.velocity = new Vector2(a.velocity.X, 0);
                        }

                        if (touchRight(RectangleItem, RectangleBlock, a))
                        {
                            a.crash = true;
                        }
                        else if (touchLeft(RectangleItem, RectangleBlock, a))
                        {
                            a.crash = true;
                        }


                    }
                }
                if (!touched)
                {
                    a.velocity = new Vector2(a.velocity.X, 100);
                }
            }

            //Mario and block
            foreach (Mario a in gobj.players)
            {
                foreach (BlockSprite b in gobj.blocks)
                {
                    Rectangle RectangleMain = getRectangle(a);
                    Rectangle RectangleOBJ = getRectangle(b);
                    if (RectangleMain.Intersects(RectangleOBJ))
                    {
                        if (b.state == "Crashed")
                        {
                            break;
                        }

                        
                        if (touchBottom(RectangleMain, RectangleOBJ, a) && b.Name != "InvisibleBlock")
                        {
                            cannotMoveDown(a);

                        }else if (touchTop(RectangleMain, RectangleOBJ, a) && b.Name != "InvisibleBlock")
                        {
                            cannotMoveUP(a);
                            //a.fallAfterJump();
                            BlockChangeManager changeState = new BlockChangeManager(b, game);
                            changeState.changeState();
                            break;
                        }
                        
                        if (touchRight(RectangleMain, RectangleOBJ, a))
                        {
                            cannotMoveRight(a);
                            if (onlyOneTouchedBlock(a, b))
                            {
                                // a.fallAfterJump();
                                a.velocity = new Vector2(a.velocity.X, 100);
                            }
                        }
                        else if (touchLeft(RectangleMain, RectangleOBJ, a))
                        {
                            cannotMoveLeft(a);
                            if (onlyOneTouchedBlock(a, b))
                            {
                                // a.fallAfterJump();
                                a.velocity = new Vector2(a.velocity.X, 100);
                            }

                        }

                        if (b.Name == "InvisibleBlock")
                        {
                            if (!touchTop(RectangleMain, RectangleOBJ, a))
                            {
                                timerGo = true;
                            }

                            if (!timerGo && touchTop(RectangleMain, RectangleOBJ, a))
                            {
                                cannotMoveUP(a);
                                //a.fallAfterJump();
                                BlockChangeManager changeState = new BlockChangeManager(b, game);
                                changeState.changeState();
                                break;
                            }
                        }
                        if (b.Name == "FlagPoleBlock")
                        {
                            cannotMoveUP(a);
                            cannotMoveRight(a);
                            cannotMoveLeft(a);
                            cannotMoveDown(a);
                            a.poleslide();

                            break;
                        }
                    }

                }

            }

            //Mario and enemis
            foreach (Mario a in gobj.players)
            {
                foreach (EnemySprite b in gobj.enemies)
                {

                    //if (found) { break; }
                    Rectangle RectangleA = getRectangle(a);
                    Rectangle RectangleB = getRectangle(b);
                    if (RectangleA.Intersects(RectangleB))
                    {
                        if (a.state == "Hurt")
                        {
                            break;
                        }

                        if (a.state == "Star")
                        {
                            EnemyChangeManager changeState = new EnemyChangeManager(b, game);
                            changeState.attackedByFireball();
                            changeState.changeState();
                            break;
                        }
                        else if (b.state == "idle" || touchBottomEnemy(RectangleA, RectangleB))
                        {

                            EnemyChangeManager changeState = new EnemyChangeManager(b, game);
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

            // Item and mario
            foreach (Mario a in gobj.players)
            {
                foreach (ISprite b in gobj.items)
                {
                    Rectangle RectangleA = getRectangle(a);
                    Rectangle RectangleB = getRectangle(b);
                    if (RectangleA.Intersects(RectangleB))
                    {
                        MarioPowerupStateType powerupStateType = a.CurrentPowerupState.GetEnumValue();
                        if (a.state == "Hurt" || powerupStateType == MarioPowerupStateType.Dead)
                        {
                            break;
                        }
                        //eat the item, and item disappear
                        ItemChangeManager changeState = new ItemChangeManager(b, game);
                        changeState.changeState();
                        break;

                    }
                }
            }

            //Fireball and mario
            foreach (ISprite a in gobj.fireBallList)
            {
                bool find = false;
                foreach (ISprite b in gobj.players)
                {
                    Rectangle RectangleA = getRectangle(a);
                    Rectangle RectangleB = getRectangle(b);
                    if (RectangleA.Intersects(RectangleB))
                    {
                        find = true;
                        FireballChangeManager changeFireBall = new FireballChangeManager(b, game);
                        changeFireBall.changeState();
                        break;
                    }
                    if (find) { break; }
                }
            }

            //Fireball and enemy
            foreach (ISprite a in gobj.fireBallList)
            {
                bool find = false;
                foreach (ISprite b in gobj.enemies)
                {
                    Rectangle RectangleA = getRectangle(a);
                    Rectangle RectangleB = getRectangle(b);
                    if (RectangleA.Intersects(RectangleB))
                    {
                        find = true;
                        EnemyChangeManager changeState = new EnemyChangeManager(b, game);
                        changeState.attackedByFireball();
                        changeState.changeState();
                        FireballChangeManager changeFireBall = new FireballChangeManager(b, game);
                        changeFireBall.changeState();
                        break;
                    }
                    if (find) { break; }
                }
            }

            //Fireball and block
            foreach (ISprite a in gobj.fireBallList)
            {

                foreach (ISprite b in gobj.blocks)
                {

                    Rectangle RectangleBlock = getRectangle(b);
                    Rectangle RectangleItem = getRectangle(a);
                    if (RectangleBlock.Intersects(RectangleItem))
                    {

                        if (touchBottom(RectangleItem, RectangleBlock, a))
                        {
                            a.velocity = new Vector2(a.velocity.X, 0);
                        }

                        if (touchRight(RectangleItem, RectangleBlock, a))
                        {
                            a.crash = true;
                        }
                        else if (touchLeft(RectangleItem, RectangleBlock, a))
                        {
                            a.crash = true;
                        }


                    }
                }
            }


        }

        

        public bool touchLeft(Rectangle mainInstance, Rectangle anotherInstance, ISprite mainSprite)
        {
            if (!leftXTouchMain(mainInstance, anotherInstance, mainSprite))
            {
                return false;
            }
            else if (touchBottom(mainInstance, anotherInstance, mainSprite)) { return false; }
            return mainInstance.Left < anotherInstance.Right && mainInstance.Right > anotherInstance.Right;
        }
        public bool touchBottom(Rectangle mainInstance, Rectangle anotherInstance, ISprite mainSprite)
        {
            return mainInstance.Bottom - anotherInstance.Top <= 20 && mainInstance.Bottom < anotherInstance.Bottom;
            //return mainInstance.Bottom - anotherInstance.Top <= 5 
        }
        public bool touchBottomEnemy(Rectangle mainInstance, Rectangle anotherInstance)
        {
            return mainInstance.Bottom > anotherInstance.Top && mainInstance.Bottom < anotherInstance.Bottom;
            //return mainInstance.Bottom > anotherInstance.Top && mainInstance.Bottom < anotherInstance.Bottom;
        }
        
        public bool touchRight(Rectangle mainInstance, Rectangle anotherInstance, ISprite mainSprite)
        {
            if (!rightXTouchMain(mainInstance, anotherInstance, mainSprite))
            {
                return false;
            }
            else if (touchBottom(mainInstance, anotherInstance, mainSprite)) { return false; }
            return mainInstance.Right > anotherInstance.Left && mainInstance.Left < anotherInstance.Left;
        }
        public bool touchTop(Rectangle mainInstance, Rectangle anotherInstance, ISprite mainSprite)
        {
            if (mainInstance.Left < anotherInstance.Left - 15 || mainInstance.Right > anotherInstance.Right + 15)
            {
                return false;
            }
            return anotherInstance.Bottom - mainInstance.Top <= 10;

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
            if (target.velocity.X < 0)
            {
                target.velocity = new Vector2(0, target.velocity.Y);
            }
        }

        public void cannotMoveRight(ISprite target)
        {
            //target.crash = true;
            if (target.velocity.X > 0)
            {
                target.velocity = new Vector2(0, target.velocity.Y);
            }
        }

        private Rectangle getRectangle(ISprite a)
        {
            int scale = Game1.scale;
            int startX = (int)a.Position.X;
            int startY = (int)a.Position.Y;

            int endX = (((int)a.Texture.Width / a.cols) * scale);
            int endy = (((int)a.Texture.Height / a.rows) * scale);
            return new Rectangle(startX, startY, endX, endy);
        }

        private Rectangle GetFutureRectangle(ISprite sprite)
        {
            int scale = Game1.scale;
            int endX = (((int)sprite.Texture.Width / sprite.cols) * scale);
            int endy = (((int)sprite.Texture.Height / sprite.rows) * scale);
            return new Rectangle(
                (int)(sprite.Position.X + sprite.velocity.X),
                (int)(sprite.Position.Y + sprite.velocity.Y),
                endX, endy);
        }


        private bool onlyOneTouchedBlock(ISprite a, ISprite b)
        {
            foreach (ISprite target in gobj.blocks)
            {
                Rectangle rect1 = getRectangle(a);
                Rectangle rect2 = getRectangle(target);
                if (rect1.Intersects(rect2))
                {
                    if (!target.Equals(b)) { return false; }
                }
            }

            return true;
        }
        /*
        private bool rightXTouchMain(Rectangle a, Rectangle b, ISprite c)
        {
            return Math.Abs(b.Left - a.Right) <= Math.Abs(a.Bottom - b.Top);
        }

        private bool leftXTouchMain(Rectangle a, Rectangle b, ISprite c)
        {
            return Math.Abs(b.Right - a.Left) <= Math.Abs(a.Bottom - b.Top);
        }*/
        private bool rightXTouchMain(Rectangle a, Rectangle b, ISprite c)
        {
            return Math.Abs(b.Left - a.Right) <= Math.Abs(a.Bottom - b.Top);
        }

        private bool leftXTouchMain(Rectangle a, Rectangle b, ISprite c)
        {
            return Math.Abs(b.Right - a.Left) <= Math.Abs(a.Bottom - b.Top);
        }

    }
}

