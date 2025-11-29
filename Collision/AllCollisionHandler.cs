using System.Collections.Generic;
using AwesomeRPG.Commands;
using AwesomeRPG.Map;

namespace AwesomeRPG.Collision
{ 
    public class AllCollisionHandler
    {
        public static AllCollisionHandler Instance { get; private set; } = new AllCollisionHandler();
        private Dictionary<CollisionPair, ICollisionCommand> collisionResponses;

        /// <summary>
        /// Calls a collision response based on the info of the collision given
        /// </summary>
        /// <param name="collision"></param>
        private void HandleCollisionResponse(CollisionInfo collision)
        {
            
            if (collision.Direction == CollisionDirection.None) return;

            CollisionPair objectTypes = collision.GetObjectTypesOfCollision();

            if (!collisionResponses.ContainsKey(objectTypes)) return;
               
          
            collisionResponses[objectTypes].Execute(collision);
        }

        private AllCollisionHandler()
        {
            collisionResponses = new Dictionary<CollisionPair, ICollisionCommand>();
            InitializeDict();
        }

        public void HandleCollisions(List<CollisionObject> movingCollisionObjects, List<CollisionObject> nonMovingCollisionObjects)
        {
            CollisionObject movingObject;
            CollisionObject nonMovingObject;

            for (int i = 0; i < movingCollisionObjects.Count; i++)
            {
                for (int k = 0; k < nonMovingCollisionObjects.Count; k++)
                {
                    //Checking to make sure the ith element wasn't removed from MovingCollisionObjects
                    if (RoomAtlas.Instance.CurrentRoom.MovingCollisionObjects.Count > i)
                        movingObject = RoomAtlas.Instance.CurrentRoom.MovingCollisionObjects[i];
                    else
                        return;

                    //Checking to make sure the kth element wasn't removed from nonMovingCollisionObjects
                    if (nonMovingCollisionObjects.Count > k)
                        nonMovingObject = nonMovingCollisionObjects[k];
                    else
                        continue;

                    CollisionInfo collision = movingObject.DetectCollision(nonMovingObject);
                    HandleCollisionResponse(collision);
                }

                for (int j = i + 1; j < RoomAtlas.Instance.CurrentRoom.MovingCollisionObjects.Count; j++)
                {
                    CollisionInfo collision = RoomAtlas.Instance.CurrentRoom.MovingCollisionObjects[i].DetectCollision(RoomAtlas.Instance.CurrentRoom.MovingCollisionObjects[j]);
                    HandleCollisionResponse(collision);
                }
            }
        }

        #region Dictionary Declarations
        private void InitializeDict()
        {
            InitializePlayerCollisions();
            InitializeEnemyCollisions();
            InitializeProjectileCollisions();
        }

        
        private void InitializeProjectileCollisions()
        {
            InitializePlayerProjectileCollisions();
            InitializeEnemyProjectileCollisions();
        }

        private void InitializePlayerProjectileCollisions()
        {
            collisionResponses[new CollisionPair(CollisionObjectType.PlayerProjectile, CollisionObjectType.Enemy)] = new PlayerProjectileEnemyCollideCommand();
            collisionResponses[new CollisionPair(CollisionObjectType.PlayerProjectile, CollisionObjectType.Wall)] = new PlayerProjectileWallCollideCommand();
            //This command will run for all player projectiles, but it only does something if it is a Boomerang
            collisionResponses[new CollisionPair(CollisionObjectType.PlayerProjectile, CollisionObjectType.Player)] = new BoomerangPlayerCollideCommand();
        }

        private void InitializeEnemyProjectileCollisions()
        {
            collisionResponses[new CollisionPair(CollisionObjectType.EnemyProjectile, CollisionObjectType.Wall)] = new EnemyProjectileWallCollideCommand();
            collisionResponses[new CollisionPair(CollisionObjectType.EnemyProjectile, CollisionObjectType.Player)] = new EnemyProjectilePlayerCollideCommand();
        }
        private void InitializePlayerCollisions()
        {
            collisionResponses[new CollisionPair(CollisionObjectType.Player, CollisionObjectType.Wall)] = new PlayerWallCollideCommand();
            collisionResponses[new CollisionPair(CollisionObjectType.Player, CollisionObjectType.Lava)] = new PlayerWallCollideCommand();
            collisionResponses[new CollisionPair(CollisionObjectType.Player, CollisionObjectType.Pickup)] = new PlayerPickupCollideCommand();
            collisionResponses[new CollisionPair(CollisionObjectType.Player, CollisionObjectType.Enemy)] = new PlayerEnemyCollideCommand();
            collisionResponses[new CollisionPair(CollisionObjectType.Player, CollisionObjectType.Entrance)] = new PlayerEntranceCollideCommand();
        }

        private void InitializeEnemyCollisions()
        {
            collisionResponses[new CollisionPair(CollisionObjectType.Enemy, CollisionObjectType.Wall)] = new EnemyWallCollideCommand();
            collisionResponses[new CollisionPair(CollisionObjectType.Enemy, CollisionObjectType.Lava)] = new EnemyWallCollideCommand();
            //Enemies treat Entrances as Walls
            collisionResponses[new CollisionPair(CollisionObjectType.Enemy, CollisionObjectType.Entrance)] = new EnemyWallCollideCommand();
            collisionResponses[new CollisionPair(CollisionObjectType.Enemy, CollisionObjectType.Enemy)] = new EnemyEnemyCollideCommand();
        }
        #endregion
    }
}
