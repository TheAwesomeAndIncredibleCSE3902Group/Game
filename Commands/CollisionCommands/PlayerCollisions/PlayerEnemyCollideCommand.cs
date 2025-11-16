using AwesomeRPG.Characters;
using AwesomeRPG.Collision;

namespace AwesomeRPG.Commands;

public class PlayerEnemyCollideCommand : ICollisionCommand
{
    public void Execute(CollisionInfo collision)
    {
        // Debug.WriteLine("DEBUG CollidePlayerEnemyCommand: Enter the battle state");
        CharacterEnemyBase enemy = (CharacterEnemyBase)collision.GetCollisionObjectOfType(CollisionObjectType.Enemy);

        Game1.TransitionToBattleState([enemy]);
    }
}
