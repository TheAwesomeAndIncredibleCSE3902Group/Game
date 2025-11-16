using AwesomeRPG.Collision;
using AwesomeRPG.Map;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Commands;

public class PlayerEntranceCollideCommand : ICollisionCommand
{
    public void Execute(CollisionInfo collision)
    {
        CollisionObject player = collision.GetCollisionObjectOfType(CollisionObjectType.Player);
        Cardinal direction = collision.Direction.ToCard();
        
        Entrance.changeRoom(player, direction);
    }
}
