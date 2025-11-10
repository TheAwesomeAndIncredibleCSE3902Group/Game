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

    public int MoveSpeed { get; init; } = 133;

    private Cardinal direction;

    protected bool _moving = true;

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
    

    
    public CharacterEnemyBase(Vector2 position, Cardinal direction)
    {
        this.Position = position;
        this.Direction = direction;

        ChangeDirectionalSprite(direction);
        Collider = new CollisionRect(this, _sprite.Width, _sprite.Height);
        ObjectType = CollisionObjectType.Enemy;
    }

    private void HandleMovement(GameTime gameTime)
    {
        if(_moving)
        {
            if (Pathing is not null)
            {
                Pathing.Update(gameTime);
                Direction = Pathing.GetDirection();
            }
            Position += CardinalToUnitVector(Direction) * (float)gameTime.ElapsedGameTime.TotalSeconds * MoveSpeed;
        }
    }


    public virtual void Update(GameTime gameTime)
    {
        HandleMovement(gameTime);
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
