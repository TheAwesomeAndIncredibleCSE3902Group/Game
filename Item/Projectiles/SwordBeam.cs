using System;
using AwesomeRPG.Sprites;
using Microsoft.Xna.Framework;
using static AwesomeRPG.Util;
using System.Data;

namespace AwesomeRPG;

/// <summary>
/// Swordbeam shot by the player. Managed by Player.
/// </summary>
public class SwordBeam : Projectile
{
    int damage;
    public SwordBeam(Vector2 position, Cardinal direction)
    {
        this.position = position;
        this.direction = direction;

        this.movementSpeed = 4;
        this.lifetime = 2;
        this.damage = 2;

        //Didn't work with arrow sprite, rework later
        sprite = ItemSpriteFactory.CreateSwordBeamSprite(direction);
    }

    public override void Destroy()
    {
        Player.Instance.spawnedProjectiles.Remove(IEquipment.Projectiles.swordBeam);
    }
}
