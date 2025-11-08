using Microsoft.Xna.Framework;
using AwesomeRPG.Sprites;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Characters;

public class CharacterEnemyLynel : CharacterEnemyBase
{
    public CharacterEnemyLynel(Vector2 position, Cardinal direction) : base(position, direction)
    {
        
    }

    public override void ChangeDirectionalSprite(Cardinal direction)
    {
        _sprite = direction switch
        {
            Cardinal.up => CharacterSpriteFactory.Instance.LynelSpriteUp(),
            Cardinal.down => CharacterSpriteFactory.Instance.LynelSpriteDown(),
            Cardinal.left => CharacterSpriteFactory.Instance.LynelSpriteLeft(),
            Cardinal.right => CharacterSpriteFactory.Instance.LynelSpriteRight(),
            _ => CharacterSpriteFactory.Instance.LynelSpriteDown(),
        };
    }
}