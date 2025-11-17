using System;
using System.Collections.Generic;
using AwesomeRPG.Map;

namespace AwesomeRPG;
public enum Weapons { bow, boomerangSack, swordSheathe, superSwordSheathe }

public abstract class Equipment
{
    
    public void Use()
    {
        //Player can only ever have one arrow on screen, so if arrow already exists then abort
        if (RoomAtlas.Instance.CurrentRoom.Projectiles.Count > 0)
            return;

        List<Projectile> projectilesToAdd = CreateProjectiles();
        AddProjectilesToRoom(projectilesToAdd);
    }

    protected abstract List<Projectile> CreateProjectiles();

    private void AddProjectilesToRoom(List<Projectile> projectiles)
    {
        foreach (Projectile projectile in projectiles)
        {
            RoomAtlas.Instance.AddProjectile(projectile);
        }
    }
}
