using Microsoft.Xna.Framework;
using Sprint0.Sprites;
using static Sprint0.Util;

namespace Sprint0.Characters;

public class CharacterEnemyArmos : CharacterEnemyBase
{
    public CharacterEnemyArmos(Vector2 position, Cardinal direction) : base(position, direction)
    {
        _sprite = CharacterSpriteFactory.Instance.ArmosSprite();
    }
}