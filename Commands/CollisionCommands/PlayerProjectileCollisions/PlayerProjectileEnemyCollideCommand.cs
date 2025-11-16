using AwesomeRPG.Characters;
using AwesomeRPG.Collision;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace AwesomeRPG.Commands;

public class PlayerProjectileEnemyCollideCommand : ICollisionCommand
{
    public void Execute(CollisionInfo collision)
    {
        Projectile projectile = (Projectile)collision.GetCollisionObjectOfType(CollisionObjectType.PlayerProjectile);
        CharacterEnemyBase enemy = (CharacterEnemyBase)collision.GetCollisionObjectOfType(CollisionObjectType.Enemy);

        Game1.TransitionToBattleState([enemy]);
        Debug.WriteLine("DEBUG ProjectileEnemyCollideCommand: Enter the battle state");
        projectile.Destroy();

        Game1.TransitionToBattleState([enemy]);
    }
}
