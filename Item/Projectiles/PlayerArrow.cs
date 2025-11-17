using System;
using AwesomeRPG.Sprites;
using Microsoft.Xna.Framework;
using static AwesomeRPG.Util;
using System.Data;
using AwesomeRPG.Collision;

namespace AwesomeRPG;

/// <summary>
/// Arrow shot by the player. Managed by Player.
/// </summary>
public class PlayerArrow : Projectile
{
    public PlayerArrow(Vector2 position, Cardinal direction)
    {
        this.direction = direction;

        this.movementSpeed = 6;
        this.lifetime = 1;

        //Didn't work with arrow sprite, rework later
        sprite = ProjectileSpriteFactory.CreateArrowSprite(direction);
        Position = position;
        Collider = new CollisionRect(this, sprite.Width, sprite.Height);
        ObjectType = CollisionObjectType.PlayerProjectile;
    }

}
