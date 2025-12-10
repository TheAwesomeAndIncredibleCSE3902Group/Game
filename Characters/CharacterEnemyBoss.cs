using Microsoft.Xna.Framework;
using AwesomeRPG.Sprites;
using static AwesomeRPG.Util;
using System;

namespace AwesomeRPG.Characters;

public class CharacterEnemyBoss : CharacterEnemyBase
{
    public override CType Type { get => CType.boss; }
    public CharacterEnemyBoss(Vector2 position, Cardinal direction) : base(position, direction)
    {
        Name = "Boss";
    }

    public override void ChangeDirectionalSprite(Cardinal direction)
    {
        _sprite = direction switch
        {
            _=> EnemySpriteFactory.Instance.BossSprite()
        };
    }
}