using AwesomeRPG.Collision;
using Microsoft.Xna.Framework;
using static AwesomeRPG.Util;

namespace AwesomeRPG;

/// <summary>
/// Arrow shot by the player. Managed by Player.
/// </summary>
public class PlayerSword : Projectile
{
    int swordOffset = 40;
    public PlayerSword(Vector2 position, Cardinal direction)
    {
        this.direction = direction;

        this.movementSpeed = 0;
        this.lifetime = 0.5f;

        sprite = ProjectileSpriteFactory.CreateSwordSprite(direction);

        Position = position + Util.CardinalToUnitVector(direction) * swordOffset;
        Collider = new CollisionRect(this, sprite.Width, sprite.Height);
        ObjectType = CollisionObjectType.PlayerProjectile;
    }

    protected override void Move()
    {
       //Sword tracks the player
       this.Position = PlayerOverworld.Instance.Position + Util.CardinalToUnitVector(direction) * swordOffset;
    }

}
