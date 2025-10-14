using Microsoft.Xna.Framework;
using AwesomeRPG.Sprites;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Characters;

public class CharacterEnemyLynel : CharacterEnemyBase
{
    public CharacterEnemyLynel(Vector2 position, Cardinal direction) : base(position, direction)
    {
        _sprite = CharacterSpriteFactory.Instance.LynelSprite();
    }

    public override void ChangeDirection(Cardinal direction)
    {
        throw new System.NotImplementedException();
    }
}