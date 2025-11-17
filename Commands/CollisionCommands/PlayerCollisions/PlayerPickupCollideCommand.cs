using System;
using AwesomeRPG.Collision;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.Commands;

public class PlayerPickupCollideCommand : ICollisionCommand
{
    public void Execute(CollisionInfo collision)
    {
        Pickup pickup = (Pickup)collision.GetCollisionObjectOfType(CollisionObjectType.Pickup);
        PlayerOverworld player = (PlayerOverworld)collision.GetCollisionObjectOfType(CollisionObjectType.Player);
        pickup.OnPlayerTouched(player);
    }
}
