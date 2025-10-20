using Microsoft.Xna.Framework;
using AwesomeRPG.Sprites;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Characters;

public class CharacterEnemyMoblin : CharacterEnemyBase
{
    public CharacterEnemyMoblin(Vector2 position, Cardinal direction) : base(position, direction)
    {
        _sprite = CharacterSpriteFactory.Instance.MoblinSprite();
    }

    public override void ChangeDirection(Cardinal direction)
    {
        throw new System.NotImplementedException();
    }
}