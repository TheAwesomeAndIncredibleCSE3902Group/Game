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
    public static int healing = 30;

    public IInventoryItem.Type ThisType => IInventoryItem.Type.potion;

    public PotionInventory()
    {

    }

    /// <summary>
    /// Returns whether it was successful at applying a potion
    /// </summary>
    /// <returns></returns>
    public static bool Apply()
    {
        Player player = Player.Instance;
        if (player.Inventory[IInventoryItem.Type.potion] < 1)
            return false;
        
        //Don't allow potion to be consumed if it can't all be used
        int healingHeadroom = player.Party[0].GetMaxHealth() - player.Party[0].GetHealth();
        if (healingHeadroom < healing)
            return false;

        player.Inventory[IInventoryItem.Type.potion]--;
        player.Party[0].ChangeHealth(healing);
        return true;
    }
}
