using System;
using System.Collections.Generic;
using AwesomeRPG.Map;
using System.Diagnostics;

namespace AwesomeRPG;

/// <summary>
/// The SwordSheathe that holds a sword
/// </summary>
public class SwordSheathe : IEquipment
{
    public void Use()
    {
        Player player = Player.Instance;
        Dictionary<IEquipment.Projectiles,Projectile> spawnedProjectiles = player.spawnedProjectiles;

        //Player can only ever have one arrow on screen, so if arrow already exists then abort
        if (spawnedProjectiles.ContainsKey(IEquipment.Projectiles.sword))
            return;

        PlayerSword sword = new PlayerSword(player.Position, player.FacingDirection);
        spawnedProjectiles[IEquipment.Projectiles.sword] = sword;
        RoomAtlas.Instance.CurrentRoom._movingCollisionObjects.Add(sword);
    }
}
