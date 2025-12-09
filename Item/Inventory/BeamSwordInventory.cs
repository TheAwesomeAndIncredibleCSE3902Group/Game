using System;
using System.Diagnostics;
using AwesomeRPG.Collision;
using AwesomeRPG.Map;
using AwesomeRPG.Sprites;

namespace AwesomeRPG;

/// <summary>
/// Represents a beamsword in the player's inventory
/// </summary>
public class BeamSwordInventory : IInventoryItem
{
    public IInventoryItem.Type ThisType => IInventoryItem.Type.beamSword;

    public BeamSwordInventory()
    {

    }

    public void Apply()
    {
        
    }
}
