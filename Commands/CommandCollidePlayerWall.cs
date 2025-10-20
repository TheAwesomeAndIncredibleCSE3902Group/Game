using System;
using AwesomeRPG.Collision;

namespace AwesomeRPG.Commands;

public class CommandCollidePlayerWall : ICommand
{
    private readonly CollisionInfo collision;

    public CommandCollidePlayerWall(CollisionInfo collision)
    {
        this.collision = collision;
    }

    public void Execute()
    {
        Player.Instance.CollisionHandler.CollideWall(collision);
    }
}
