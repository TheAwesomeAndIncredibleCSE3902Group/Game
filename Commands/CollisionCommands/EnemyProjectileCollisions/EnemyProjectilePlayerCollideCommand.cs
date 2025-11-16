using AwesomeRPG.Characters;
using AwesomeRPG.Collision;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace AwesomeRPG.Commands;

/// <summary>
/// Enemy projectile collides with the player
/// </summary>
public class EnemyProjectilePlayerCollideCommand : ICollisionCommand
{
    public void Execute(CollisionInfo collision)
    {
        Projectile projectile = (Projectile)collision.GetCollisionObjectOfType(CollisionObjectType.EnemyProjectile);
        if (projectile is not MoblinFire)
        {
            Debug.Write("Collision for this projectile not set up!");
            Debug.Write("Come set me up in EnemyProjectilePlayerCollideCommand");
            return;
        }

        ICharacter enemy = (projectile as MoblinFire).Firee;

        projectile.Destroy();
        Game1.TransitionToBattleState([enemy as CharacterEnemyBase]);
    }
}
