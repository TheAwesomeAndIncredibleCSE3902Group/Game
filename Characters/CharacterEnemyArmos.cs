using Microsoft.Xna.Framework;
using AwesomeRPG.Sprites;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Characters;

public class CharacterEnemyArmos : CharacterEnemyBase
{
    public CharacterEnemyArmos(Vector2 position, Cardinal direction) : base(position, direction)
    {
        _sprite = CharacterSpriteFactory.Instance.ArmosSprite();
    }
}