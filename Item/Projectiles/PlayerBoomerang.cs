using System;
using AwesomeRPG.Sprites;
using Microsoft.Xna.Framework;
using static AwesomeRPG.Util;
using System.Data;

namespace AwesomeRPG;

/// <summary>
/// Boomerang thrown by the player. Managed by Player.
/// </summary>
public class PlayerBoomerang : Projectile
{
    float initialSpeed;
    public PlayerBoomerang(Vector2 position, Cardinal direction)
    {

        this.position = position;
        this.direction = direction;
        initialSpeed = 2;
        movementSpeed = initialSpeed;
        initialSpeed = movementSpeed;
        lifetime = 3;

        //Didn't work with arrow sprite, rework later
        sprite = ItemSpriteFactory.CreateBoomerangSprite();
    }

/*
    protected override void Move()
    {
        base.Move();
        movementSpeed = initialSpeed - (age / lifetime) * 2 * initialSpeed; //Silly math to make the boomerang go back
    }
    */

    protected override void Move()
    {
        if (age < lifetime / 2)
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

        Vector2 toPlayer = Player.Instance.Position - position;
        toPlayer.Normalize();
        position += 2 * movementSpeed * toPlayer;
    }

    public override void Destroy()
    {
        Player.Instance.spawnedProjectiles.Remove(IEquipment.Projectiles.boomerang);
    }
}
