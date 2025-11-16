using AwesomeRPG.Collision;
using AwesomeRPG.Characters;
using Microsoft.Xna.Framework;
using System;

namespace AwesomeRPG.Commands;

/// <summary>
/// Makes one enemy turn around
/// </summary>
public class EnemyEnemyCollideCommand : ICollisionCommand
{
    public void Execute(CollisionInfo collision)
    {
        Console.WriteLine("Enemy collided with enemy! Yay!");
        //CharacterEnemyBase enemy = (CharacterEnemyBase)collision.GetCollisionObjectOfType(CollisionObjectType.Enemy);
        CharacterEnemyBase enemy = PickBestChar(collision.GetBothCollisionObjects(), collision.Direction.ToCard());
        float pushPixels = enemy.MoveSpeed * 2 / Util.ApproxFramesPerSecond;
        //Vector2 bumpUnitDirection = Util.CardinalToUnitVector(collision.Direction.ToCard().Opposite());
        Vector2 bumpUnitDirection = Util.CardinalToUnitVector(collision.Direction.ToCard());


        enemy.Position += pushPixels * bumpUnitDirection;
        enemy.ForceDirection(enemy.Direction.Opposite());
    }
    
    private CharacterEnemyBase PickBestChar((CollisionObject, CollisionObject) objects, Util.Cardinal collisionDirection)
    {
        CharacterEnemyBase char1 = objects.Item1 as CharacterEnemyBase;
        if (char1?.Direction == collisionDirection)
            return char1;
        else
            return objects.Item2 as CharacterEnemyBase;
    }
}
