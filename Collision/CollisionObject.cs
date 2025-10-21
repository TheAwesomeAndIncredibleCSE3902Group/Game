using Microsoft.Xna.Framework;

namespace AwesomeRPG.Collision
{
    public enum CollisionObjectType { Player, Enemy, Wall }
    public abstract class CollisionObject
    {
        public Vector2 Position { get; set; }

        public CollisionRect Collider { get; protected set; }

        public CollisionObjectType ObjectType { get; protected set; }

        /// <summary>
        /// Creates a collisionInfo tracks in which direction the objects collided
        /// </summary>
        /// <param name="otherCollisionObject"></param>
        /// <returns>CollisionInfo which carries the objects collided along with the direction of collision</returns>
        public CollisionInfo DetectCollision(CollisionObject otherCollisionObject)
        {
            Rectangle intersectRect = Rectangle.Intersect(Collider.GetCollisionRect(), otherCollisionObject.Collider.GetCollisionRect());
            CollisionDirection direction = GetCollisionDirection(otherCollisionObject, intersectRect);
            return new CollisionInfo(this, otherCollisionObject, direction);
        }

        
        private CollisionDirection GetCollisionDirection(CollisionObject otherCollisionObject, Rectangle intersectRect)
        { 
            if(intersectRect.Width > 0)
            {
                return Position.X < otherCollisionObject.Position.X ? CollisionDirection.Left : CollisionDirection.Right; 
            }
            else if(intersectRect.Height > 0)
            {
                return Position.Y < otherCollisionObject.Position.Y ? CollisionDirection.Top : CollisionDirection.Bottom;
            }
            else
            {
                return CollisionDirection.None;
            }
        }
        

    }
}
