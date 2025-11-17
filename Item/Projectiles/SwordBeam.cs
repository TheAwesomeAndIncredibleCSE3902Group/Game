using System;
using AwesomeRPG.Sprites;
using Microsoft.Xna.Framework;
using static AwesomeRPG.Util;
using System.Data;
using AwesomeRPG.Collision;

namespace AwesomeRPG;

/// <summary>
/// Swordbeam shot by the player. Managed by Player.
/// </summary>
public class SwordBeam : Projectile
{
    public SwordBeam(Vector2 position, Cardinal direction)
    {   
        this.direction = direction;

        movementSpeed = 4;
        lifetime = 2;

        //Didn't work with arrow sprite, rework later
        sprite = ProjectileSpriteFactory.CreateSwordBeamSprite(direction);
        Position = position;
        Collider = new CollisionRect(this, sprite.Width, sprite.Height);
        ObjectType = CollisionObjectType.PlayerProjectile;
    }

}
