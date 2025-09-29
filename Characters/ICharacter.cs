using System;
using System.Drawing;
using Sprint0.Sprites;

namespace Sprint0.Characters;

// ICharacter(ISprite sprite, Point position);

public interface ICharacter
{

    void Update();
    void Draw();
}
