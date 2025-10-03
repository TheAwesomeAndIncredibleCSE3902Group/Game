using System;
using Sprint0.Sprites;
using Microsoft.Xna.Framework;
using static Sprint0.Util;
using System.Data;

namespace Sprint0;

/// <summary>
/// Arrow shot by the player. Managed by Player.
/// </summary>
public class PlayerArrow : Projectile
{
    int damage;
    public PlayerArrow(Vector2 position, Cardinal direction)
    {
        this.position = position;
        this.direction = direction;

        this.movementSpeed = 2;
        this.lifetime = 2;
        this.damage = 2;

        //Didn't work with arrow sprite, rework later
        sprite = MapItemSpriteFactory.CreatePotionSprite();
    }

    public override void Destroy()
    {
        Player.Instance.spawnedProjectiles.Remove(IEquipment.Projectiles.arrow);
    }
}
