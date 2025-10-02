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
    public PlayerArrow(Vector2 position, Cardinal direction, float movementSpeed = 1, float lifetime = 3, int damage = 1)
    {
        this.position = position;
        this.direction = direction;
        this.movementSpeed = movementSpeed;
        this.lifetime = lifetime;

        this.damage = damage;

        //Didn't work with arrow sprite, rework later
        sprite = OverworldItemSpriteFactory.Instance.CreatePotionSprite();
    }

    public override void Destroy()
    {
        Player.Instance.spawnedProjectiles.Remove(IEquipment.Projectiles.arrow);
    }
}
