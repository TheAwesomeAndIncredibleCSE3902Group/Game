using AwesomeRPG.Collision;
using AwesomeRPG.Characters;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace AwesomeRPG.Commands;

/// <summary>
/// Destroys the projectile when it collides with a wall
/// </summary>
public class PlayerProjectileWallCollideCommand : ICollisionCommand
{
    public void Execute(CollisionInfo collision)
    {
        Projectile projectile = (Projectile)collision.GetCollisionObjectOfType(CollisionObjectType.PlayerProjectile);
        projectile.Destroy();
    }
}
