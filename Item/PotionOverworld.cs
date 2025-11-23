using System;
using System.Diagnostics;
using AwesomeRPG.Collision;
using AwesomeRPG.Map;
using AwesomeRPG.Sprites;

namespace AwesomeRPG;

public class PotionOverworld : Pickup
{
    public PotionOverworld(RoomMap levelMap) : base(levelMap)
    { 
        Sprite = MapItemSpriteFactory.CreatePotionSprite();
        Collider = new CollisionRect(this, Sprite.Width, Sprite.Height);
    }
    protected override void Apply(PlayerOverworld player)
    {
        Player.Instance.Inventory[IInventoryItem.Type.potion]++;

        TestPrintApply();
    }

    private void TestPrintApply()
    {
        Console.WriteLine("Picked up potion!");
        Console.WriteLine("Player now has " + Player.Instance.Inventory[IInventoryItem.Type.potion]  + " potions!");
    }
}
