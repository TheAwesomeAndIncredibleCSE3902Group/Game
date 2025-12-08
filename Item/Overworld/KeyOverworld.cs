using System;
using System.Diagnostics;
using AwesomeRPG.Collision;
using AwesomeRPG.Commands;
using AwesomeRPG.Map;
using AwesomeRPG.Sprites;

namespace AwesomeRPG;

public class KeyOverworld : Pickup
{
    private UseKeyCommand _useKeyCommand;
    
    public KeyOverworld(RoomMap levelMap, int roomX, int roomY, int lockId) : base(levelMap)
    { 
        Sprite = MapItemSpriteFactory.CreateKeySprite();
        Collider = new CollisionRect(this, Sprite.Width, Sprite.Height);
        _useKeyCommand = new(roomX, roomY, lockId);
    }
    protected override void Apply()
    {
        _useKeyCommand.Execute();
    }
}
