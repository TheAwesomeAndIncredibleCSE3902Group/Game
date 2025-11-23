using System;
using System.Diagnostics;
using AwesomeRPG.Collision;
using AwesomeRPG.Map;
using AwesomeRPG.Sprites;

namespace AwesomeRPG;

/// <summary>
/// Represents a typical potion in the player's inventory
/// </summary>
public class PotionInventory : IInventoryItem
{
    public readonly int healing = 5;

    public IInventoryItem.Type ThisType => IInventoryItem.Type.potion;

    public PotionInventory()
    {

    }

    public void Apply()
    {
        Player.Instance.PlayerStats.ChangeHealth(healing);
    }
}
