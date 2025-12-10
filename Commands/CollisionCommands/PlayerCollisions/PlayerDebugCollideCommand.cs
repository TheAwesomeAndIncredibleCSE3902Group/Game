using AwesomeRPG.Collision;
using AwesomeRPG.Map;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Commands;

public class PlayerDebugCollideCommand : ICollisionCommand
{
    public void Execute(CollisionInfo collision)
    {
        CollisionObject player = collision.GetCollisionObjectOfType(CollisionObjectType.Player);
        
        DebugEntrance.ChangeRoom(player);
    }
}
