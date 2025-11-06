using System;
using System.Collections.Generic;
using AwesomeRPG.Map;

namespace AwesomeRPG;

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

        WaveyArrow arrow = new WaveyArrow(player.Position, player.FacingDirection);
        spawnedProjectiles[IEquipment.Projectiles.arrow] = arrow;
        RoomAtlas.Instance.CurrentRoom._movingCollisionObjects.Add(arrow);
    }
}
