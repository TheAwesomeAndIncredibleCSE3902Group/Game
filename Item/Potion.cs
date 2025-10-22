using System;
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
        player.TakeDamage(-1);
    }
}
