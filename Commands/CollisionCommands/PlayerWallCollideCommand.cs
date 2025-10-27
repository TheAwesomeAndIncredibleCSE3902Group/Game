using AwesomeRPG.Collision;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.Commands;

public class PlayerWallCollideCommand : ICollisionCommand
{
    public void Execute(CollisionInfo collision)
    {
        CollisionObject player = collision.GetCollisionObjectOfType(CollisionObjectType.Player);
        float pushPixels = 2;
        Vector2 bumpUnitDirection = Util.CardinalToUnitVector(collision.Direction.ToCard().Opposite());

        player.Position += pushPixels * bumpUnitDirection;
    }
}
