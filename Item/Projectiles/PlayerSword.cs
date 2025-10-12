using System;
using AwesomeRPG.Sprites;
using Microsoft.Xna.Framework;
using static AwesomeRPG.Util;
using System.Data;
using StateMachineDemo;

namespace AwesomeRPG;

/// <summary>
/// Arrow shot by the player. Managed by Player.
/// </summary>
public class PlayerSword : Projectile
{
    int damage;
    int swordOffset = 40;
    public PlayerSword(Vector2 position, Cardinal direction)
    {
        this.direction = direction;

        this.position = position + Util.CardinalToUnitVector(direction) * swordOffset;

        this.movementSpeed = 0;
        this.lifetime = 0.5f;
        this.damage = 2;

        sprite = ItemSpriteFactory.CreateSwordSprite(direction);
    }

    protected override void Move()
    {
       //Sword tracks the player
       this.position = Player.Instance.Position + Util.CardinalToUnitVector(direction) * swordOffset;
    }

    public override void Destroy()
    {
        Player.Instance.spawnedProjectiles.Remove(IEquipment.Projectiles.sword);
    }
}
