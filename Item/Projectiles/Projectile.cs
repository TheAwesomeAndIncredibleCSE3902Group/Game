using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using AwesomeRPG.Sprites;
using static AwesomeRPG.Util;
using AwesomeRPG.Collision;

namespace AwesomeRPG;

/// <summary>
/// Defines movement and drawing for an object that travels a linear path with a limited lifespan
/// </summary>
public abstract class Projectile : CollisionObject
{
    public ISprite sprite;

    protected Cardinal direction;
    //Pixels per tick. Might change to pixels per second later
    protected float movementSpeed;
    //In seconds
    protected float lifetime;
    protected float age = 0;

    public void Update(GameTime gt)
    {
        age += (float)gt.ElapsedGameTime.TotalSeconds;
        if (age > lifetime)
            Destroy();
        else
        {
            Move();
        }
    }

    protected virtual void Move()
    {
        Position += movementSpeed * Util.CardinalToUnitVector(direction);
    }

    public void Draw(GameTime gt)
    {
        sprite.Draw(gt, Position);
    }

    public abstract void Destroy();
}
