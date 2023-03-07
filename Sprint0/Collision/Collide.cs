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
            bool found = false;

            // enemy and enemy does not collide
            // enemy and item does not collide

            // enemy and block
            foreach (MovingEnemy a in gobj.enemies)
            {
                Velocity = a.velocity;
                foreach (BlockSprite b in gobj.blocks)
                {
                    if (found) { break; }

                    Rectangle RectangleA = new Rectangle((int)a.Position.X, (int)a.Position.Y, (int)(a.Texture.Width / 2), a.Texture.Height);
                    Rectangle RectangleB = new Rectangle((int)b.Position.X, (int)b.Position.Y, (int)(a.Texture.Width), a.Texture.Height);
                    if (RectangleA.Intersects(RectangleB))
                    {
                        a.crash = true;
                    }


                }
            }

            // Item and block
            found = false;
            foreach (BlockSprite a in gobj.blocks)
            {
                foreach (ISprite b in gobj.items)
                {
                    if (found) { break; }
                    Rectangle RectangleA = new Rectangle((int)a.Position.X, (int)a.Position.Y, (int)(a.Texture.Width), a.Texture.Height);
                    Rectangle RectangleB = new Rectangle((int)b.Position.X, (int)b.Position.Y, (int)(a.Texture.Width / 2), a.Texture.Height);
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
            found = false;
            foreach (Mario a in gobj.players)
            {
                Velocity = a.velocity;
                foreach (BlockSprite b in gobj.blocks)
                {
                    if (found) { break; }
                    Rectangle RectangleA = new Rectangle((int)a.Position.X, (int)a.Position.Y, (int)(a.Texture.Width), a.Texture.Height * 2);
                    Rectangle RectangleB = new Rectangle((int)b.Position.X, (int)b.Position.Y, (int)(a.Texture.Width), a.Texture.Height * 2);
                    Rectangle = RectangleA;
                    if (Rectangle.Intersects(RectangleB))
                    {
                        a.crash = true;
                        if (TouchTop(a, b))
                        {
                            if (a.velocity.Y > 0)
                            {
                                a.velocity = new Vector2(a.velocity.X, 0);

                            }

                        }
                        else if (TouchBottom(a, b))
                        {
                            //a.velocity = new Vector2(a.velocity.X, 150);
                            a.fallAfterJump();
                            BlockChangeManager changeState = new BlockChangeManager(b, game);
                            changeState.changeState();
                            found = true;
                            break;

                        }
                        if (TouchLeft(a, b))
                        {
                            if (a.velocity.X > 0)
                            {
                                a.velocity = new Vector2(0, a.velocity.Y);
                            }
                            a.velocity = Vector2.Zero;
                        }
                        if (TouchRight(a, b))
                        {
                            if (a.velocity.X < 0)
                            {
                                a.velocity = new Vector2(0, a.velocity.Y);
                            }
                            a.velocity = Vector2.Zero;
                        }

                    }
                    else
                    {
                        a.crash = false;
                    }
                }
            }


            //mario and enemy
            found = false;
            foreach (Mario a in gobj.players)
            {
                foreach (ISprite b in gobj.enemies)
                {

                    if (found) { break; }
                    Rectangle RectangleA = new Rectangle((int)a.Position.X, (int)a.Position.Y, (int)(a.Texture.Width), a.Texture.Height);
                    Rectangle RectangleB = new Rectangle((int)b.Position.X, (int)b.Position.Y, (int)(a.Texture.Width / 2), a.Texture.Height);
                    if (b.state == "Normal" && RectangleA.Intersects(RectangleB))
                    {
                        //Kill the enemy
                        if (RectangleB.Top > RectangleA.Top)
                        {
                            found = true;
                            EnemyChangeManager changeState = new EnemyChangeManager(b, game);
                            
                            changeState.changeState();
                            break;
                        }
                        else
                        {
                            //killed by enemy
                            a.crash = true;
                            a.TakeDamage();
                        }

                    }
                }

                


                
            }
            
            // Item and mario
            found = false;
            foreach (Mario a in gobj.players)
            {
                foreach (ISprite b in gobj.items)
                {
                    if (found) { break; }
                    Rectangle RectangleA = new Rectangle((int)a.Position.X, (int)a.Position.Y, (int)(a.Texture.Width), a.Texture.Height);
                    Rectangle RectangleB = new Rectangle((int)b.Position.X, (int)b.Position.Y, (int)(a.Texture.Width / 2), a.Texture.Height);
                    if (RectangleA.Intersects(RectangleB))
                    {
                        //eat the item, and item disappear

                    }
                }
            }


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

