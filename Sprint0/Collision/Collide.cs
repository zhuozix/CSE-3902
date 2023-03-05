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
using Sprint0.Enemy;
using Sprint0.Blocks;
using Sprint0.MarioPlayer;

namespace Sprint0.Collision
{
    public class Collide : ICollision
    {
        private Texture2D _texture;
        public Vector2 Position;
        public Vector2 Velocity { get; set; }
        public Rectangle Rectangle { get; set; }
        private GameObjectManager gobj;
       // public bool collideA { get; set; }
        public bool collideblock { get; set; }
        public bool collideMario { get; set; }
        public List<bool> collideEnemy { get; set; }
        public List<bool> collidePlayer { get; set; }
        public bool fall { get; set; }
        public Collide(GameObjectManager gameObjectManager)
        {
            this.gobj = gameObjectManager;
        }

        public void Update(GameTime gameTime)
        {
            // enemy and enemy
            foreach(MovingEnemy a in gobj.enemies)
            {
                foreach(MovingEnemy b in gobj.enemies)
                {
                    if (!a.Texture.Equals(b.Texture))
                    {
                        Rectangle RectangleA = new Rectangle((int)a.Position.X, (int)a.Position.Y, (int)(a.Texture.Width / 2), a.Texture.Height);
                        Rectangle RectangleB = new Rectangle((int)b.Position.X, (int)b.Position.Y, (int)(a.Texture.Width / 2), a.Texture.Height);
                        if (RectangleA.Intersects(RectangleB))
                        {
                            a.crash = true;
                        }
                    }
                        
                    
                }
            }

            //enemy and block
            foreach(MovingEnemy a in gobj.enemies)
            {
                foreach(ISprite b in gobj.blocks)
                {
                    if (!a.Texture.Equals(b.Texture))
                    {
                        Rectangle RectangleA = new Rectangle((int)a.Position.X, (int)a.Position.Y, (int)(a.Texture.Width / 2), a.Texture.Height);
                        Rectangle RectangleB = new Rectangle((int)b.Position.X, (int)b.Position.Y, (int)(a.Texture.Width), a.Texture.Height);
                        if (RectangleA.Intersects(RectangleB))
                        {
                            a.crash = true;
                        }
                    }
                        
                    
                }
            }

            //Mario and block
            foreach (Mario a in gobj.players)
            {
                foreach (ISprite b in gobj.blocks)
                {
                    if (!a.Texture.Equals(b.Texture))
                    {
                        Rectangle RectangleA = new Rectangle((int)a.Position.X, (int)a.Position.Y, (int)(a.Texture.Width )/3, a.Texture.Height);
                        Rectangle RectangleB = new Rectangle((int)b.Position.X, (int)b.Position.Y, (int)(a.Texture.Width), a.Texture.Height);
                        Rectangle = RectangleA;
                        if (RectangleA.Intersects(RectangleB))
                        {
                            a.crash = true;
                            
                        }
                    }


                }
            }
            //mario and enemy
            foreach (Mario a in gobj.players)
            {
                foreach (ISprite b in gobj.enemies)
                {
                    if (!a.Texture.Equals(b.Texture))
                    {
                        Rectangle RectangleA = new Rectangle((int)a.Position.X, (int)a.Position.Y, (int)(a.Texture.Width), a.Texture.Height);
                        Rectangle RectangleB = new Rectangle((int)b.Position.X, (int)b.Position.Y, (int)(a.Texture.Width / 2), a.Texture.Height);
                        if (RectangleA.Intersects(RectangleB))
                        {
                            a.crash = true;
                            a.TakeDamage();
                        }
                    }


                }
            }
        }


        public bool TouchLeft(ISprite collide)
        {
            bool intersect = false;
            Rectangle Rectangle1 = new Rectangle((int)collide.Position.X, (int)collide.Position.Y, collide.Texture.Width, collide.Texture.Height);
            if (Rectangle.Intersects(Rectangle1))
            {
                intersect = true;
            }
            return intersect;
        }

        public bool TouchRight(ISprite collide)
        {
            bool intersect = false;
            Rectangle Rectangle1 = new Rectangle((int)collide.Position.X, (int)collide.Position.Y, collide.Texture.Width, collide.Texture.Height);
            if (Rectangle.Intersects(Rectangle1))
            {
                intersect = true;
            }
            return intersect;
        }

        public bool TouchTop(ISprite collide)
        {
            Rectangle Rectangle1 = new Rectangle((int)collide.Position.X, (int)collide.Position.Y, collide.Texture.Width, collide.Texture.Height);
            return this.Rectangle.Bottom + this.Velocity.Y > Rectangle1.Top &&
                this.Rectangle.Top < Rectangle1.Top &&
                this.Rectangle.Right > Rectangle1.Left &&
                this.Rectangle.Left < Rectangle1.Right;
        }

        public bool TouchBottom(ISprite collide)
        {
            Rectangle Rectangle1 = new Rectangle((int)collide.Position.X, (int)collide.Position.Y, collide.Texture.Width, collide.Texture.Height);
            return this.Rectangle.Top + this.Velocity.Y < Rectangle1.Bottom &&
                this.Rectangle.Bottom > Rectangle1.Bottom &&
                this.Rectangle.Right > Rectangle1.Left &&
                this.Rectangle.Left < Rectangle1.Right;
        }
    }
}

