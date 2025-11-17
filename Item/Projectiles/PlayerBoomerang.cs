using System;
using AwesomeRPG.Sprites;
using Microsoft.Xna.Framework;
using static AwesomeRPG.Util;
using System.Data;
using AwesomeRPG.Collision;

namespace AwesomeRPG;

/// <summary>
/// Boomerang thrown by the player. Managed by Player.
/// </summary>
public class PlayerBoomerang : Projectile
{
    float initialSpeed;

    //In pixels. Used to return this to the middle of the player
    const float approxPlayerSize = 16 * 3;
    public PlayerBoomerang(Vector2 position, Cardinal direction)
    {
        this.direction = direction;
        initialSpeed = 6;
        movementSpeed = initialSpeed;
        initialSpeed = movementSpeed;
        lifetime = 2;

        //Didn't work with arrow sprite, rework later
        sprite = ProjectileSpriteFactory.CreateBoomerangSprite();
        Position = position;
        Collider = new CollisionRect(this, sprite.Width, sprite.Height);
        ObjectType = CollisionObjectType.PlayerProjectile;
    }

    //Moves forward for half its life, then moves backward
    public bool IsMovingForward { get => age < lifetime / 2; }

    protected override void Move()
    {
        if (IsMovingForward)
            MoveForward();
        else
            MoveBack();
    }

    private void MoveForward()
    {
        base.Move();
    }
    
    /// <summary>
    /// Moves back a bit faster so it is basically guaranteed to get back to the player
    /// </summary>
    private void MoveBack()
    {
        //Vector2 toPlayer = Player.Instance.Position - Position;
        Vector2 toPlayer = PlayerOverworld.Instance.Position - Position + new Vector2(approxPlayerSize / 2f);
        toPlayer.Normalize();
        Position += 2 * movementSpeed * toPlayer;
    }

}
