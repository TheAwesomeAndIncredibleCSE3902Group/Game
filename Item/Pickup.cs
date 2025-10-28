using System;
using AwesomeRPG.Collision;
using AwesomeRPG.Map;
using AwesomeRPG.Sprites;
using Microsoft.Xna.Framework;

namespace AwesomeRPG;

/// <summary>
/// Pickups have no collision *except* with Player.
/// Once they are touched by the Player, they run an Apply method and then Destroy themselves
/// </summary>
public abstract class Pickup : CollisionObject
{
    public ISprite Sprite { get; protected set;  }

    private RoomMap LevelMap {  get; init; }
    public Pickup(RoomMap levelMap)
    {
        LevelMap = levelMap;
        ObjectType = CollisionObjectType.Pickup;
    }

    public void OnPlayerTouched(Player player)
    {
        //Run the Apply method of the specific Pickup that extends this
        this.Apply(player);

        //Then destroy this
        Destroy();
    }

    public void Draw(GameTime gt)
    {
        Sprite.Draw(gt, Position);
    }

    protected abstract void Apply(Player player);
    private void Destroy()
    {
        LevelMap.Pickups.Remove(this);
    }

}
