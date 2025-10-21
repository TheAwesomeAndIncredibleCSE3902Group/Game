using System;
using System.Drawing;
using AwesomeRPG.Sprites;
using AwesomeRPG.Map;
using Microsoft.Xna.Framework;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Characters;

// ICharacter(ISprite sprite, Point position);

public abstract class CharacterEnemyBase : ICharacter
{
    protected AnimatableSprite _sprite;
    public IPathingScheme Pathing { get; set; } = null;

    public Vector2 Position;

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

            ChangeDirection(value);
            direction = value;
        }
    }
    private Cardinal direction;

    private bool _moving = true;

    
    public CharacterEnemyBase(Vector2 position, Cardinal direction)
    {
        this.Position = position;
        this.Direction = direction;
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
            switch (Direction)
            {
                case Cardinal.up:
                    Position.Y -= (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 10.0);
                    break;
                case Cardinal.down:
                    Position.Y += (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 10.0);
                    break;
                case Cardinal.left:
                    Position.X -= (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 10.0);
                    break;
                case Cardinal.right:
                    Position.X += (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 10.0);
                    break;
            }
        }
    }

    public void Draw(GameTime gameTime)
    {
        _sprite.Draw(gameTime, Position);
    }

    public abstract void ChangeDirection(Cardinal direction);
}
