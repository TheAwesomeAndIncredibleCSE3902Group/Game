using System;
using System.Drawing;
using AwesomeRPG.Sprites;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.Characters;

// ICharacter(ISprite sprite, Point position);

public interface ICharacter
{
    public IPathingScheme Pathing {get; set;}

    public void Update(GameTime gameTime);
    public void Draw(GameTime gameTime);
    public void ChangeDirection(Util.Cardinal direction);
}
