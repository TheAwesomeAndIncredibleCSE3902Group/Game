using Microsoft.Xna.Framework;
using AwesomeRPG.Sprites;
using static AwesomeRPG.Util;
using System;

namespace AwesomeRPG.Characters;

public class CharacterEnemyArmos : CharacterEnemyBase
{
    public override CType Type { get => CType.armos; }
    public CharacterEnemyArmos(Vector2 position, Cardinal direction) : base(position, direction)
    {
        Name = "Armos";
    }

    public override void ChangeDirectionalSprite(Cardinal direction)
    {
        _sprite = direction switch
        {
            Cardinal.up => EnemySpriteFactory.Instance.ArmosSpriteUp(),
            Cardinal.down => EnemySpriteFactory.Instance.ArmosSpriteDown(),
            _=> EnemySpriteFactory.Instance.ArmosSpriteDown()
        };
    }
}