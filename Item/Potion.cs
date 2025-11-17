using System;
using System.Diagnostics;
using AwesomeRPG.Collision;
using AwesomeRPG.Map;
using AwesomeRPG.Sprites;

namespace AwesomeRPG;

public class Potion : Pickup
{
    public Potion(RoomMap levelMap) : base(levelMap)
    { 
        Sprite = MapItemSpriteFactory.CreatePotionSprite();
        Collider = new CollisionRect(this, Sprite.Width, Sprite.Height);
    }
    protected override void Apply(PlayerOverworld player)
    {
        //TODO: this should definitely be a separate heal method
        Debug.WriteLine("Potion picked up");
        player.TakeDamage(-1);
    }
}
