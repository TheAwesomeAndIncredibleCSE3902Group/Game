using Microsoft.Xna.Framework;
using Sprint0.Sprites;
using static Sprint0.Util;

namespace Sprint0.Characters;

public class CharacterEnemyLynel : CharacterEnemyBase
{
    public CharacterEnemyLynel(Vector2 position, Cardinal direction) : base(position, direction)
    {
        _sprite = CharacterSpriteFactory.Instance.LynelSprite();
    }
}