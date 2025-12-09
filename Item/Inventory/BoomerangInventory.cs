using System;
using System.Diagnostics;
using AwesomeRPG.Collision;
using AwesomeRPG.Map;
using AwesomeRPG.Sprites;

namespace AwesomeRPG;

/// <summary>
/// Represents a boomerang in the player's inventory
/// </summary>
public class BoomerangInventory : IInventoryItem
{
    public IInventoryItem.Type ThisType => IInventoryItem.Type.boomerang;

    public BoomerangInventory()
    {

    }

    public void Apply()
    {
        
    }
}
