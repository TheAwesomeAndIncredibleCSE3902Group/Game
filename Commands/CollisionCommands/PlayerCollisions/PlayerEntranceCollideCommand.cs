using AwesomeRPG.Collision;
using AwesomeRPG.Map;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Commands;

public class PlayerEntranceCollideCommand : ICollisionCommand
{
    RoomMap oldRoom;
    RoomMap newRoom;
    RoomAtlas roomAtlas;
    Cardinal direction;
    public void Execute(CollisionInfo collision)
    {
        Entrance entrance = (Entrance)collision.GetCollisionObjectOfType(CollisionObjectType.Entrance);
        CollisionObject player = collision.GetCollisionObjectOfType(CollisionObjectType.Player);
        direction = collision.Direction.ToCard();
        
        entrance.changeRoom(player, direction);
    }
}
