using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using AwesomeRPG.Commands;

namespace AwesomeRPG.Collision
{ 
    public class AllCollisionHandler
    {
        private Dictionary<CollisionPair, Action<CollisionInfo>> collisionResponses;

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
            collisionResponses[objectTypes].Invoke(collision);
        }

        public AllCollisionHandler()
        {
            collisionResponses = new Dictionary<CollisionPair, Action<CollisionInfo>>();
            InitializeDict();
          
        }

        private void InitializeDict()
        {
            collisionResponses[new CollisionPair(CollisionObjectType.Player,CollisionObjectType.Wall)] = PlayerWallCollision;
        }

        private void PlayerWallCollision(CollisionInfo collision)
        {
            new CommandCollidePlayerWall(collision).Execute();
            
        }

    }
}
