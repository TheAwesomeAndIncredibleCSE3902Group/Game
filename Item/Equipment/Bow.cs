using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sprint0;

/// <summary>
/// The Bow can shoot an Arrow
/// </summary>
public class Bow : IEquipment
{
    public void Use()
    {
        Player player = Player.Instance;
        Dictionary<IEquipment.Projectiles,Projectile> spawnedProjectiles = player.spawnedProjectiles;

        //Player can only ever have one arrow on screen, so if arrow already exists then abort
        if (spawnedProjectiles.ContainsKey(IEquipment.Projectiles.arrow))
            return;

        //PlayerArrow arrow = new PlayerArrow(player.Position, player.FacingDirection);
        WaveyArrow arrow = new WaveyArrow(player.Position, player.FacingDirection);
        spawnedProjectiles[IEquipment.Projectiles.arrow] = arrow;
    }
}
