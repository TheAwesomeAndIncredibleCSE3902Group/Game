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

            if (!collisionResponses.ContainsKey(objectTypes)) return;
               
          
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
            InitializeEnemyCollisions();
            InitializeProjectileCollisions();
        }

        private void InitializeProjectileCollisions()
        {
            collisionResponses[new CollisionPair(CollisionObjectType.PlayerProjectile, CollisionObjectType.Enemy)] = new ProjectileEnemyCollideCommand();
            collisionResponses[new CollisionPair(CollisionObjectType.PlayerProjectile, CollisionObjectType.Wall)] = new ProjectileWallCollideCommand();

            //This command will run for all player projectiles, but it only does something if it is a Boomerang
            collisionResponses[new CollisionPair(CollisionObjectType.PlayerProjectile, CollisionObjectType.Player)] = new BoomerangPlayerCollideCommand();
        }

        private void InitializePlayerCollisions()
        {
            collisionResponses[new CollisionPair(CollisionObjectType.Player, CollisionObjectType.Wall)] = new PlayerWallCollideCommand();
            collisionResponses[new CollisionPair(CollisionObjectType.Player, CollisionObjectType.Pickup)] = new PlayerPickupCollideCommand();
            collisionResponses[new CollisionPair(CollisionObjectType.Player, CollisionObjectType.Enemy)] = new PlayerEnemyCollideCommand();
            collisionResponses[new CollisionPair(CollisionObjectType.Player, CollisionObjectType.Entrance)] = new PlayerEntranceCollideCommand();
        }

        private void InitializeEnemyCollisions()
        {
            collisionResponses[new CollisionPair(CollisionObjectType.Enemy, CollisionObjectType.Wall)] = new EnemyWallCollideCommand();
            //Enemies treat Entrances as Walls
            collisionResponses[new CollisionPair(CollisionObjectType.Enemy, CollisionObjectType.Entrance)] = new EnemyWallCollideCommand();
        }
        
    }
}
