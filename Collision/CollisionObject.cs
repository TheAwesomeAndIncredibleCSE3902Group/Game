using Microsoft.Xna.Framework;

namespace AwesomeRPG.Collision
{
    public abstract class CollisionObject
    {
        public Vector2 Position { get; set; }

        public CollisionRect Collider { get; }

        public CollisionInfo DetectCollision(CollisionObject collisionObject)
        {
            Rectangle intersectRect = Rectangle.Intersect(this.Collider.GetCollisionRect(), collisionObject.Collider.GetCollisionRect());
            CollisionInfo.CollisionDirection direction = CollisionInfo.CollisionDirection.None;
            return new CollisionInfo(this, collisionObject, direction);
        }

        /*
        private CollisionInfo.CollisionDirection GetCollisionDirection(Rectangle intersectRect)
        { 
            if(intersectRect.IsEmpty)
            {
                 return CollisionInfo.CollisionDirection.None;
            }
            else if(intersectRect.Width > 0)
            {
                return 
            }
        }
        */

    }
}
