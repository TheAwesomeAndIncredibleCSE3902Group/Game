using System;
using Microsoft.Xna.Framework;
using Sprint0.Sprites;

namespace Sprint0.Item;

public interface IMapItem
{
    public bool Enabled { get; set; }
    public ISprite Sprite { get; }
    public void BePickedUp();
    public void Draw(GameTime gt);
}
