using AwesomeRPG.Characters;
using AwesomeRPG.Collision;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace AwesomeRPG.Commands;

public class EnemyProjectilePlayerCollideCommand : ICollisionCommand
{
    public void Execute(CollisionInfo collision)
    {
        Projectile projectile = (Projectile)collision.GetCollisionObjectOfType(CollisionObjectType.EnemyProjectile);
        CharacterEnemyBase enemy = (CharacterEnemyBase)collision.GetCollisionObjectOfType(CollisionObjectType.Enemy);

        projectile.Destroy();
        Game1.TransitionToBattleState([enemy]);
    }
}
