using AwesomeRPG.Collision;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace AwesomeRPG.Commands;

public class CollidePlayerEnemyCommand : ICollisionCommand
{
    public void Execute(CollisionInfo collision)
    {
        Debug.WriteLine("DEBUG CollidePlayerEnemyCommand: Enter the battle state");
        Game1.TransitionToBattleState();
    }
}
