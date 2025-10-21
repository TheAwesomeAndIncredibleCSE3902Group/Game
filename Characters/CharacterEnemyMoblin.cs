using Microsoft.Xna.Framework;
using AwesomeRPG.Sprites;
using static AwesomeRPG.Util;
using System;

namespace AwesomeRPG.Characters;

public class CharacterEnemyMoblin : CharacterEnemyBase
{
    public CharacterEnemyMoblin(Vector2 position, Cardinal direction) : base(position, direction)
    {
        ChangeDirection(direction);
    }

    public override void ChangeDirection(Cardinal direction)
    {
        _sprite = direction switch
        {
            Cardinal.up => CharacterSpriteFactory.Instance.MoblinSpriteUp(),
            Cardinal.down => CharacterSpriteFactory.Instance.MoblinSpriteDown(),
            Cardinal.left => CharacterSpriteFactory.Instance.MoblinSpriteLeft(),
            Cardinal.right => CharacterSpriteFactory.Instance.MoblinSpriteRight()
        };
    }
}