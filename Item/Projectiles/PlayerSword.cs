using System;
using Sprint0.Sprites;
using Microsoft.Xna.Framework;
using static Sprint0.Util;
using System.Data;

namespace Sprint0;

/// <summary>
/// Arrow shot by the player. Managed by Player.
/// </summary>
public class PlayerSword : Projectile
{
    int damage;
    public PlayerSword(Vector2 position, Cardinal direction)
    {
        int swordOffset = 40;
        this.position = position + Util.CardinalToUnitVector(direction)*swordOffset;
        this.direction = direction;

        this.movementSpeed = 0;
        this.lifetime = 2;
        this.damage = 2;

        sprite = ItemSpriteFactory.CreateSwordSprite(direction);
    }

    protected override void Move()
    {
       //Sword does not move
    }

    public override void Destroy()
    {
        Player.Instance.spawnedProjectiles.Remove(IEquipment.Projectiles.sword);
    }
}
