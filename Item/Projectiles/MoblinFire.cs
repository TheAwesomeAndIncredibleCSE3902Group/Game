using Microsoft.Xna.Framework;
using static AwesomeRPG.Util;
using AwesomeRPG.Collision;
using AwesomeRPG.Map;
using AwesomeRPG.Characters;

namespace AwesomeRPG;

/// <summary>
/// Fire shot by the Moblin
/// </summary>
public class MoblinFire : Projectile
{
    //That which fired this
    public ICharacter Firee { get; private set; }

    public MoblinFire(Vector2 position, Cardinal direction, ICharacter firee)
    {
        this.direction = direction;
        this.Firee = firee;

        this.movementSpeed = 3;
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
