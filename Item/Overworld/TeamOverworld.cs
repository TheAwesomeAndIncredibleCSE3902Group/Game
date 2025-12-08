using System;
using System.Diagnostics;
using AwesomeRPG.Collision;
using AwesomeRPG.Commands;
using AwesomeRPG.Map;
using AwesomeRPG.Sprites;
using AwesomeRPG.Stats;

namespace AwesomeRPG;

public enum CharType {Zelda, Merchant, OldLady, Link};
public class TeamOverworld : Pickup
{
    CharType charType;
    public TeamOverworld(RoomMap levelMap, string charTypeName) : base(levelMap)
    {
        CharType charType = StringToCharType(charTypeName);
        SetCharacterType(charType);
        Collider = new CollisionRect(this, Sprite.Width, Sprite.Height);
    }
    protected override void Apply()
    {
        Player.Instance.Party.Add(new PlayerStats(charType));
    }
    private void SetCharacterType(CharType teammate)
    {
        switch(teammate)
        {
            case CharType.Zelda:
                Sprite = TeamSpriteFactory.Instance.CreateZeldaSprite();
                break;
            case CharType.Merchant:
                Sprite = TeamSpriteFactory.Instance.CreateMerchantSprite();
                break;
            case CharType.OldLady:
                Sprite = TeamSpriteFactory.Instance.CreateOldSprite();
                break;
            default:
                Sprite = EnemySpriteFactory.Instance.KrisSprite();
                break;
        }
    }

    private CharType StringToCharType(string type)
    {
        return type switch
        {
            "zelda" => CharType.Zelda,
            "merchant" => CharType.Merchant,
            "old" => CharType.OldLady,
            _ => CharType.Link
        };
    }
}
