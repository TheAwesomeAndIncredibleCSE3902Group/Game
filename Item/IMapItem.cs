using System;
using Microsoft.Xna.Framework;
using AwesomeRPG.Sprites;

namespace AwesomeRPG.Item;

public interface IMapItem
{
    public bool Enabled { get; set; }
    public ISprite Sprite { get; }
    public void BePickedUp();
    public void Draw(GameTime gt);
}
