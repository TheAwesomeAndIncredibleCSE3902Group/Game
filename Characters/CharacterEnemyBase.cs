using System;
using System.Drawing;
using Sprint0.Sprites;
using Sprint0.Tiles;
using Microsoft.Xna.Framework;
using static Sprint0.Util;

namespace Sprint0.Characters;

// ICharacter(ISprite sprite, Point position);

public abstract class CharacterEnemyBase : ICharacter
{
    protected AnimatableSprite _sprite;

    public Vector2 Position;

    public Cardinal Direction;

    private Boolean _moving = true;

    
    public CharacterEnemyBase(Vector2 position, Cardinal direction)
    {
        this.Position = position;
        this.Direction = direction;
    }

    public void Update(GameTime gameTime)
    {        
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
}
