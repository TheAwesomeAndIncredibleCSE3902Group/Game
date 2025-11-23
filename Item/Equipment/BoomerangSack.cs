using System;
using System.Collections.Generic;
using AwesomeRPG.Map;
using System.Diagnostics;

namespace AwesomeRPG;

/// <summary>
/// The BoomerangSack can throw a Boomerang
/// </summary>
public class BoomerangSack : Equipment
{
    protected override List<Projectile> CreateProjectiles()
    {
        PlayerOverworld player = Player.Instance.PlayerOverworld;
        return [new PlayerBoomerang(player.Position, player.FacingDirection)];
    }
}
