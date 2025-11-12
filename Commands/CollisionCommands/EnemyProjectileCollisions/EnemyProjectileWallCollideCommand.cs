using AwesomeRPG.Collision;
using AwesomeRPG.Characters;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace AwesomeRPG.Commands;

/// <summary>
/// Destroys the projectile when it collides with a wall
/// </summary>
public class EnemyProjectileWallCollideCommand : ICollisionCommand
{
    public void Execute(CollisionInfo collision)
    {
        Projectile projectile = (Projectile)collision.GetCollisionObjectOfType(CollisionObjectType.EnemyProjectile);
        projectile.Destroy();
    }
}
