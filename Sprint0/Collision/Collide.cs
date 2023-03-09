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
        public Collide(Game1 gameInstance)
        {
            this.gobj = gameInstance.gameObjectManager;
            this.factory = gameInstance.spritesFactory;
            game = gameInstance;
        }

        public void Update(GameTime gameTime)
        {
            

            // enemy and enemy does not collide
            // enemy and item does not collide

            // enemy and block
            foreach (MovingEnemy a in gobj.enemies)
            {
                Velocity = a.velocity;
                foreach (ISprite b in gobj.blocks)
                {

                    Rectangle RectangleMain = getRectangle(a);
                    Rectangle RectangleOBJ = getRectangle(b);
                    if (RectangleMain.Intersects(RectangleOBJ))
                    {
                        if (touchRight(RectangleMain, RectangleOBJ))
                        {
                            a.crash = true;
                        }
                        else if (touchLeft(RectangleMain, RectangleOBJ))
                        {
                            a.crash = true;
                        }
                        if (touchBottom(RectangleMain, RectangleOBJ))
                        {
                            a.moveDirectionY = 0;
                        }
                        else
                        {
                            a.moveDirectionY = 1;
                        }
                        
                    }


                }
            }

            // Item and block
            
            foreach (BlockSprite a in gobj.blocks)
            {
                foreach (ISprite b in gobj.items)
                {
                    Rectangle RectangleA = getRectangle(a);
                    Rectangle RectangleB = getRectangle(b);
                    if (RectangleA.Intersects(RectangleB))
                    {
                        a.crash = true;
                        if (TouchBottom(a, b))
                        {
                            b.velocity = new Vector2(b.velocity.X, 0);


                        }
                        if (TouchLeft(a, b) || TouchRight(a, b))
                        {
                            // move to the opposite direction
                        }

                    }
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
                        

                        if (touchRight(RectangleMain,RectangleOBJ))
                        {
                            cannotMoveRight(a);
                        }
                        else if (touchLeft(RectangleMain,RectangleOBJ))
                        {
                            cannotMoveLeft(a);
                        }
                        
                        else if (touchBottom(RectangleMain,RectangleOBJ))
                        {
                            cannotMoveDown(a);
                        }
                         

                         if (touchTop(RectangleMain,RectangleOBJ))
                        {
                            cannotMoveUP(a);
                            a.fallAfterJump();
                            BlockChangeManager changeState = new BlockChangeManager(b, game);
                            changeState.changeState();
                            break;
                        }

                    }
                }
            }


            foreach (Mario a in gobj.players)
            {
                foreach (SpriteE b in gobj.enemies)
                {

                    //if (found) { break; }
                    Rectangle RectangleA = getRectangle(a);
                    Rectangle RectangleB = getRectangle(b);
                    if (RectangleA.Intersects(RectangleB))
                    {
                        if(a.state == "Star")
                        {
                            EnemyChangeManager changeState = new EnemyChangeManager(b, game);
                            changeState.attackedByFireball();
                            changeState.changeState();
                            break;
                        }                    
                        else if (b.state == "idle" || TouchTop(a,b))
                        {
                            
                            EnemyChangeManager changeState = new EnemyChangeManager(b, game);    
                            changeState.changeState();
                            break;
                        }
                        else if(a.state == "Normal")
                        {
                            
                            if (b.state == "Normal" || b.state == "rolling")
                            {
                            //killed by enemy
                            a.crash = true;
                            a.TakeDamage();
                            //a.state = "Hurt";
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


        }

        
        public bool touchLeft(Rectangle mainInstance, Rectangle anotherInstance)
        {
            if (touchBottom(mainInstance, anotherInstance)){
                return false;
            }
            return mainInstance.Left < anotherInstance.Right && mainInstance.Right > anotherInstance.Right;
        }
        public bool touchBottom(Rectangle mainInstance, Rectangle anotherInstance)
        {
           return mainInstance.Bottom > anotherInstance.Top && mainInstance.Top < anotherInstance.Top;
        }
        public bool touchRight(Rectangle mainInstance, Rectangle anotherInstance)
        {
            if (touchBottom(mainInstance, anotherInstance)){
                return false;
            }
            return mainInstance.Right > anotherInstance.Left && mainInstance.Left < anotherInstance.Left;
        }
        public bool touchTop(Rectangle mainInstance, Rectangle anotherInstance)
        {
            if(mainInstance.Left < anotherInstance.Left - 15 || mainInstance.Right > anotherInstance.Right + 15)
            {
                return false;
            }
            return mainInstance.Top < anotherInstance.Bottom && mainInstance.Bottom > anotherInstance.Bottom;
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
            int startX = (int)a.Position.X;
            int startY = (int)a.Position.Y;
            int scale = Game1.scale;
            int easyTweakX = 0;
            int easyTweakY = 0;
            int endX = (((int)a.Texture.Width / a.cols) * scale) - easyTweakX;
            int endy = (((int)a.Texture.Height / a.rows ) * scale) + easyTweakY;   
            return new Rectangle(startX,startY,endX,endy);
        }
        public bool TouchLeft(ISprite collide1, ISprite collide2)
        {
            bool intersect = false;
            Rectangle RectangleA = new Rectangle((int)collide1.Position.X, (int)collide1.Position.Y, collide1.Texture.Width, collide1.Texture.Height);
            Rectangle RectangleB = new Rectangle((int)collide2.Position.X, (int)collide2.Position.Y, collide2.Texture.Width, collide2.Texture.Height);
            if (RectangleA.Right + collide1.velocity.X > RectangleB.Left && RectangleA.Left < RectangleB.Left && RectangleA.Bottom > RectangleB.Top && RectangleA.Top < RectangleB.Bottom)
            {
                intersect = true;
            }
            return intersect;
        }


        public bool TouchRight(ISprite collide1, ISprite collide2)
        {
            bool intersect = false;
            Rectangle RectangleA = new Rectangle((int)collide1.Position.X, (int)collide1.Position.Y, collide1.Texture.Width, collide1.Texture.Height);
            Rectangle RectangleB = new Rectangle((int)collide2.Position.X, (int)collide2.Position.Y, collide2.Texture.Width, collide2.Texture.Height);
            if (RectangleA.Left + collide1.velocity.X < RectangleB.Right && RectangleA.Right > RectangleB.Right && RectangleA.Bottom > RectangleB.Top && RectangleA.Top < RectangleB.Bottom)
            {
                intersect = true;
            }
            return intersect;
        }

        public bool TouchTop(ISprite collide1, ISprite collide2)
        {
            Rectangle RectangleA = new Rectangle((int)collide1.Position.X, (int)collide1.Position.Y, collide1.Texture.Width, collide1.Texture.Height);
            Rectangle RectangleB = new Rectangle((int)collide2.Position.X, (int)collide2.Position.Y, collide2.Texture.Width, collide2.Texture.Height);
            return RectangleA.Bottom + collide1.velocity.Y > RectangleB.Top &&
                RectangleA.Top < RectangleB.Top &&
                RectangleA.Right > RectangleB.Left &&
                RectangleA.Left < RectangleB.Right;
        }

        public bool TouchBottom(ISprite collide1, ISprite collide2)
        {
            bool intersect = false;
            Rectangle RectangleA = new Rectangle((int)collide1.Position.X, (int)collide1.Position.Y, collide1.Texture.Width, collide1.Texture.Height);
            Rectangle RectangleB = new Rectangle((int)collide2.Position.X, (int)collide2.Position.Y, collide2.Texture.Width, collide2.Texture.Height);
            return RectangleA.Top + collide1.velocity.Y < RectangleB.Bottom &&
           RectangleA.Bottom > RectangleB.Top &&
           RectangleA.Right > RectangleB.Left &&
           RectangleA.Left < RectangleB.Right;
        }
    }
}

