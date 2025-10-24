using System;
using System.Diagnostics;
using AwesomeRPG.Sprites;

namespace AwesomeRPG;

public class Potion : Pickup
{
    public Potion() : base()
    {
        Sprite = MapItemSpriteFactory.CreatePotionSprite();
    }
    protected override void Apply(Player player)
    {
        //TODO: this should definitely be a separate heal method
        Debug.WriteLine("Potion picked up");
        player.TakeDamage(-1);
    }
}
