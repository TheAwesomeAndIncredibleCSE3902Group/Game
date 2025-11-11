using Microsoft.Xna.Framework;
using static AwesomeRPG.Util;
using AwesomeRPG.Collision;
using AwesomeRPG.Map;

namespace AwesomeRPG;

/// <summary>
/// Fire shot by the Moblin
/// </summary>
public class MoblinFire : Projectile
{
    public MoblinFire(Vector2 position, Cardinal direction)
    {
        this.direction = direction;

        this.movementSpeed = 6;
        this.lifetime = 1;

        sprite = ProjectileSpriteFactory.CreateFireSprite();
        Position = position;
        Collider = new CollisionRect(this, sprite.Width, sprite.Height);
        ObjectType = CollisionObjectType.EnemyProjectile;
    }

    public override void Destroy()
    {
        RoomAtlas.Instance.RemoveProjectile(this);
    }
}
