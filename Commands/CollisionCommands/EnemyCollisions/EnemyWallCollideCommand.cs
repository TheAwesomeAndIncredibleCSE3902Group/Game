using AwesomeRPG.Collision;
using AwesomeRPG.Characters;
using Microsoft.Xna.Framework;
using System;

namespace AwesomeRPG.Commands;

/// <summary>
/// Pushes the enemy away from the wall and sends it the opposite way it was going
/// </summary>
public class EnemyWallCollideCommand : ICollisionCommand
{
    public void Execute(CollisionInfo collision)
    {
        Console.WriteLine("Enemy collided with wall! Yay!");
        CharacterEnemyBase enemy = (CharacterEnemyBase)collision.GetCollisionObjectOfType(CollisionObjectType.Enemy);
        float pushPixels = enemy.MoveSpeed/Util.ApproxFramesPerSecond;
        Vector2 bumpUnitDirection = Util.CardinalToUnitVector(collision.Direction.ToCard().Opposite());

        enemy.Position += pushPixels * bumpUnitDirection;
        enemy.ForceDirection(enemy.Direction.Opposite());
    }
}
