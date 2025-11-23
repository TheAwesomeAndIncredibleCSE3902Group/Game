using System;
using System.Collections.Generic;
using AwesomeRPG.Map;

namespace AwesomeRPG;

/// <summary>
/// The Bow can shoot an Arrow
/// </summary>
public class Bow : Equipment
{
    protected override List<Projectile> CreateProjectiles()
    {
        PlayerOverworld player = Player.Instance.PlayerOverworld;
        return [new WaveyArrow(player.Position,player.FacingDirection)];
    }
}
