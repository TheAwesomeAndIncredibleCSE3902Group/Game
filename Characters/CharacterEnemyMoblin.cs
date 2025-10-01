using Microsoft.Xna.Framework;
using Sprint0.Sprites;
using static Sprint0.Util;

namespace Sprint0.Characters;

public class CharacterEnemyMoblin : CharacterEnemyBase
{
    public CharacterEnemyMoblin(Vector2 position, Cardinal direction) : base(position, direction)
    {
        _sprite = CharacterSpriteFactory.Instance.MoblinSprite();
    }
}