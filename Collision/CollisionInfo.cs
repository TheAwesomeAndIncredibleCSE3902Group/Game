using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Emit;

namespace AwesomeRPG.Collision
{
    public enum CollisionDirection { Left, Right, Top, Bottom, None }
    public class CollisionInfo
    {
        
        private CollisionObject ReferenceCollisionObject { get; set; }
        private CollisionObject OtherCollisionObject { get; set; }
        public CollisionDirection Direction { get; private set; }

        /// <summary>
        /// Create a collisionInfo which contains the two objects along with the direction they collided based off the reference
        /// </summary>
        /// <param name="collisionObject1"></param>
        /// <param name="collisionObject2"></param>
        /// <param name="direction"></param>
        public CollisionInfo(CollisionObject collisionObject1, CollisionObject collisionObject2, CollisionDirection direction)
        {
            ReferenceCollisionObject = collisionObject1;
            OtherCollisionObject = collisionObject2;
            Direction = direction;
        }
        
        /// <summary>
        /// Returns a hashset of the objectTypes of the collision
        /// </summary>
        /// <returns></returns>
        public CollisionPair GetObjectTypesOfCollision()
        {
            return new CollisionPair(ReferenceCollisionObject.ObjectType, OtherCollisionObject.ObjectType);
        }

        /// <summary>
        /// Returns the CollisionObject in the CollisionInfo of the given type. Returns null if neither object is that type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public CollisionObject GetCollisionObjectOfType(CollisionObjectType type)
        {
            if (ReferenceCollisionObject.ObjectType == type) return ReferenceCollisionObject;
            else if (OtherCollisionObject.ObjectType == type) return OtherCollisionObject;
            else return null;
        }

        /*
        private CollisionDirection ReverseDirection(CollisionDirection direction)
        {
            switch (direction)
            {
                case CollisionDirection.Top:
                    direction = CollisionDirection.Bottom;
                    break;
                case CollisionDirection.Bottom:
                    direction = CollisionDirection.Top;
                    break;
                case CollisionDirection.Left:
                    direction = CollisionDirection.Right;
                    break;
                case CollisionDirection.Right:
                    direction = CollisionDirection.Left;
                    break;
                default:
                    direction = CollisionDirection.None;
                    break;
            }
            return direction;
        }
        */

    }
}
