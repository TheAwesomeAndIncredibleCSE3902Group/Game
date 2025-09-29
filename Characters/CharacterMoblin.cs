using System;
using System.Drawing;
using Sprint0.Sprites;
using Sprint0.Tiles;
using Microsoft.Xna.Framework;
using static Sprint0.Util;

namespace Sprint0.Characters;

// ICharacter(ISprite sprite, Point position);

public class CharacterMoblin : ICharacter
{
    private AnimatableSprite sprite;

    public Vector2 Position;

    public Cardinal Direction;

    
    public CharacterMoblin(Vector2 position, Cardinal direction)
    {
        this.Position = position;
        this.Direction = direction;

        sprite = CharacterSpriteFactory.Instance.MoblinSprite();

    }

    public void Update(GameTime gameTime)
    {

    }

    public void Draw(GameTime gameTime)
    {
        sprite.Draw(gameTime, Position);
    }
}
