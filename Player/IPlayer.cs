using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sprint0.Sprites;
using static Sprint0.Util;

namespace Sprint0;

public interface IPlayer
{
    public static IPlayer Instance { get; }
    //I'm not entirely sure whether Player or PlayerState should handle this sprite
    public ISprite Sprite { get; }
    public IPlayerState PlayerState { get; }
    public List<IEquipment> Equipment { get; }

    public void Update(GameTime gt);
    public void Draw(GameTime gt);
}
