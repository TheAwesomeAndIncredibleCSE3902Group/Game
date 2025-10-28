using System;
using System.Drawing;
using AwesomeRPG.Sprites;
using AwesomeRPG.Map;
using Microsoft.Xna.Framework;
using AwesomeRPG.Collision;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Characters;

public abstract class CharacterEnemyBase : CollisionObject, ICharacter
{
    protected AnimatableSprite _sprite;
    public IPathingScheme Pathing { get; set; } = null;

    public Cardinal Direction
    {
        get
        {
            return direction;
        }
        set
        {
            if (value == direction)
                return;

            ChangeDirectionalSprite(value);
            direction = value;
        }
    }
    private Cardinal direction;

    private bool _moving = true;

    
    public CharacterEnemyBase(Vector2 position, Cardinal direction)
    {
        this.Position = position;
        this.Direction = direction;

        int spriteSize = 15;
        Collider = new CollisionRect(this, spriteSize * 3, spriteSize * 3);
        ObjectType = CollisionObjectType.Enemy;
    }

    public void Update(GameTime gameTime)
    {
        if (Pathing is not null)
        {
            Pathing.Update(gameTime);
            Direction = Pathing.GetDirection();
        }

        if (_moving)
        {
            Position += CardinalToUnitVector(Direction) * (float)gameTime.ElapsedGameTime.TotalSeconds * 100;
        }
    }

    public void Draw(GameTime gameTime)
    {
        _sprite.Draw(gameTime, Position);
    }


    /// <summary>
    /// Overrides Pathing and sets the direction
    /// </summary>
    /// <param name="direction"></param>
    public void ForceDirection(Cardinal direction)
    {
        if (Pathing is not null)
            Pathing.TrySetDirection(direction);

        Direction = direction;
    }
    
    public abstract void ChangeDirectionalSprite(Cardinal direction);
}
