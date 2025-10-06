using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Sprint0.Sprites;
using static Sprint0.Util;

namespace Sprint0;

/// <summary>
/// Defines movement and drawing for an object that travels a linear path with a limited lifespan
/// </summary>
public abstract class Projectile
{
    public ISprite sprite;

    protected Vector2 position;
    protected Cardinal direction;
    //Pixels per tick. Might change to pixels per second later
    protected float movementSpeed;
    //In seconds
    protected float lifetime;
    protected float age = 0;

    public void Update(GameTime gt)
    {
        age += gt.ElapsedGameTime.Milliseconds / 1000f;
        if (age > lifetime)
            Destroy();
        else
        {
            Move();
        }
    }

    protected virtual void Move()
    {
        position += movementSpeed * Util.CardinalToUnitVector(direction);
    }

    public void Draw(GameTime gt)
    {
        sprite.Draw(gt, position);
    }

    public abstract void Destroy();
}
