using AwesomeRPG.Collision;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace AwesomeRPG.Commands;

public class ProjectileEnemyCollideCommand : ICollisionCommand
{
    public void Execute(CollisionInfo collision)
    {
        Projectile projectile = (Projectile)collision.GetCollisionObjectOfType(CollisionObjectType.PlayerProjectile);
        Debug.WriteLine("DEBUG ProjectileEnemyCollideCommand: Enter the battle state");
        Game1.TransitionToBattleState();
        projectile.Destroy();
    }
}
