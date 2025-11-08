using Microsoft.Xna.Framework;
using AwesomeRPG.Sprites;
using static AwesomeRPG.Util;
using System;

namespace AwesomeRPG.Characters;

public class CharacterEnemyArmos : CharacterEnemyBase
{
    public CharacterEnemyArmos(Vector2 position, Cardinal direction) : base(position, direction)
    {
        
    }

    public override void ChangeDirectionalSprite(Cardinal direction)
    {
        _sprite = direction switch
        {
            Cardinal.up => CharacterSpriteFactory.Instance.ArmosSpriteUp(),
            Cardinal.down => CharacterSpriteFactory.Instance.ArmosSpriteDown(),
            _=> CharacterSpriteFactory.Instance.ArmosSpriteDown()
        };
    }
}