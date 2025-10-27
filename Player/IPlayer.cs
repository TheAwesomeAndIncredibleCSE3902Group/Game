using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using AwesomeRPG.Sprites;
using static AwesomeRPG.Util;

namespace AwesomeRPG;

public interface IPlayer
{
    public static IPlayer Instance { get; }
    public Cardinal FacingDirection { get; }
    public Vector2 Position { get; set; }
    //I'm not entirely sure whether Player or PlayerState should handle this sprite
    public ISprite Sprite { get; set; }
    public PlayerCollisionHandler CollisionHandler { get; }

    public List<IEquipment> Equipment { get; }

    public void BeIdle();
    public void Update(GameTime gt);
    public void Draw(GameTime gt);
    public void Move(Cardinal direction);
    public void UseEquipment();
}
