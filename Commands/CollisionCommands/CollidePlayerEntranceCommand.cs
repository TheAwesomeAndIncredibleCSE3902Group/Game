using AwesomeRPG.Collision;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace AwesomeRPG.Commands;

public class CollidePlayerEntranceCommand : ICollisionCommand
{
    public void Execute(CollisionInfo collision)
    {
        //need to figure out if I can room swap with only collisioninfo as the param
    }
}
