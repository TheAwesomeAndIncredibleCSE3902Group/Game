using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AwesomeRPG;

/// <summary>
/// The SuperSwordSheathe that holds a sword with a swordBeam
/// </summary>
public class SuperSwordSheathe : IEquipment
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
        SwordBeam swordBeam = new SwordBeam(player.Position, player.FacingDirection);
        spawnedProjectiles[IEquipment.Projectiles.swordBeam] = swordBeam;
    }
}
