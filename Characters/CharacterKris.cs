using System;
using System.Drawing;
using Microsoft.Xna.Framework;
using Sprint0.Sprites;
using Sprint0.Tiles;

namespace Sprint0.Characters;

// ICharacter(ISprite sprite, Point position);

public class CharacterKris : ICharacter
{
    public CharacterKris()
    {
        _sprite = CharacterSpriteFactory.Instance.KrisSprite(); 
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
}
