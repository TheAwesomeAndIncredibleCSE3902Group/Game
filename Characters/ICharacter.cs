using System;
using System.Drawing;
using Sprint0.Sprites;
using Microsoft.Xna.Framework;

namespace Sprint0.Characters;

// ICharacter(ISprite sprite, Point position);

public interface ICharacter
{

    public void Update(GameTime gameTime);
    public void Draw(GameTime gameTime);
}
