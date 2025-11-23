using System;
using System.Collections.Generic;
using AwesomeRPG.Map;
using System.Diagnostics;

namespace AwesomeRPG;

/// <summary>
/// The SwordSheathe that holds a sword
/// </summary>
public class SwordSheathe : Equipment
{
    protected override List<Projectile> CreateProjectiles()
    {
        PlayerOverworld player = Player.Instance.PlayerOverworld;
        return [new PlayerSword(player.Position, player.FacingDirection)];
    }
}
