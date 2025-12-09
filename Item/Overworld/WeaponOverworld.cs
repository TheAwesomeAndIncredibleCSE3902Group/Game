using System;
using System.Diagnostics;
using AwesomeRPG.Collision;
using AwesomeRPG.Commands;
using AwesomeRPG.Map;
using AwesomeRPG.Sprites;
using AwesomeRPG.Stats;

namespace AwesomeRPG;

public class WeaponOverworld : Pickup
{
    private string _weaponTypeName;
    public WeaponOverworld(RoomMap levelMap, string weaponTypeName) : base(levelMap)
    {
        Sprite = ParseStringToSprite(weaponTypeName);
        _weaponTypeName = weaponTypeName;
        Collider = new CollisionRect(this, Sprite.Width, Sprite.Height);
    }
    protected override void Apply()
    {
        switch(_weaponTypeName)
        {
            case "boomerang":
                Player.Instance.Inventory[IInventoryItem.Type.boomerang]++;
                break;
            case "bow":
                Player.Instance.Inventory[IInventoryItem.Type.bow]++;
                break;
            case "beamsword":
                Player.Instance.Inventory[IInventoryItem.Type.beamSword]++;
                break;
            default:
                break;

        }
    }

    private ISprite ParseStringToSprite(string type)
    {
        return type switch
        {
            "boomerang" => ProjectileSpriteFactory.CreateBoomerangSprite(),
            "bow" => ProjectileSpriteFactory.CreateArrowSprite(Util.Cardinal.up),
            "beamsword" => ProjectileSpriteFactory.CreateSwordBeamSprite(Util.Cardinal.up),
            _ => ProjectileSpriteFactory.CreateFireSprite()
        } ;
    }
}
