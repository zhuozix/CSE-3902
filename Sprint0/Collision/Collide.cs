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

        public List<bool> collideEnemy { get; set; }

        public bool fall { get; set; }
        public Collide(GameObjectManager gameObjectManager)
        {
            this.gobj = gameObjectManager;
        }

        public void Update(GameTime gameTime)
        {
            /* List<ISprite> enemy = gobj.enemies;
             List<ISprite> block = gobj.blocks;
             for (int i = 0; i < enemy.Count; i++)
             {
                 Rectangle = new Rectangle((int)enemy[i].Position.X, (int)enemy[i].Position.Y, enemy[i].Texture.Width, enemy[i].Texture.Height);
                 for (int j = 0; j < block.Count; j++)
                 {
                     if (TouchLeft(block[j]) || TouchRight(block[j]))
                     {
                         collideEnemy[i] = true;
                     }

                 }         
                 if (enemy[i].Position.X >= 780)
                 {
                     collideA= true;
                 }
                 else if (enemy[i].Position.X <= 0)
                 {
                     collideA= false;
                 }
             }*/

            detectEnemyCollison();
            //detectTwoEnemyCollison();
        }

        public void detectEnemyCollison()
        {
            List<ISprite> enemy = gobj.enemies;
            List<ISprite> block = gobj.blocks;
            for (int i = 0; i < enemy.Count; i++)
            {
                Rectangle = new Rectangle((int)enemy[i].Position.X, (int)enemy[i].Position.Y, enemy[i].Texture.Width, enemy[i].Texture.Height);
                for (int j = 0; j < block.Count; j++)
                {
                    Rectangle Rectangle1 = new Rectangle((int)block[j].Position.X, (int)block[j].Position.Y, block[j].Texture.Width, block[j].Texture.Height);
                    if (Rectangle.X >= Rectangle1.X)
                    {
                        enemy[i].collide = true;
                    }

                }
                if (Rectangle.X >= 780)
                {
                    enemy[i].collideA = true;
                }
                else if (Rectangle.X <= 0)
                {
                    enemy[i].collideA = false;
                }
            }
        }
        public void detectTwoEnemyCollison()
        {
            List<ISprite> enemy = gobj.enemies;
            //List<ISprite> block = gobj.blocks;
            for (int i = 0; i < enemy.Count; i++)
            {
                Rectangle = new Rectangle((int)enemy[i].Position.X, (int)enemy[i].Position.Y, enemy[i].Texture.Width, enemy[i].Texture.Height);
                for (int j = 0; j < enemy.Count; j++)
                {
                    if (enemy[i].Position.X == enemy[j].Position.X)
                    {
                        enemy[i].collideA = true;
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

