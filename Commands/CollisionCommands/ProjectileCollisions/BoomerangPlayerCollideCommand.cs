using System;
using AwesomeRPG.Collision;

namespace AwesomeRPG.Commands;

public class BoomerangPlayerCollideCommand : ICollisionCommand
{
    public void Execute(CollisionInfo collision)
    {
        Projectile projectile = (Projectile)collision.GetCollisionObjectOfType(CollisionObjectType.PlayerProjectile);

        //Destroys itself if moving backward
        if ((projectile as PlayerBoomerang)?.IsMovingForward == false)
            projectile.Destroy();
    }
}
