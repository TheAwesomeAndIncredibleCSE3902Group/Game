using Microsoft.Xna.Framework;
using AwesomeRPG.Collision;
using Microsoft.Xna.Framework.Graphics;
using AwesomeRPG.Sprites;

namespace AwesomeRPG.Map
{
    public class Door : CollisionObject
    {
        public int ID;
        private AnimatableSprite _sprite;
        public Door(Vector2 startPos, int width, int height, int id)
        {
            Position = startPos;
            Collider = new CollisionRect(this, width, height);
            ObjectType = CollisionObjectType.Wall;
            ID = id;
            _sprite = EnemySpriteFactory.Instance.LockSprite();
        }

        public void Draw(GameTime gameTime)
        {
            _sprite.Draw(gameTime, Position);
        }

        
    }
}
