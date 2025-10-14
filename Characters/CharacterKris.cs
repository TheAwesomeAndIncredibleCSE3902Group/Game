using System;
using System.Drawing;
using Microsoft.Xna.Framework;
using AwesomeRPG.Sprites;
using AwesomeRPG.Map;

namespace AwesomeRPG.Characters;

// ICharacter(ISprite sprite, Point position);

public class CharacterKris : ICharacter
{
    public CharacterKris()
    {
        _sprite = CharacterSpriteFactory.Instance.KrisSprite(); 
    }
    public IPathingScheme Pathing
    {
        //This Character does not have an IPathingScheme
        get => null;
        set { }
    }
    protected AnimatableSprite _sprite;
    public Vector2 Position;

    public void Update(GameTime gameTime)
    {
        Position.X = (float) Math.Cos(gameTime.TotalGameTime.TotalMilliseconds / 1000.0) * 100.0f + 200f;
        Position.Y = (float) Math.Sin(gameTime.TotalGameTime.TotalMilliseconds / 1000.0) * 100.0f + 200f;
    }
    public void Draw(GameTime gameTime)
    {
        _sprite.Draw(gameTime, Position);
    }

    public void ChangeDirection(Util.Cardinal direction)
    {
        throw new NotImplementedException();
    }
}
