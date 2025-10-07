using System;
using System.Drawing;
using AwesomeRPG.Sprites;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.Characters;

// ICharacter(ISprite sprite, Point position);

public interface ICharacter
{

    public void Update(GameTime gameTime);
    public void Draw(GameTime gameTime);
}
