using System;
using System.Collections.Generic;
using AwesomeRPG.Stats;


namespace AwesomeRPG;

/// <summary>
/// Represents the player in both the Overworld and the Battle mode
/// </summary>
public class Player
{
    public static Player Instance { get; private set; }
    public PlayerStats PlayerStats { get; init; }
    public PlayerOverworld PlayerOverworld { get; init; }
    public Dictionary<IInventoryItem.Type, int> Inventory { get; private set; }

    public Player(PlayerStats pStats, PlayerOverworld pOverworld)
    {
        Instance = this;
        PlayerStats = pStats;
        PlayerOverworld = pOverworld;
        
        InitInventory();
    }

    private void InitInventory()
    {
        Inventory = new();
        //Cheeky little reflection means the entire Inventory dictionary can be initialized without explicitly knowing every Type
        foreach (IInventoryItem.Type inventoryType in Enum.GetValues(typeof(IInventoryItem.Type)))
            Inventory[inventoryType] = 0;
    }
}