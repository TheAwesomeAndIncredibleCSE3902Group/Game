using AwesomeRPG.Collision;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.Commands;

public class PlayerWallCollideCommand : ICollisionCommand
{
    public void Execute(CollisionInfo collision)
    {
        CollisionObject player = collision.GetCollisionObjectOfType(CollisionObjectType.Player);

        //Player must be pushed by exactly as many pixels as it walked, otherwise there will be jitter
        const float backup = 240;
        float pushPixels = ((player as Player)?.MovementSpeed ?? backup) / Util.ApproxFramesPerSecond;

        Vector2 bumpUnitDirection = Util.CardinalToUnitVector(collision.Direction.ToCard().Opposite());
        player.Position += pushPixels * bumpUnitDirection;
    }
}
