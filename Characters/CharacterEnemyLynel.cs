using Microsoft.Xna.Framework;
using AwesomeRPG.Sprites;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Characters;

public class CharacterEnemyLynel : CharacterEnemyBase
{
    public CharacterEnemyLynel(Vector2 position, Cardinal direction) : base(position, direction)
    {
        ChangeDirection(direction);
    }

    public override void ChangeDirection(Cardinal direction)
    {
        _sprite = direction switch
        {
            Cardinal.up => CharacterSpriteFactory.Instance.LynelSpriteUp(),
            Cardinal.down => CharacterSpriteFactory.Instance.LynelSpriteDown(),
            Cardinal.left => CharacterSpriteFactory.Instance.LynelSpriteLeft(),
            Cardinal.right => CharacterSpriteFactory.Instance.LynelSpriteRight()
        };
    }
}