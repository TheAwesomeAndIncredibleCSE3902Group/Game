using System;
using Sprint0.Sprites;
using Microsoft.Xna.Framework;
using static Sprint0.Util;
using System.Data;

namespace Sprint0;

/// <summary>
/// Boomerang thrown by the player. Managed by Player.
/// </summary>
public class PlayerBoomerang : Projectile
{
    int damage;
    float initialSpeed;
    public PlayerBoomerang(Vector2 position, Cardinal direction)
    {
        
        this.position = position;
        this.direction = direction;
        initialSpeed = 2;
        movementSpeed = initialSpeed;
        initialSpeed = movementSpeed;
        lifetime = 3;
        damage = 1;

        //Didn't work with arrow sprite, rework later
        sprite = MapItemSpriteFactory.CreateCandleSprite();
    }

    public override void Update(GameTime gt)
    {
        base.Update(gt);
        movementSpeed = initialSpeed - (age / lifetime) * 2 * initialSpeed; //Silly math to make the boomerang go back
    }

    public override void Destroy()
    {
        Player.Instance.spawnedProjectiles.Remove(IEquipment.Projectiles.boomerang);
    }
}
