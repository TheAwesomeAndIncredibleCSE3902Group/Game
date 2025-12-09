using System;
using System.Diagnostics;
using AwesomeRPG.Collision;
using AwesomeRPG.Map;
using AwesomeRPG.Sprites;

namespace AwesomeRPG;

/// <summary>
/// Represents a bow in the player's inventory
/// </summary>
public class BowInventory : IInventoryItem
{
    public IInventoryItem.Type ThisType => IInventoryItem.Type.bow;

    public BowInventory()
    {

    }

    public void Apply()
    {
        
    }
}
