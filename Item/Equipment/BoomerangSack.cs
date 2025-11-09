using System;
using System.Collections.Generic;
using AwesomeRPG.Map;
using System.Diagnostics;

namespace AwesomeRPG;

/// <summary>
/// The BoomerangSack can throw a Boomerang
/// </summary>
public class BoomerangSack : IEquipment
{
    public void Use()
    {
        Player player = Player.Instance;
        Dictionary<IEquipment.Projectiles,Projectile> spawnedProjectiles = player.spawnedProjectiles;

        //Player can only ever have one arrow on screen, so if arrow already exists then abort
        if (spawnedProjectiles.ContainsKey(IEquipment.Projectiles.boomerang))
            return;

        PlayerBoomerang boomerang = new PlayerBoomerang(player.Position, player.FacingDirection);
        spawnedProjectiles[IEquipment.Projectiles.boomerang] = boomerang;
        RoomAtlas.Instance.CurrentRoom._movingCollisionObjects.Add(boomerang);
    }
}
