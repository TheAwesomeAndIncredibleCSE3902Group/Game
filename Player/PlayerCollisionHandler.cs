using System;
using System.Diagnostics;
using System.Numerics;
using AwesomeRPG.Collision;


namespace AwesomeRPG;

/// <summary>
/// This will be held by the Player. Responsible for reactions the player has after colliding with all objects.
/// </summary>
public class PlayerCollisionHandler
{
    public void CollideWall(CollisionInfo collision)
    {
        float pushPixels = 2;
        Vector2 bumpUnitDirection = Util.CardinalToUnitVector(collision.Direction.ToCard().Opposite());

        Player.Instance.Position += pushPixels * bumpUnitDirection;
    }

    public void CollideEnemy(CollisionInfo collision)
    {
        //TODO: Goto turn-based? I actually prefer only going to turn-based when the player hits an enemy
        throw new NotImplementedException();
    }

    public void CollideItem(CollisionInfo collision)
    {
        //TODO: pick up item
        throw new NotImplementedException();
    }
}
