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
        private Dictionary<ImmutableHashSet<CollisionObjectType>, Action<CollisionInfo>> collisionResponses;

        /// <summary>
        /// Calls a collision response based on the info of the collision given
        /// </summary>
        /// <param name="collision"></param>
        public void HandleCollision(CollisionInfo collision)
        {
            if (collision.Direction != CollisionDirection.None) return;

            ImmutableHashSet<CollisionObjectType> objectTypes = collision.GetObjectTypesOfCollision();

            if (!collisionResponses.ContainsKey(objectTypes))
            {
                PrintErrorMessage(objectTypes);
            }
            collisionResponses[objectTypes].Invoke(collision);
        }

        public AllCollisionHandler()
        {
            collisionResponses = new();
            InitializeDict();
        }

        private void InitializeDict()
        {
            ImmutableHashSet<CollisionObjectType> playerWallCollision = [CollisionObjectType.Player, CollisionObjectType.Wall];
            collisionResponses[playerWallCollision] = PlayerWallCollision;
        }

        private void PlayerWallCollision(CollisionInfo collision)
        {
            new CommandCollidePlayerWall(collision);
        }

        private void PrintErrorMessage(ImmutableHashSet<CollisionObjectType> objectTypes)
        {
            Debug.WriteLine("Unmarked collision between");
            foreach (CollisionObjectType type in objectTypes)
            {
                Debug.Write(type + " ");
            }
            return;
        }
    }
}
