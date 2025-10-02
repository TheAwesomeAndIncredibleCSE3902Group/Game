using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sprint0;

/// <summary>
/// The Bow can shoot an Arrow, which it assigns to Player.Arrow
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

        const float arrowSpeed = 2;
        const float lifeTime = 2;
        const int damage = 2;
        PlayerArrow arrow = new PlayerArrow(player.Position, player.FacingDirection, arrowSpeed, lifeTime, damage);
        spawnedProjectiles[IEquipment.Projectiles.arrow] = arrow;
    }
}
