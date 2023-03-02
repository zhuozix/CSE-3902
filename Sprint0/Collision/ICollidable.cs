using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Collision
{
    public interface ICollidable
    {
        public Rectangle Hitbox { get; set; }
        public Point HitboxOffset { get; set; }
        public Vector2 Position { get; set; }
        public bool CanCollide { get; set; }
        public bool IsAnchored { get; set; }

        public Texture2D HitboxTexture { get; set; }
        public double LastCollisionTime { get; set; }

        public void OnCollision(ICollidable hit);
        public void DrawHitbox(SpriteBatch spriteBatch, Color color);
        public void UpdateHitbox();
        public bool ShouldCollideWith(ICollidable other);

    }
}
