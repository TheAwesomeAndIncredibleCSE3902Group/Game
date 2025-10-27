using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using AwesomeRPG.Commands;

namespace AwesomeRPG.Collision
{ 
    public class AllCollisionHandler
    {
        private Dictionary<CollisionPair, ICollisionCommand> collisionResponses;

        /// <summary>
        /// Calls a collision response based on the info of the collision given
        /// </summary>
        /// <param name="collision"></param>
        public void HandleCollision(CollisionInfo collision)
        {
            
            if (collision.Direction == CollisionDirection.None) return;

            CollisionPair objectTypes = collision.GetObjectTypesOfCollision();

            if (!collisionResponses.ContainsKey(objectTypes))
            {
                Debug.WriteLine($"Unmarked collision between {objectTypes}");
                return;
            }
            collisionResponses[objectTypes].Execute(collision);
        }

        public AllCollisionHandler()
        {
            collisionResponses = new Dictionary<CollisionPair, ICollisionCommand>();
            InitializeDict();
          
        }

        private void InitializeDict()
        {
            InitializePlayerCollisions();
        }

        private void InitializePlayerCollisions()
        {
            collisionResponses[new CollisionPair(CollisionObjectType.Player, CollisionObjectType.Wall)] = new CollidePlayerWallCommand();
            collisionResponses[new CollisionPair(CollisionObjectType.Player, CollisionObjectType.Pickup)] = new CollidePlayerPickupCommand();
            collisionResponses[new CollisionPair(CollisionObjectType.Player, CollisionObjectType.Enemy)] = new CollidePlayerEnemyCommand();
            collisionResponses[new CollisionPair(CollisionObjectType.Player, CollisionObjectType.Entrance)] = new PlayerEntranceCollideCommand();
        }
        
    }
}
