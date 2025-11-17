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
        Player player = Player.Instance;
        return [new PlayerBoomerang(player.Position, player.FacingDirection)];
    }
}
