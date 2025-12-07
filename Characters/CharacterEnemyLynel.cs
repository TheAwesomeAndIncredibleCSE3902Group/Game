using Microsoft.Xna.Framework;
using AwesomeRPG.Sprites;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Characters;

public class CharacterEnemyLynel : CharacterEnemyBase
{
    public override CType Type { get => CType.lynel; }
    public CharacterEnemyLynel(Vector2 position, Cardinal direction) : base(position, direction)
    {
        Name = "Lynel";
    }

    public override void ChangeDirectionalSprite(Cardinal direction)
    {
        _sprite = direction switch
        {
            Cardinal.up => EnemySpriteFactory.Instance.LynelSpriteUp(),
            Cardinal.down => EnemySpriteFactory.Instance.LynelSpriteDown(),
            Cardinal.left => EnemySpriteFactory.Instance.LynelSpriteLeft(),
            Cardinal.right => EnemySpriteFactory.Instance.LynelSpriteRight(),
            _ => EnemySpriteFactory.Instance.LynelSpriteDown(),
        };
    }
}