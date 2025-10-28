using AwesomeRPG.Collision;

namespace AwesomeRPG.Commands;

public class NoCollisionCollideCommand : ICollisionCommand
{
    public void Execute(CollisionInfo collision)
    {
        //Nothin' here. This is just a way to explicitly designate two collider types as non-interacting
    }
}
